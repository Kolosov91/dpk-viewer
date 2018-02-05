using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace ConstructGraphicLibrary.BaseTools
{
    /// <summary>
    /// Типо макросы, как в Си
    /// </summary>
    public static class Macros
    {
        public delegate Brush ChooseBrushDelegate(int currentIndex, Brush currentBrush, int selectedIndex, Brush selectedBrush);
        /// <summary>
        /// Выбор кисти
        /// (если индексы равны - то selectedBrush, иначе - currentBrush)
        /// </summary>
        public static ChooseBrushDelegate ChooseBrush =
            (int currentIndex, Brush currentBrush, int selectedIndex, Brush selectedBrush) =>
            { return (currentIndex.Equals(selectedIndex)) ? selectedBrush : currentBrush; };
        /*----------------------------------------------------------------------------------*/
        public delegate bool CheckEntryTimeDelegate(TimeSpan leftTime, TimeSpan currentTime, TimeSpan rightTime);
        /// <summary>
        /// Проверка вхождения времени в интервал между leftTime и rightTime
        /// </summary>
        public static CheckEntryTimeDelegate CheckEntryTime =
            (TimeSpan leftTime, TimeSpan currentTime, TimeSpan rightTime) =>
            { return (currentTime.CompareTo(leftTime) >= 0) && (currentTime.CompareTo(rightTime) <= 0); };
        /*----------------------------------------------------------------------------------*/
        public delegate string TimeSpanToStringDelegate(TimeSpan time);
        /// <summary>
        /// Преобразование TimeSpan в двухстрочный string (строки разделяются символом перехода коретки на след строку)
        /// </summary>
        public static TimeSpanToStringDelegate TimeSpanToString =
            (TimeSpan time) =>
            { return string.Format("{0}:{1}{2}{3}:{4}",
                time.Hours.ToString().PadLeft(2,'0'),
                time.Minutes.ToString().PadLeft(2,'0'),
                '\n',
                time.Seconds.ToString().PadLeft(2,'0'),
                time.Milliseconds.ToString().PadLeft(3,'0')); };
        /*----------------------------------------------------------------------------------*/
        /// <summary>
        /// Преобразование TimeSpan в string
        /// </summary>
        public static TimeSpanToStringDelegate TimeSpanToStringLine =
            (TimeSpan time) =>
            {
                return string.Format("{0}:{1}:{2}:{3}",
                  time.Hours.ToString().PadLeft(2, '0'),
                  time.Minutes.ToString().PadLeft(2, '0'),
                  time.Seconds.ToString().PadLeft(2, '0'),
                  time.Milliseconds.ToString().PadLeft(3, '0'));
            };
    }
}
