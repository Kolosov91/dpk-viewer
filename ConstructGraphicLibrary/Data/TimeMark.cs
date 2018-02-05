using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConstructGraphicLibrary.Data
{
    public class TimeMark
    {
        public TimeSpan Time { get; set; }
        public string Name { get; set; }
        public TimeMark() { Time = TimeSpan.MinValue; Name = "Метка"; }
        public TimeMark(TimeSpan time, string name) { Time = time; Name = name; }
    }
}
