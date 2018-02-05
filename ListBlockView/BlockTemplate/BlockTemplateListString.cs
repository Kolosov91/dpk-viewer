using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Globalization;
using System.Windows;

namespace ListBlockViewLib.BlockTemplate
{
    public class BlockTemplateListString : BlockTemplateBase
    {
        /// <summary>
        /// Алгоритм рендеринга
        /// </summary>
        /// <param name="dc"> контекст рисования </param>
        new void DataRender(DrawingContext dc)
        {
            //рисуем прямоугольник фона
            dc.DrawRectangle(ColorBackground, new Pen(ColorBorder, 1.0), this.RenderRect);
            if ((Strings == null) || (Strings.Count == 0)) return;
            Point currentLocation = this.RenderRect.Location;
            foreach (string str in Strings)
            {
                //форматируем текст для рисования
                FormattedText txt = new FormattedText(str, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface(FontName), FontSize, ColorFont);
                txt.MaxTextWidth = this.RenderRect.Width;
                txt.MaxTextHeight = this.RenderRect.Height;
                txt.TextAlignment = TextAlignment.Justify;
                //рисуем текст
                dc.DrawText(txt, currentLocation);
                currentLocation.Y += HeightLine;
            }
        }

        public BlockTemplateListString()
        {
            base.DataRender = this.DataRender;
            HeightLine = 14;
            ColorBackground = Brushes.WhiteSmoke;
            ColorBorder = Brushes.Gray;
            ColorFont = Brushes.Black;
            FontSize = 10;
            FontName = "Courrier New";
            Strings = new List<string>();
        }

        public double HeightLine { get; set; }
        public Brush ColorBackground { get; set; }
        public Brush ColorBorder { get; set; }
        public Brush ColorFont { get; set; }
        public double FontSize { get; set; }
        public string FontName { get; set; }
        public List<string> Strings { get; set; }
    }
}
