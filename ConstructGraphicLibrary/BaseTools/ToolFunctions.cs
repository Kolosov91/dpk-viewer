using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows;
using System.Globalization;
using System.Windows.Media.Imaging;

namespace ConstructGraphicLibrary.BaseTools
{
    /// <summary>
    /// Статический класс (сервис) полезных функций
    /// </summary>
    public static class ToolFunctions
    {
        /// <summary>
        /// Получить изменение (растояние) по оси Х из времени
        /// </summary>
        /// <param name="currentTime">текущее время</param>
        /// <param name="TimeInPt">коэффициент</param>
        /// <param name="leftTime">время левой границы</param>
        /// <returns>растояние в пикселях между левой временной границей и текущим временем</returns>
        public static double GetDxByTime(TimeSpan currentTime, double TimeInPt, TimeSpan leftTime)
        {
            return Math.Abs(currentTime.TotalMilliseconds - leftTime.TotalMilliseconds) / TimeInPt;
        }
        /// <summary>
        /// Функция расчёта коэффициента TimeInPoint
        /// </summary>
        /// <param name="x1">Точка на экране левой временной границы (в пикселях от левого края окна)</param>
        /// <param name="x2">Точка на экране правой временной границы (в пикселях от левого края окна)</param>
        /// <param name="t1">Левая временная граница</param>
        /// <param name="t2">Правая временная граница</param>
        /// <returns>Значение коэффиента TimeInPoint</returns>
        public static double GetTimeInPt(double x1, double x2, TimeSpan t1, TimeSpan t2)
        {
            double msecD = t1.Subtract(t2).Duration().TotalMilliseconds;
            return (msecD) / Math.Abs(x1 - x2);
        }
        /// <summary>
        /// Написть текст
        /// </summary>
        /// <param name="dc">контекст рисования</param>
        /// <param name="str">текст</param>
        /// <param name="pt">точка вывода</param>
        /// <param name="sz">размеры области вывода</param>
        /// <param name="brush">цвет текста</param>
        /// <param name="fntSz">размер шрифта</param>
        /// <param name="txtAl">выравнивание текста</param>
        public static void DrawTxt(DrawingContext dc, string str, Point pt, Size sz, Brush brush, double fntSz, TextAlignment txtAl, string fontName = "Calibri")
        {
            FormattedText txt = new FormattedText(str, System.Globalization.CultureInfo.CurrentCulture, System.Windows.FlowDirection.LeftToRight, new Typeface(fontName), 2, brush);
            txt.MaxTextWidth = (sz.Width == 0) ? 1 : sz.Width;
            txt.MaxTextHeight = (sz.Height == 0) ? 1 : sz.Height;
            txt.TextAlignment = txtAl;
            txt.SetFontSize(fntSz);
            dc.DrawText(txt, pt);
        }
        static double GetOptimalFontHeight(string txt, Rect txtRect, double factorHeight)
        {
            string[] strings = txt.Split('\n');
            int maxChars = 0;
            for (int i = strings.Length-1; i >= 0; i--)
                if (strings[i].Length >= maxChars) { maxChars = strings[i].Length; }
            return (txtRect.Width / (double)maxChars) * factorHeight;
        }
        public static void DrawAutoTxt(DrawingContext dc, string txt, Rect txtRect, Brush txtBrush, TextAlignment txtAlignment, double factorHeight, string fontName = "Calibri")
        {
            double fontHeight = GetOptimalFontHeight(txt, txtRect, factorHeight);
            FormattedText fmtTxt = new FormattedText(txt, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface(fontName), fontHeight, txtBrush);
            fmtTxt.MaxTextWidth = (txtRect.Width == 0) ? 1 : txtRect.Width;
            fmtTxt.MaxTextHeight = (txtRect.Height == 0) ? 1 : txtRect.Height;
            fmtTxt.TextAlignment = txtAlignment;
            dc.DrawText(fmtTxt, txtRect.Location);
        }
        /// <summary>
        /// нарисовать временную линию
        /// </summary>
        /// <param name="dc">контекст ривоания</param>
        /// <param name="pt">точка начала линии</param>
        /// <param name="cellSz">размер области текста вывода</param>
        /// <param name="txt">текстовое значение времени</param>
        /// <param name="hLine">высота линии</param>
        public static void DrawVTimeLine(DrawingContext dc, Point pt, Size cellSz, string txt, double hLine, bool isRightCellTxt = true, double fontSize = 12, string fontName = "Calibri")
        {
            double shiftK = 2;
            Point ptTxt = new Point(pt.X + shiftK, pt.Y);
            if (isRightCellTxt)
                DrawTxt(dc, txt, ptTxt, cellSz, SystemColors.ControlTextBrush, fontSize, TextAlignment.Left, fontName);
            else
                DrawTxt(dc, txt, new Point(ptTxt.X - cellSz.Width - shiftK, ptTxt.Y), cellSz, SystemColors.ControlTextBrush, fontSize, TextAlignment.Left, fontName);
            dc.DrawLine(new Pen(Brushes.Gray, 0.8), pt, new Point(pt.X, pt.Y + hLine));
        }
        /// <summary>
        /// Получить время по точке
        /// </summary>
        /// <param name="pt">точка экранная</param>
        /// <param name="leftPt">левая точка</param>
        /// <param name="TimeInPt">стоимость пикселя в мс</param>
        /// <param name="leftTime">левое время</param>
        /// <returns>время по точке</returns>
        public static TimeSpan GetTimeByPoint(Point pt, Point leftPt, double TimeInPt, TimeSpan leftTime)
        {
            double wdth = pt.X - leftPt.X;
            double tmp = wdth * TimeInPt;
            TimeSpan tm = new TimeSpan((long)((tmp + leftTime.TotalMilliseconds) * 10000.0));
            return tm;
        }
        /// <summary>
        /// Получить строковое значение времени
        /// </summary>
        /// <param name="time">время</param>
        /// <returns>строковое значение времени</returns>
        public static string TimeSpanToString(TimeSpan time)
        {
            return time.Hours.ToString().PadLeft(2, '0') + ":" + time.Minutes.ToString().PadLeft(2, '0') + " " + time.Seconds.ToString().PadLeft(2, '0') + ":" + time.Milliseconds.ToString().PadLeft(3, '0');
        }
        /// <summary>
        /// Рисование горизонтальной линии со значением текстовым
        /// </summary>
        /// <param name="dc">контекст рисования</param>
        /// <param name="ptLine">точка расположения линии</param>
        /// <param name="cellSzTxt">размер ячейки с текстом</param>
        /// <param name="lenghtLine">длина линии</param>
        /// <param name="txt">текст</param>
        /// <param name="fontSz">размер шрифта</param>
        /// <param name="fontName">имя шрифта</param>
        /// <param name="isTxtUnderLine">расположение текста</param>
        public static void DrawHValueLine(DrawingContext dc, Point ptLine, Size cellSzTxt, double lenghtLine, string txt, double fontSz = 12, string fontName = "Calibri", bool isTxtUnderLine = false)
        {
            if (isTxtUnderLine)
                DrawTxt(dc, txt, ptLine, cellSzTxt, Brushes.Brown, fontSz, TextAlignment.Left, fontName);
            else
                DrawTxt(dc, txt, new Point(ptLine.X, ptLine.Y - cellSzTxt.Height), cellSzTxt, Brushes.Brown, fontSz, TextAlignment.Left, fontName);
            dc.DrawLine(new Pen(Brushes.Brown, 0.8), ptLine, new Point(ptLine.X + lenghtLine, ptLine.Y));
        }
        /// <summary>
        /// Написать значение в точке в текстовом виду
        /// </summary>
        /// <param name="dc">контекст рисования</param>
        /// <param name="pt">текущая точка</param>
        /// <param name="nextPt">следующая точка</param>
        /// <param name="value">значение в точке</param>
        public static bool DrawTxtValue(DrawingContext dc, Point pt, Point nextPt, int value, bool IsTextUnderPrevPoint, Size cellSize)
        {
            bool IsTextUnderPoint = true;
            Size sz = new Size(cellSize.Width, cellSize.Height);
            Point ptRect = new Point();
            Rect TxtValRect = new Rect(ptRect, cellSize);
            dc.PushOpacity(0.7);
            switch (pt.Y.CompareTo(nextPt.Y))
            {
                case -1:          
                    OVER_POINT:
                        ptRect = new Point(pt.X + 2, pt.Y - cellSize.Height - 2);
                        TxtValRect = new Rect(ptRect, sz);
                        IsTextUnderPoint = false;
                    break;
                case 0:
                    if (IsTextUnderPrevPoint) goto OVER_POINT;
                    else goto UNDER_POINT;
                case 1:                 
                    UNDER_POINT:
                        ptRect = new Point(pt.X + 2, pt.Y + 2);
                        TxtValRect = new Rect(ptRect, sz);
                        IsTextUnderPoint = true;
                    break;
            }
            dc.DrawRoundedRectangle(Brushes.WhiteSmoke, new Pen(Brushes.White, 1), TxtValRect, TxtValRect.Height / 5, TxtValRect.Height / 5);
            ToolFunctions.DrawTxt(dc, "0x" + value.ToString("X"), ptRect, cellSize, Brushes.Brown, 10, TextAlignment.Left);
            dc.Pop();
            return IsTextUnderPoint;
        }
        public static ImageSource GetImage(string asseblyName, string resouceName)
        {
            Uri uri = new Uri("pack://application:,,,/" + asseblyName + ";component/" + resouceName, UriKind.RelativeOrAbsolute);
            return BitmapFrame.Create(uri, BitmapCreateOptions.None, BitmapCacheOption.Default);
        }
    }
}
