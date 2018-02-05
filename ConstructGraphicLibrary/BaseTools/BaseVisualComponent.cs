using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows;

namespace ConstructGraphicLibrary.BaseTools
{
    /// <summary>
    /// Базовый класс элемента визуального компонента
    /// </summary>
    public class BaseVisualComponent
    {
        /// <summary>
        /// конструктор по-умолчанию
        /// </summary>
        public BaseVisualComponent()
        {
            ClipFlag = true;
            Field = new Rect();
            RenderingBuffer = new DrawingVisual();
        }
        /// <summary>
        /// Признак ограничения области рисование (разрешение)
        /// </summary>
        public bool ClipFlag { get; set; }
        /// <summary>
        /// Область элемента
        /// </summary>
        public Rect Field;
        /// <summary>
        /// Визуальный буфер
        /// </summary>
        public DrawingVisual RenderingBuffer { get; protected set; }
        /// <summary>
        /// Процедура рендеринга элемента
        /// </summary>
        public void SelfRender()
        {
            using (DrawingContext dc = RenderingBuffer.RenderOpen())
            {
                if (ClipFlag)
                    dc.PushClip(new RectangleGeometry(Field));//ограничение области рисования
                if (UpdateDataProc != null) UpdateDataProc();//обновление(перерасчёт) исходных данных
                if (RenderProc != null) RenderProc(dc);//рендеринг (перерисовка содержимого компонента)
                dc.Close();
            }
        }
        /// <summary>
        /// делегат процедуры рендеринга наследника
        /// </summary>
        /// <param name="dc">контекст рисования</param>
        protected delegate void RenderDelegate(DrawingContext dc);
        /// <summary>
        /// ссылка на процедуру рисования наследника
        /// </summary>
        protected RenderDelegate RenderProc { get; set; }
        /// <summary>
        /// Делегат процедуры пересчёта (обновления) данных
        /// </summary>
        protected delegate void UpdateDataDelegate();
        /// <summary>
        /// Ссылка на процедуру пересчёта (обновления) данных
        /// </summary>
        protected UpdateDataDelegate UpdateDataProc { get; set; }
    }
}
