using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace ListBlockViewLib.BlockTemplate
{
    /// <summary>
    /// Базовый шаблон отображения
    /// </summary>
    public class BlockTemplateBase
    {
        /// <summary>
        /// Область рисования
        /// </summary>
        protected Rect RenderRect;
        /// <summary>
        /// Буфер рисования
        /// </summary>
        protected DrawingVisual RenderBuffer;
        /// <summary>
        /// Размеры области рисования
        /// </summary>
        public Size RenderSize { get { return this.RenderRect.Size; } set { this.RenderRect.Size = value; } }

        public BlockTemplateBase() { RenderRect = new Rect(); RenderSize = new Size(); RenderBuffer = new DrawingVisual(); }

        /// <summary>
        /// Базовый алгоритм рендеринга
        /// </summary>
        /// <param name="renderLocation"> точка отрисовки </param>
        /// <returns> буфер рисования </returns>
        public DrawingVisual Render(Point renderLocation)
        {
            using (DrawingContext dc = RenderBuffer.RenderOpen())
            {
                RenderRect.Location = renderLocation;
                dc.PushClip(new RectangleGeometry(RenderRect));
                //вызываем функцию в которой описан алгоритм риования
                if (DataRender != null) DataRender(dc);
                dc.Close();
            }
            return RenderBuffer;
        }

        protected delegate void DataRenderDelegate(DrawingContext dc);
        //указатель на функцию с алгоритмом рисования
        protected DataRenderDelegate DataRender { get; set; }
    }
}
