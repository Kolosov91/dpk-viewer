using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DPK;
using DpkViewer.Tools;

namespace DpkViewerTools.Descryptor
{
    public class ld_f_simple : ILineDescryptor
    {
        public const string NameToken = "f_simple";
        public int FirstBit { get; set; }
        public int CountBits { get; set; }
        public string NameField { get; set; }

        public ld_f_simple()
        {
            FirstBit = LineDescryptorConst.MIN_NUMBER_BIT;
            CountBits = LineDescryptorConst.MIN_LENGHT_FIELD;
            NameField = LineDescryptorConst.DEFAULT_NAME_FIELD;
        }

        public string GetText(DpkWordItem word)
        {
            List<bool> fieldVal = new List<bool>();
            List<bool> dataVal = Service.ConvertFromInt(word.DATA, 24);
            for (int i = FirstBit - 9; i < (CountBits + FirstBit - 9); i++)
            { fieldVal.Add(dataVal[i]); }
            return string.Format("{0} {1}", NameField, Service.ConvertFrom(fieldVal));
        }
    }

    public class ld_f_variant : ILineDescryptor
    {
        public const string NameToken = "f_variant";
        public int FirstBit { get; set; }
        public int CountBits { get; set; }
        public string NameField { get; set; }
        public Dictionary<List<bool>, string> Values { get; set; }

        public ld_f_variant()
        {
            FirstBit = LineDescryptorConst.MIN_NUMBER_BIT;
            CountBits = LineDescryptorConst.MIN_LENGHT_FIELD;
            NameField = LineDescryptorConst.DEFAULT_NAME_FIELD;
            Values = new Dictionary<List<bool>, string>();
        }

        public string GetText(DpkWordItem word)
        {
            List<bool> fieldVal = new List<bool>();
            List<bool> dataVal = Service.ConvertFromInt(word.DATA, 24);
            for (int i = FirstBit - 9; i < (CountBits + FirstBit - 9); i++)
            { fieldVal.Add(dataVal[i]); }
            string res = string.Format("{0} {1}", NameField, Service.ConvertFrom(fieldVal));
            foreach (List<bool> key in Values.Keys)
            {
                if (Service.IsEqual(fieldVal, key))
                { res += " - " + Values[key]; break; }
            }
            return res;
        }
    }

    public class ld_f_enum : ILineDescryptor
    {
        public const string NameToken = "f_enum";
        public int FirstBit { get; set; }
        public int CountBits { get; set; }
        public Dictionary<List<bool>, string> ListEnum { get; set; }

        public ld_f_enum()
        {
            FirstBit = LineDescryptorConst.MIN_NUMBER_BIT;
            CountBits = LineDescryptorConst.MIN_LENGHT_FIELD;
            ListEnum = new Dictionary<List<bool>, string>();
        }

        public string GetText(DpkWordItem word)
        {
            List<bool> fieldVal = new List<bool>();
            List<bool> dataVal = Service.ConvertFromInt(word.DATA, 24);
            for (int i = FirstBit - 9; i < (CountBits + FirstBit - 9); i++)
            { fieldVal.Add(dataVal[i]); }
            //
            string res = "";
            foreach (List<bool> key in ListEnum.Keys)
            {
                bool flag = true;
                int numBit = 0;
                foreach (bool item in key)
                {
                    if (item)
                        if (!item.Equals(fieldVal[numBit])) { flag = false; break; }
                    numBit++;
                }
                if (flag) res += ListEnum[key] + ", ";
            }
            return res;
        }
    }
}
