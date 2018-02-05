using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DPK;

namespace DpkViewerTools.Descryptor
{
    public interface ILineDescryptor
    {
        int FirstBit { get; set; }
        int CountBits { get; set; }
        string GetText(DpkWordItem word);
    }

    public static class LineDescryptorConst
    {
        public const int MIN_NUMBER_BIT = 9;
        public const int MAX_NUMBER_BIT = 32;
        //
        public const int MIN_LENGHT_FIELD = 1;
        public const int MAX_LENGTH_FIELD = 24;
        //
        public const string DEFAULT_NAME_FIELD = "NONE";
        //
        public static bool IsNumBitValid(int numBit)
        {
            if ((numBit >= MIN_NUMBER_BIT) && (numBit <= MAX_NUMBER_BIT)) return true;
            else return false;
        }
        public static bool IsLenghtFiledValid(int numBit, int length)
        {
            if (!IsNumBitValid(numBit)) return false;
            if ((length >= MIN_LENGHT_FIELD) && (length <= (MAX_LENGTH_FIELD - (numBit - MIN_NUMBER_BIT)))) return true;
            else return false;
        }
    }
}
