using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConstructGraphicLibrary.BaseTools;
using ConstructGraphicLibrary.Data;
using System.Windows.Media;
using System.Windows;

namespace ConstructGraphicLibrary.Components
{
    /// <summary>
    /// Компонент "Временные метки"
    /// (Рендерит метки, точки пересечения с графиком и значения в них)
    /// </summary>
    public class TimeMarksScale : BaseVisualComponent, ICommonData
    {
        /// <summary>
        /// Ссылка на общие данные, необходимые для рендеринга
        /// </summary>
        public CommonData CommonData { get; set; }
        /// <summary>
        /// Список меток
        /// </summary>
        public List<TimeMark> Marks;
        /// <summary>
        /// Индекс выбранной текущей метки (выделенной)
        /// </summary>
        public int IndexTimeMark;
        /*----------------------------------------*/
        /// <summary>
        /// Конструктор
        /// </summary>
        public TimeMarksScale()
        {
            this.UpdateDataProc = this.UpdateData;
            this.RenderProc = this.Render;
            this.ClipFlag = false;
            Marks = new List<TimeMark>();
            IndexTimeMark = -1;
        }
        /*________________________________________*/
        /// <summary>
        /// Точки пересечения линии метки с графиками
        /// </summary>
        List<Point> CrossPoints = new List<Point>();
        /// <summary>
        /// Значения в точках пересечения
        /// </summary>
        List<int> Values = new List<int>();
        /*----------------------------------------*/
        /// <summary>
        /// построение списка точек пересечения и списка значений в них
        /// </summary>
        protected void UpdateData()
        {
            CrossPoints.Clear();
            Values.Clear();
            for (int i = Marks.Count - 1; i >= 0; i--)
                if (Macros.CheckEntryTime(CommonData.LeftTime, Marks[i].Time, CommonData.RightTime))
                {
                    double X = this.Field.X + ToolFunctions.GetDxByTime(Marks[i].Time, CommonData.TimeInPoint, CommonData.LeftTime);
                    foreach (GeometryGraphic geomGr in CommonData.RenderedGraphics)
                    {
                        int cnt = geomGr.Points.Count;
                        for(int j=0; j<cnt-1; j++)
                            if ((X >= geomGr.Points[j].X) && (X < geomGr.Points[j+1].X))
                            {
                                CrossPoints.Add(new Point(X, geomGr.Points[j].Y));
                                Values.Add(geomGr.Points[j].SourcePoint.Value);
                                break;
                            }
                    }
                }
        }
        /*________________________________________*/
        /// <summary>
        /// Рендеринг меток, точек пересечений и значений в них
        /// </summary>
        /// <param name="dc">Контекст рисования</param>
        protected void Render(DrawingContext dc)
        {
            dc.PushOpacity(0.7);
            /*Отрисовка меток*/
            for (int i = Marks.Count - 1; i >= 0; i--)
                if (Macros.CheckEntryTime(CommonData.LeftTime, Marks[i].Time, CommonData.RightTime))
                {               
                    Point MarkPoint = new Point(this.Field.X + ToolFunctions.GetDxByTime(Marks[i].Time, CommonData.TimeInPoint, CommonData.LeftTime), this.Field.Y);
                    dc.DrawLine(new Pen(Macros.ChooseBrush(i, Brushes.DarkGreen, IndexTimeMark, Brushes.Orange), 1), MarkPoint, new Point(MarkPoint.X, this.Field.Bottom));
                    Rect valueRect = new Rect(new Point(MarkPoint.X + 2, MarkPoint.Y + 2), new Size(6 * 5, 26));
                    dc.DrawRoundedRectangle(
                        Macros.ChooseBrush(i, new LinearGradientBrush(Colors.DarkGreen, Colors.LightGreen, 0.5), 
                        IndexTimeMark, new LinearGradientBrush(Colors.Orange, Colors.OrangeRed, 0.5)),
                        new Pen(Macros.ChooseBrush(i, Brushes.DarkGreen, IndexTimeMark, Brushes.OrangeRed), 1), 
                        valueRect, valueRect.Height / 5, valueRect.Height / 5);
                    ToolFunctions.DrawAutoTxt(dc, Macros.TimeSpanToString(Marks[i].Time), valueRect, Brushes.White, TextAlignment.Justify, 1.95);
                }
            /*Отрисовка значений в точке пересечения линии метки и графика*/
            for (int i = CrossPoints.Count - 1; i >= 0; i--)
            {
                dc.DrawGeometry(Brushes.Brown, new Pen(Brushes.Brown, 1), new EllipseGeometry(CrossPoints[i], 1, 1));
                Rect valueRect = new Rect(new Point(CrossPoints[i].X + 2, CrossPoints[i].Y + 2), new Size(Values[i].ToString().Length * 5 + 10, 13));
                dc.DrawRoundedRectangle(Brushes.WhiteSmoke, new Pen(Brushes.White, 1), valueRect, valueRect.Height / 5, valueRect.Height / 5);
                ToolFunctions.DrawAutoTxt(dc, "0x" + Values[i].ToString("X"), valueRect, Brushes.Brown, TextAlignment.Center, 1.95);
            }
            dc.Pop();
        }
    }
}
