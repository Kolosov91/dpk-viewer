using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConstructGraphicLibrary.Data
{
    /// <summary>
    /// Данные для орагинзации визуального компонента HorizontalTimeScrollBar
    /// </summary>
    public class HorizontalTimeScrollBarData
    {
        /// <summary>
        /// Левая временная граница
        /// </summary>
        public TimeSpan LeftTimeBorder { get; set; }
        /// <summary>
        /// Правая временная граница
        /// </summary>
        public TimeSpan RightTimeBorder { get; set; }
        /// <summary>
        /// Левая временная граница видимого интервала
        /// </summary>
        public TimeSpan CurrentLeftTime { get; set; }
        /// <summary>
        /// Правая временная граница видимого интервала
        /// </summary>
        public TimeSpan CurrentRightTime { get { return CurrentLeftTime.Add(VisibilityTimeInterval); } }
        /// <summary>
        /// Длина видимого временного интервала
        /// </summary>
        public TimeSpan VisibilityTimeInterval { get; set; }
        /// <summary>
        /// Конструктор
        /// </summary>
        public HorizontalTimeScrollBarData() 
        {
            LeftTimeBorder = TimeSpan.Zero;
            RightTimeBorder = TimeSpan.Zero;
            CurrentLeftTime = TimeSpan.Zero;
            VisibilityTimeInterval = TimeSpan.Zero;
        }
    }
}
