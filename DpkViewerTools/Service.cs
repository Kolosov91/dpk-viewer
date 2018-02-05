using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace DpkViewer.Tools
{
    public static class Service
    {
        /// <summary>
        /// Расчёт оптиммального размера шрифта (чтобы максиматьно полное заполнение прямоугольника текстом)
        /// </summary>
        /// <param name="sz">Размер прямоугольника текста</param>
        /// <param name="strLength">количество символов в строке</param>
        /// <returns>Оптимальный размер шрифта</returns>
        public static double GetOptimalFontSize(Size sz, int strLength)
        {
            Size curSz = new Size(sz.Width-0.2, sz.Height-0.2);
            double fontSize = curSz.Height * 0.7;
            if (fontSize <= 0) return 0.4;
            for (double dx = 0.1; dx < 1; dx += 0.1)
                if ((strLength * fontSize * (dx - 0.3)) >= curSz.Width)
                {
                    fontSize = curSz.Height * (dx - 0.3);
                    break;
                }
            if (fontSize <= 0) return 0.4;
            return fontSize;
        }
        public static void DrawTxt(DrawingContext dc, string str, Point pt, Size sz, Brush brush, string fontName)
        {
            FormattedText txt = new FormattedText(str, System.Globalization.CultureInfo.CurrentCulture, System.Windows.FlowDirection.LeftToRight, new Typeface(fontName), 2, brush);
            txt.MaxTextWidth = (sz.Width == 0) ? 1 : sz.Width;
            txt.MaxTextHeight = (sz.Height == 0) ? 1 : sz.Height;
            txt.TextAlignment = TextAlignment.Center;
            txt.SetFontSize(GetOptimalFontSize(sz, str.Length));
            dc.DrawText(txt, pt);
        }
        public static List<bool> ConvertFromInt(int value, int cnt)
        {
            List<bool> values = new List<bool>();
            int currentCnt = 0;
            while (currentCnt < cnt)
            {
                if ((value & (0x1 << currentCnt)) > 0)
                    values.Add(true);
                else
                    values.Add(false);
                currentCnt++;
            }
            return values;
        }
        public static bool IsEqual(List<bool> list_1, List<bool> list_2)
        {
            if ((list_1 == null) && (list_2 == null)) return true;
            if ((list_1 == null) || (list_2 == null)) return false;
            if (list_1.Count != list_2.Count) return false;
            for (int i = 0; i < list_1.Count; i++)
                if (list_1[i].Equals(list_2[i])) continue;
                else return false;
            return true;
        }
        public static List<bool> ParseBinFrom(string value, out bool isGood)
        {
            List<bool> values = new List<bool>();
            for (int i = 0; i < value.Length; i++)
                if (value[i].Equals('1')) values.Add(true);
                else if (value[i].Equals('0')) values.Add(false);
                else { isGood = false; return null; }
            isGood = true;
            return values;
        }
        public static string ConvertFrom(List<bool> val)
        {
            string res = "";
            if (val == null) return res;
            foreach (bool item in val)
                if (item) res = res + '1';
                else res = res + '0';
            return res;
        }
    }
}
