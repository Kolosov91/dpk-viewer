using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConstructGraphicLibrary.BaseTools;
using System.Windows.Media;
using System.Windows;
using ConstructGraphicLibrary.Data;

namespace ConstructGraphicLibrary.Components
{
    /// <summary>
    /// Компонент "Шкала времени"
    /// </summary>
    public class TimeScale : BaseVisualComponent, ICommonData
    {
        /// <summary>
        /// расстояние между временными линиями (шкала)
        /// </summary>
        public double dX_TimeLine { get; protected set; }
        /// <summary>
        /// Размер ячейки со значением времени
        /// </summary>
        public Size SizeCellTimeValue { get; protected set; }
        /// <summary>
        /// Ссылка на общие данные, необходимые для рендеринга
        /// </summary>
        public CommonData CommonData { get; set; }
        /// <summary>
        /// Конструктор
        /// </summary>
        public TimeScale()
        {
            this.RenderProc = this.Render;
            dX_TimeLine = 100;
            SizeCellTimeValue = new Size(35, 30);
        }
        /// <summary>
        /// Рендеринг шкалы времени
        /// </summary>
        /// <param name="dc">контекст рисования</param>
        protected void Render(DrawingContext dc)
        {
            dc.PushOpacity(0.6);
            /*Левая временная полоса*/
            ToolFunctions.DrawVTimeLine(dc, this.Field.Location, SizeCellTimeValue,
                ToolFunctions.TimeSpanToString(ToolFunctions.GetTimeByPoint(this.Field.Location, this.Field.Location, CommonData.TimeInPoint, CommonData.LeftTime)), this.Field.Height);
            /*Правая временная полоса*/
            ToolFunctions.DrawVTimeLine(dc, new Point(this.Field.X + this.Field.Width, this.Field.Y), SizeCellTimeValue,
                ToolFunctions.TimeSpanToString(ToolFunctions.GetTimeByPoint(this.Field.Location, this.Field.Location, CommonData.TimeInPoint, CommonData.RightTime)), this.Field.Height, false);
            /*количество временных линий*/
            double cnt = (int)(this.Field.Width / dX_TimeLine);
            double wSmallInterval = this.Field.Width / cnt;
            Point linePt = new Point(this.Field.X + wSmallInterval, this.Field.Y);
            /*отрисовка временных линий*/
            for (int i = 0; i < (int)cnt - 1; i++)
            {
                ToolFunctions.DrawVTimeLine(dc, linePt, SizeCellTimeValue,
                    ToolFunctions.TimeSpanToString(ToolFunctions.GetTimeByPoint(linePt, this.Field.Location, CommonData.TimeInPoint, CommonData.LeftTime)), this.Field.Height);
                linePt.X += wSmallInterval;
            }
            dc.Pop();
        }
    }
}
