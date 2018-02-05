using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DPK;
using DpkViewer.Tools;

namespace DpkViewerTools.Descryptor
{
    public class FormatDescryptorWord
    {
        public List<bool> Address { get; set; }
        public List<ILineDescryptor> ListLines { get; set; }
        public FormatDescryptorWord() { ListLines = new List<ILineDescryptor>(); Address = new List<bool>(); }
        public string GetText(int numWord, DpkWordItem word)
        {
            return string.Format("№ {0} Время: {1} Адрес[1-8]: {2} Данные[9-32]: {3}", new object[] {numWord, 
                string.Format("{0}:{1}:{2}:{3}", word.Time.Hours.ToString().PadLeft(2,'0'), word.Time.Minutes.ToString().PadLeft(2,'0'), 
                word.Time.Seconds.ToString().PadLeft(2,'0'), word.Time.Milliseconds.ToString().PadLeft(3,'0')),
                Service.ConvertFrom(Service.ConvertFromInt(word.ADR,8)), 
                Service.ConvertFrom(Service.ConvertFromInt(word.DATA,24))});
        }
    }
}
