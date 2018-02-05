using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DPK;

namespace GeneratorDpkLogFile
{
    class Program
    {
        readonly static int Version = 1;
        readonly static int Revision = 0;
        static void Main(string[] args)
        {
            Console.WriteLine(string.Format("Программа генерации DpkLog-файла версии {0}.{1}", Version, Revision));
            DpkDataBuf dpkLogFile = new DpkDataBuf();
            List<object> dpkWords = new List<object>();
            Random ramdomizer = new Random();
            Console.WriteLine("Введите кол-во слов ДПК в файле и нажмите клавишу 'Enter':");
            int countWords = 10000;
            if (!int.TryParse(Console.ReadLine(), out countWords)) countWords = 10000;
            TimeSpan time = TimeSpan.Zero;
            for (int i = 0; i < countWords; i++)
            {
                DpkWordItem word = new DpkWordItem();
                word.IsGood = true;
                word.Flags = 0;
                word.ADR = 1;
                word.DATA = ramdomizer.Next(0, 0xFFFF);
                word.Time = time;
                time = time.Add(new TimeSpan(0, 0, 0, 0, ramdomizer.Next(10, 30)));
                dpkWords.Add(word);
            }
            dpkLogFile.PutData(dpkWords);
            Console.WriteLine("Введите имя DpkLog-файла и нажмите клавишу 'Enter':");
            string fileName = Console.ReadLine();
            dpkLogFile.SaveToFile(fileName + ".dpklog");
        }
    }
}
