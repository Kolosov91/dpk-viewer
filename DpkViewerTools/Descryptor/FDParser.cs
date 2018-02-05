using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DpkViewer.Tools;

namespace DpkViewerTools.Descryptor
{
    public class FDParser
    {
        public List<FormatDescryptorWord> Descryptors { get; protected set; }
        public bool IsValidParse { get; protected set; }
        public FDParser()
        {
            Descryptors = new List<FormatDescryptorWord>();
            IsValidParse = false;
        }
        public class Lexems
        {
            public const string WORD = "word";
            public const string END = "end";
            public const string COMMENTORY = "!";
        }
        public void Parse(List<string> lines)
        {
            FormatDescryptorWord currentDescryptor = null;//текущий выбранный описатель слова
            IsValidParse = false;
            Descryptors.Clear();
            //обходим все строки с описателями
            for (int i = 0; i < lines.Count; i++)
            {
                string[] tokenLines = lines[i].Trim().Split(new char[] { ' ' });//разбиваем строку на лексемы
                if (tokenLines[0].Equals(Lexems.COMMENTORY))//пропускаем строку с комментариями
                    continue;
                int tempVal = 0;
                bool tempFlag = false;
                switch (tokenLines[0])//чему равен первая лексема (описано в куроводстве оператора)
                {
                    case Lexems.WORD://лексема - отправка данных - описатель слова
                        if (currentDescryptor != null) { IsValidParse = false; return; }//проверка на повторный токен описателя в строке
                        if (tokenLines.Length != 2) { IsValidParse = false; return; }//проверка кол-ва аргументов
                        //создание описателя данных
                        currentDescryptor = new FormatDescryptorWord();
                        //разбор аргументов
                        /*Адрес*/
                        if (tokenLines[1].Length != 8) { IsValidParse = false; return; }
                        currentDescryptor.Address = Service.ParseBinFrom(tokenLines[1], out tempFlag);
                        if (!tempFlag) { IsValidParse = false; return; }
                        break;
                    case ld_f_simple.NameToken://лексема - описатель поля в слове - прростой
                        if (currentDescryptor == null) { IsValidParse = false; return; }
                        ld_f_simple ld_simple = new ld_f_simple();
                        ld_simple.NameField = tokenLines[1];//строковое имя поля
                        if (int.TryParse(tokenLines[2], out tempVal))
                            if (LineDescryptorConst.IsNumBitValid(tempVal))
                            { ld_simple.FirstBit = tempVal; }//номер бита
                            else { IsValidParse = false; return; }
                        else { IsValidParse = false; return; }
                        if (int.TryParse(tokenLines[3], out tempVal))
                            if (LineDescryptorConst.IsLenghtFiledValid(ld_simple.FirstBit, tempVal))
                            { ld_simple.CountBits = tempVal; }//кол-во бит
                            else { IsValidParse = false; return; }
                        else { IsValidParse = false; return; }
                        currentDescryptor.ListLines.Add(ld_simple);
                        break;
                    case ld_f_variant.NameToken://лексема - описатель поля в слове - вариантный
                        if (currentDescryptor == null) { IsValidParse = false; return; }
                        ld_f_variant ld_variant = new ld_f_variant();
                        ld_variant.NameField = tokenLines[1];//строковое имя поля
                        /*2*/
                        if (int.TryParse(tokenLines[2], out tempVal))
                            if (LineDescryptorConst.IsNumBitValid(tempVal))
                            { ld_variant.FirstBit = tempVal; }//номер бита
                            else { IsValidParse = false; return; }
                        else { IsValidParse = false; return; }
                        /**/
                        /*3*/
                        if (int.TryParse(tokenLines[3], out tempVal))
                            if (LineDescryptorConst.IsLenghtFiledValid(ld_variant.FirstBit, tempVal))
                            { ld_variant.CountBits = tempVal; }//кол-во бит
                            else { IsValidParse = false; return; }
                        else { IsValidParse = false; return; }
                        /**/
                        for (int j = 4; j < tokenLines.Length; j += 2)//обход значений и их констант (обозначений)
                        {
                            bool isGood= false;
                            if (tokenLines[j].Length != ld_variant.CountBits) { IsValidParse = false; return; }
                            List<bool> value = Service.ParseBinFrom(tokenLines[j], out isGood);
                            if (!isGood) { IsValidParse = false; return; }
                            ld_variant.Values.Add(value, tokenLines[j + 1]);                            
                        }
                        currentDescryptor.ListLines.Add(ld_variant);
                        break;
                    case ld_f_enum.NameToken:
                        if (currentDescryptor == null) { IsValidParse = false; return; }
                        ld_f_enum ld_enum = new ld_f_enum();
                        /*2*/
                        if (int.TryParse(tokenLines[1], out tempVal))
                            if (LineDescryptorConst.IsNumBitValid(tempVal))
                            { ld_enum.FirstBit = tempVal; }//номер бита
                            else { IsValidParse = false; return; }
                        else { IsValidParse = false; return; }
                        /**/
                        /*3*/
                        if (int.TryParse(tokenLines[2], out tempVal))
                            if (LineDescryptorConst.IsLenghtFiledValid(ld_enum.FirstBit, tempVal))
                            { ld_enum.CountBits = tempVal; }//кол-во бит
                            else { IsValidParse = false; return; }
                        else { IsValidParse = false; return; }
                        /**/
                        for (int j = 3; j < tokenLines.Length; j += 2)//обход значений и их констант (обозначений)
                        {
                            bool isGood = false;
                            if (tokenLines[j].Length != ld_enum.CountBits) { IsValidParse = false; return; }
                            List<bool> value = Service.ParseBinFrom(tokenLines[j], out isGood);
                            if (!isGood) { IsValidParse = false; return; }
                            ld_enum.ListEnum.Add(value, tokenLines[j + 1]);
                        }
                        currentDescryptor.ListLines.Add(ld_enum);
                        break;
                    case Lexems.END://лексема - конец описателя слова
                        this.Descryptors.Add(currentDescryptor);
                        currentDescryptor = null;
                        IsValidParse = true;
                        break;
                    default:
                        IsValidParse = false;
                        return;
                }
            }
        }
    }
}
