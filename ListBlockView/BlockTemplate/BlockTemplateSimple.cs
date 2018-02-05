using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Globalization;
using System.Windows;

namespace ListBlockViewLib.BlockTemplate
{
    /// <summary>
    /// простенький Шаблон рендеринга
    /// </summary>
    public class BlockTemplateSimple : BlockTemplateBase
    {
        /// <summary>
        /// Текстовая строка
        /// </summary>
        public string Data { get; set; }
        /// <summary>
        /// Цвет фона
        /// </summary>
        public Brush ColorBackground { get; set; }
        /// <summary>
        /// Цвет границ
        /// </summary>
        public Brush ColorBorder { get; set; }
        /// <summary>
        /// Цвет шрифта
        /// </summary>
        public Brush ColorFont { get; set; }
        /// <summary>
        /// размер шрифта
        /// </summary>
        public double FontSize { get; set; }
        /// <summary>
        /// Название шрифта
        /// </summary>
        public string FontName { get; set; }

        public BlockTemplateSimple()
        {
            base.DataRender = this.DataRender;
            ColorBackground = Brushes.WhiteSmoke;
            ColorBorder = Brushes.Gray;
            ColorFont = Brushes.Black;
            FontSize = 10;
            FontName = "Calibri";
        }

        /// <summary>
        /// Алгоритм рендеринга
        /// </summary>
        /// <param name="dc"> контекст рисования </param>
        new void DataRender(DrawingContext dc)
        {
            //рисуем границу
            dc.DrawRectangle(ColorBackground, new Pen(ColorBorder, 1.0), this.RenderRect);
            //форматируем текст для рисования
            FormattedText txt = new FormattedText(Data, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface(FontName), FontSize, ColorFont); 
            txt.MaxTextWidth = this.RenderRect.Width;
            txt.MaxTextHeight = this.RenderRect.Height;
            txt.TextAlignment = TextAlignment.Justify;
            //рисуем текст
            dc.DrawText(txt, this.RenderRect.Location);
        }
    }
}
