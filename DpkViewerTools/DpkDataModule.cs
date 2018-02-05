using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using AsvTools;

namespace DPK
{
    public interface IDpkWordInterface
    {
        TimeSpan Time { get; set; }
        int Flags { get; set; }
        int ADR { get; set; }
        int DATA { get; set; }
        bool IsGood { get; set; }
    }
    /// <summary>
    /// Коды сообщений Arinc
    /// </summary>
    public abstract class CODES_MSG_ARINC
    {
        /// <summary>
        /// Ошибки модуля ARINC
        /// </summary>
        public const byte CODE_MSG_ERR = 0x0;
        /// <summary>
        /// Принятая информация
        /// </summary>
        public const byte CODE_MSG_DATA = 0x1;
        /// <summary>
        /// Ответ на команду
        /// </summary>
        public const byte CODE_MSG_ANSWER = 0x2;
        /// <summary>
        /// Признак переполнения буфера
        /// </summary>
        public const byte CODE_MSG_OVER = 0xF;
    }
    public abstract class DpkDataConstants
    {
        public const string LOG_FILE_EXT = "dpklog";
        public const string LOG_FILE_MASK = "Файлы протокола ДПК|*." + LOG_FILE_EXT;
        public const string CFG_FILE_EXT = "dpkcfg";
        public const string CFG_FILE_MASK = "Файлы настройки ДПК|*." + CFG_FILE_EXT;

        public const byte CODE_SPEED_12 = 0;
        public const byte CODE_SPEED_50 = 1;
        public const byte CODE_SPEED_100 = 2;

        public const byte CODE_MSG_ERR = 0x0;	//Передаётся информация об ошибке контроллера ДПК. Возникает при ошибках в работе контроллера
        public const byte CODE_MSG_DATA = 0x1;	//Передается принятая информация
        public const byte CODE_MSG_ANSWER = 0x2;	//Ответ на входное сообщение
        public const byte CODE_MSG_OVER = 0xF;	//Признак переполнения входного буфера (потеря информации)

        public const int RECVW_COUNT = 32;         //Макс. количество адресов слов ДПК для приема
        public const int RECVM_COUNT = 32;         //Макс. количество масок ДПК для приема

        public const byte CODE_CMD_SETUP = 0x1;	//Установка параметров модуля приема
        public const byte CODE_CMD_STOP = 0x2;	//Останов. Полная остановка работы модуля приема. Возобновление только по команде CODE_CMD_SETUP
        public const byte CODE_CMD_STATE = 0x3;	//Запрос состояния модуля
        public const byte CODE_CMD_SENDER = 0x4;	//Установка параметров модуля передачи и запуск передатчика
        public const byte CODE_CMD_STOPSEND = 0x5;	//Останов передачи. Полная остановка работы модуля передачи. Возобновление только по команде CODE_CMD_ SENDER
        public const int SENDER_COUNT = 16;         //Макс. количество слов ДПК для передачи

        /* - [Flags] - */
        public const byte SYNC_FLAG = 0x1;
        public const int ERROR_FLAG = 0x2;
        public const int OVERFLOW_FLAG = 0x4;
    }


    public class DpkWordItem : IDpkWordInterface, IDublicate<DpkWordItem>, IStreamable
    {
        public TimeSpan Time
        {
            get;
            set;
        }
        public int Flags
        {
            get;
            set;
        }
        public int ADR
        {
            get;
            set;
        }
        public int DATA
        {
            get;
            set;
        }
        public bool IsGood
        {
            get;
            set;
        }

        public DpkWordItem Clone()
        {
            DpkWordItem res = new DpkWordItem();
            CopyTo(res);
            return res;
        }
        public void CopyTo(DpkWordItem dst)
        {
            dst.Time = new TimeSpan(Time.Ticks);
            dst.Flags = Flags;
            dst.ADR = ADR;
            dst.DATA = DATA;
            dst.IsGood = IsGood;
        }

        public void LoadFromStream(BinaryReader br)
        {
            long ticks = br.ReadInt64();
            Time = new TimeSpan(ticks);
            Flags = br.ReadInt32();
            ADR = br.ReadInt32();
            DATA = br.ReadInt32();
        }
        public void SaveToStream(BinaryWriter bw)
        {
            bw.Write(Time.Ticks);
            bw.Write(Flags);
            bw.Write(ADR);
            bw.Write(DATA);
        }
    }

    public class DpkWord
    {
        uint adr;
        uint data;

        public bool IsGood
        {
            get;
            set;
        }
        public uint Adr
        {
            get { return adr; }
            set { adr = value; }
        }

        public uint Data
        {
            get { return data; }
            set { data = value; }
        }

        public uint RawData
        {
            get { return (data << 8) + adr; }
            set { SetRawData(value); }
        }

        public DpkWord(DpkWordItem dpk)
        {
            adr = (uint)dpk.ADR;
            data = (uint)dpk.DATA;
        }

        public DpkWord(uint dpk)
        {
            SetRawData(dpk);
        }

        private void SetRawData(uint dpk)
        {
            adr = (dpk & 0xFF);
            data = (dpk >> 8) & 0xFFFFFF;
        }
        public static byte Reverse8bit(byte b)
        {
            byte result = 0, mask1 = 0x1, mask2 = 0x80;
            for(int i = 0; i < 8; i++)
            {
                result += (byte)(((mask1 & b) != 0) ? mask2 : 0);
                mask1 <<= 1;
                mask2 >>= 1;
            }
            return result;
        }
    }
    public class DpkDataPackage
    {
        byte[] raw_buf = null;
        bool isOkData = false;
        byte code_msg = 0;

        List<DpkWordItem> dpk_buf = new List<DpkWordItem>(7);

        public DpkDataPackage(byte[] src)
        {
            int count = src.Length;
            if(count > 2)
            {
                raw_buf = new byte[count - 2];
                Array.Copy(src, 2, raw_buf, 0, count - 2);
                ParseData();
            }
            else
                isOkData = false;
        }
        private void ParseData()
        {
            UInt32 u32; byte flags, mask;
            int cnt = raw_buf.Length;
            isOkData = false;
            dpk_buf.Clear();
            try
            {
                using(BinaryReader br = new BinaryReader(new MemoryStream(raw_buf)))
                {
                    code_msg = br.ReadByte();
                    switch(code_msg)
                    {
                    case CODES_MSG_ARINC.CODE_MSG_ERR:
                        if(cnt < 5)
                            return;
                        {
                            DpkWordItem dpk = new DpkWordItem();
                            u32 = br.ReadUInt32();
                            dpk.Time = new TimeSpan(u32 * 10000);
                            dpk.Flags = DpkDataConstants.ERROR_FLAG;
                            dpk_buf.Add(dpk);
                        }
                        return;
                    case CODES_MSG_ARINC.CODE_MSG_DATA:
                        {
                            flags = br.ReadByte();
                            cnt = (cnt - 2) / 8;    // количество слов ДПК
                            mask = 1;
                            for(int i = 0; i < cnt; i++)
                            {
                                DpkWordItem dpk = new DpkWordItem();
                                u32 = br.ReadUInt32();
                                dpk.Time = new TimeSpan(u32 * 10000);
                                u32 = br.ReadUInt32();
                                dpk.ADR = (int)(u32 & 0xFF);
                                dpk.DATA = (int)((u32 >> 8) & 0xFFFFFF);
                                dpk.Flags = ((mask & flags) != 0) ? 1 : 0;
                                mask <<= 1;
                                dpk_buf.Add(dpk);
                            }
                        }
                        break;
                    case CODES_MSG_ARINC.CODE_MSG_ANSWER:
                        if(cnt < 2)
                            return;
                        {
                            DpkWordItem dpk = new DpkWordItem();
                            dpk.ADR = br.ReadByte();
                            if(cnt == 3)
                                dpk.DATA = br.ReadByte();
                            dpk.Time = new TimeSpan(0);
                            dpk_buf.Add(dpk);
                        }
                        break;
                    case CODES_MSG_ARINC.CODE_MSG_OVER:
                        if(cnt < 5)
                            return;
                        {
                            DpkWordItem dpk = new DpkWordItem();
                            u32 = br.ReadUInt32();
                            dpk.Time = new TimeSpan(u32 * 10000);
                            dpk.Flags = DpkDataConstants.OVERFLOW_FLAG;
                            dpk_buf.Add(dpk);
                        }
                        break;
                    default:
                        return;
                    }
                }
                isOkData = true;
            }
            catch
            {
                isOkData = false;
            }
        }

        public bool IsGood
        {
            get { return isOkData; }
            set { isOkData = value; }
        }
        public byte CodeMsg
        {
            get { return code_msg; }
        }
        public byte ErCode
        {
            get { return raw_buf[1]; }
        }
        public byte AnswerCode
        {
            get { return raw_buf[2]; }
        }
        public List<DpkWordItem> DpkData
        {
            get { return dpk_buf; }
        }
    }

    //public class DpkDataChangedEventArgs : EventArgs
    //{
    //    public DpkDataPackage pack;
    //    public int Count;
    //    public bool overflow;

    //    public DpkDataChangedEventArgs(DpkDataPackage pack, int count, bool over)
    //        : base()
    //    {
    //        this.pack = pack;
    //        Count = count;
    //        overflow = over;
    //    }
    //}
    //public delegate void DpkDataChangedEventHandler(object sender, DpkDataChangedEventArgs ev);

    public class DpkDataBuf : IStreamable
    {
        #region MAGIC
        readonly byte[] magic = new byte[]{
0x44,   // D
0x61,   // a
0x74,   // t
0x61,   // a
0x20,   //
0x66,   // f
0x69,   // i
0x6C,   // l
0x65,   // e
0x20,   // 
0x44,   // D
0x50,   // P
0x4B,   // K
0x20,   //
0x76,   // v 
0x31,   // 1 
0x2E,   // .
0x30    // 0
        };
        #endregion
        int MAX_ITEMS_COUNT = 104000;         // ~ 10Mbyte
        List<DpkWordItem> data_buf = new List<DpkWordItem>();

        public DpkDataBuf()
        {
        }
        public DpkDataBuf(int max_items)
        {
            MAX_ITEMS_COUNT = max_items;
        }

        public void PutData(List<object> list)
        {
            data_buf.Clear();
            for(int i = 0; i < list.Count; i++)
            {
                data_buf.Add(((DpkWordItem)list[i]).Clone());
            }
        }

        public List<object> GetBuf()
        {
            List<object> res = new List<object>(data_buf.Count);
            foreach(var item in data_buf)
            {
                res.Add(item.Clone());
            }
            return res;
        }

        public DpkWordItem this[int index]
        {
            get
            {
                {
                    if(index < Count)
                        return data_buf[index];
                }
                return null;
            }
        }
        public int Count
        {
            get { return data_buf.Count; }
        }
        public void Clear()
        {
            data_buf.Clear();

        }

        public void LoadFromStream(BinaryReader br)
        {
            Clear();
            int cnt = br.ReadInt32();
            if((cnt > MAX_ITEMS_COUNT) || (cnt < 0))
                throw new Exception("Ошибка формата файла");
            for(int i = 0; i < cnt; i++)
            {
                DpkWordItem dp = new DpkWordItem();
                dp.LoadFromStream(br);
                data_buf.Add(dp);
            }
        }
        public void SaveToStream(BinaryWriter bw)
        {
            bw.Write(data_buf.Count);
            foreach(DpkWordItem item in data_buf)
            {
                item.SaveToStream(bw);
            }
        }
        public void LoadFromFile(string fn)
        {
            using(BinaryReader br = new BinaryReader(File.OpenRead(fn)))
            {
                byte[] buf = br.ReadBytes(magic.Length);
                for(int i = 0; i < magic.Length; i++)
                {
                    if(buf[i] != magic[i])
                        throw new Exception("Ошибка формата файла");
                }

                LoadFromStream(br);
            }
        }
        public void SaveToFile(string fn)
        {
            using(BinaryWriter bw = new BinaryWriter(File.Create(fn)))
            {
                bw.Write(magic);
                SaveToStream(bw);
            }
        }
    }

    public static class DpkDataParser
    {
        public static string AdrToShortString(IDpkWordInterface src)
        {
            return String.Format("{0:X2}", src.ADR);
        }

        public static string AdrToString(IDpkWordInterface src)
        {
            return (Convert.ToString(src.ADR, 2).PadLeft(8, '0'));
        }

        public static string DataToShortString(IDpkWordInterface src)
        {
            return String.Format("{0:X3}", src.DATA);
        }

        public static string DataToString(IDpkWordInterface src)
        {
            return Convert.ToString(src.DATA, 2).PadLeft(24, '0');
        }
    }


    //================================================================================================
    public class UsbDataPackageQueueOverflowEventArgs : EventArgs
    {
        public int Count;

        public UsbDataPackageQueueOverflowEventArgs(int count)
            : base()
        {
            Count = count;
        }
    }
    public delegate void UsbDataPackageQueueOverflowEventHandler(object sender, UsbDataPackageQueueOverflowEventArgs ev);
    public class UsbDataPackage
    {
        public byte[] raw_buf = null;

        public UsbDataPackage(byte[] src, int count)
        {
            raw_buf = new byte[count];
            Array.Copy(src, 0, raw_buf, 0, count);
        }
    }

    public class UsbDataPackageQueue
    {
        int MAX_ITEMS_COUNT = 104000;         // ~ 10Mbyte
        Queue<UsbDataPackage> _buf = null;
        object _buf_sync = new object();
        bool overflow = false;
        public event UsbDataPackageQueueOverflowEventHandler OnOverflowChanged;

        public UsbDataPackageQueue(int max_sz)
        {
            if(max_sz > 0)
                MAX_ITEMS_COUNT = max_sz;
            _buf = new Queue<UsbDataPackage>(MAX_ITEMS_COUNT);
        }
        public void Push(byte[] src, int count)
        {
            if(overflow)
                return;
            lock(_buf_sync)
            {
                if(_buf.Count >= MAX_ITEMS_COUNT)
                {
                    overflow = true;
                    if(OnOverflowChanged != null)
                        OnOverflowChanged(this, new UsbDataPackageQueueOverflowEventArgs(_buf.Count));
                    return;
                }
                _buf.Enqueue(new UsbDataPackage(src, count));
            }
        }

        public UsbDataPackage Pop()
        {
            UsbDataPackage res = null;
            lock(_buf_sync)
            {
                if(_buf.Count > 0)
                    res = _buf.Dequeue();
            }
            return res;
        }

        public int Count
        {
            get
            {
                lock(_buf_sync)
                {
                    return _buf.Count;
                }
            }
        }
        public bool OverFlow
        {
            get { return overflow; }
            set { overflow = value; }
        }


        internal void Clear()
        {
            lock(_buf_sync)
            {
                _buf.Clear();
                overflow = false;
            }
        }
    }

}
