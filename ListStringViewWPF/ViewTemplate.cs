using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace ListStringViewWPF
{
    /// <summary>
    /// Шаблон отображения вида блока компонента (строка)
    /// </summary>
    public class ViewTemplate
    {
        /// <summary>
        /// Цвет фона
        /// </summary>
        public Brush ColorBackground { get; set; }
        /// <summary>
        /// Название шрифта
        /// </summary>
        public string FontName { get; set; }
        /// <summary>
        /// Размер шрифта
        /// </summary>
        public int FontSize { get; set; }
        /// <summary>
        /// Цвет шрифта
        /// </summary>
        public Brush FontColor { get; set; }
        public ViewTemplate()
        {
            ColorBackground = Brushes.White;
            FontName = "Arial";
            FontSize = 10;
            FontColor = Brushes.Black;
        }
        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        /// <param name="colorBackground">Цвет фона</param>
        /// <param name="fontName">Название шрифта</param>
        /// <param name="fontSize">Размер шрифта</param>
        /// <param name="fontColor">Цвет шрифта</param>
        public ViewTemplate(Brush colorBackground, string fontName, int fontSize, Brush fontColor)
            : this()
        {
            ColorBackground = colorBackground;
            FontName = fontName;
            FontSize = fontSize;
            FontColor = fontColor;
        }
    }
}
