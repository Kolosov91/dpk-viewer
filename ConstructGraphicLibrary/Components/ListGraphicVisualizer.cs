using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConstructGraphicLibrary.BaseTools;
using ConstructGraphicLibrary.Data;
using System.Windows.Media;
using ConstructGraphicLibrary.StaticConstructors.RenderedGeometry;
using ConstructGraphicLibrary.StaticConstructors;
using System.Windows;
using System.Threading;
using System.Windows.Threading;

namespace ConstructGraphicLibrary.Components
{
    /// <summary>
    /// "Визуализатор графиков" (в виде списка, последовательно)
    /// </summary>
    public class ListGraphicVisualizer : BaseVisualComponent, ICommonData
    {
        /// <summary>
        /// Ссылка на общие данные
        /// </summary>
        public CommonData CommonData { get; set; }
        /// <summary>
        /// признак аппроксимации
        /// </summary>
        public bool IsAppearApproximation { get; protected set; }
        /// <summary>
        /// Конструктор
        /// </summary>
        public ListGraphicVisualizer()
        {
            this.UpdateDataProc = this.UpdateData;
            this.RenderProc = this.Render;
            IsAppearApproximation = false;
            this.ClipFlag = false;
        }
        /// <summary>
        /// Рендеринг графиков
        /// </summary>
        /// <param name="dc">контекст рисования</param>
        protected void Render(DrawingContext dc)
        {         
            Rect graphicRect = new Rect(this.Field.Location, new Size(this.Field.Width, HeightGraphic));
            for (int i = 0; i < CommonData.RenderedGraphics.Count; i++)
            {
                dc.PushOpacity(0.5);
                dc.DrawRoundedRectangle(
                    Macros.ChooseBrush(i, 
                    new LinearGradientBrush(
                    new GradientStopCollection() { new GradientStop(Colors.LightSteelBlue, 0.5), new GradientStop(Colors.LightSlateGray, 1) },
                    new Point(0, 0), new Point(0, 1)),
                    CommonData.IndexActiveGraphic,
                    new LinearGradientBrush(
                    new GradientStopCollection() { new GradientStop(Colors.LightSlateGray, 0.5), new GradientStop(Colors.LightYellow, 1) },
                    new Point(0, 0), new Point(0, 1))), 
                    null,
                    new Rect(new Point(graphicRect.X, graphicRect.Y - 4), new Size(graphicRect.Width, graphicRect.Height + 8)), 5, 5);
                ToolFunctions.DrawAutoTxt(dc, CommonData.SourceGraphics[i].Name,
                    new Rect(new Point(graphicRect.X - CommonData.Width_LeftBorder, graphicRect.Y), new Size(CommonData.Width_LeftBorder,
                        (HeightGraphic < HeightBetweenGraphics) ? HeightBetweenGraphics : HeightGraphic)),
                    Macros.ChooseBrush(i, Brushes.Black, CommonData.IndexActiveGraphic, Brushes.DarkBlue), TextAlignment.Center, 1.95, "Cambria");
                dc.Pop();
                DrawGraphic(dc, CommonData.RenderedGraphics[i], Macros.ChooseBrush(i, Brushes.Blue, CommonData.IndexActiveGraphic, Brushes.DarkBlue), graphicRect);               
                graphicRect.Y += (HeightBetweenGraphics + HeightGraphic);             
            }                
        }
        /// <summary>
        /// Отрисовка графика
        /// </summary>
        /// <param name="dc">контекст рисования</param>
        /// <param name="renderedGraphic"></param>
        protected void DrawGraphic(DrawingContext dc, GeometryGraphic renderedGraphic, Brush lineBrush, Rect rect)
        {   
            PathGeometry graphic;
            graphic = GeometryLinesConstructor.ConstructGeometry(renderedGraphic, rect);
            dc.DrawGeometry(null, new Pen(lineBrush, 0.5), graphic);
            graphic = GeometryPointsConstructor.ConstructGeometry(renderedGraphic, rect, CommonData.LeftTime, CommonData.RightTime);
            dc.DrawGeometry(null, new Pen(Brushes.BlueViolet, 1.0), graphic);
            DrawValuePoint(dc, renderedGraphic);
        }
        /// <summary>
        /// Рисование значения в точке
        /// </summary>
        /// <param name="dc">контекст рисования</param>
        /// <param name="renderedGraphic"></param>
        protected void DrawValuePoint(DrawingContext dc, GeometryGraphic renderedGraphic)
        {
            {
                dc.PushOpacity(0.5);
                Rect valueRect = new Rect(new Point(renderedGraphic.Points[0].Position.X + 2, renderedGraphic.Points[0].Position.Y+2), 
                    new Size(renderedGraphic.Points[0].SourcePoint.Value.ToString().Length * 5 + 10, 13));
                dc.DrawRoundedRectangle(Brushes.WhiteSmoke, new Pen(Brushes.White, 1), valueRect, valueRect.Height / 5, valueRect.Height / 5);
                ToolFunctions.DrawAutoTxt(dc, "0x" + renderedGraphic.Points[0].SourcePoint.Value.ToString("X"), valueRect, Brushes.Brown, TextAlignment.Center, 1.95);
                //
                valueRect = new Rect(new Point(renderedGraphic.Points.Last().Position.X + 2, renderedGraphic.Points.Last().Position.Y+2), 
                    new Size(renderedGraphic.Points.Last().SourcePoint.Value.ToString().Length * 5 + 10, 13));
                dc.DrawRoundedRectangle(Brushes.WhiteSmoke, new Pen(Brushes.White, 1), valueRect, valueRect.Height / 5, valueRect.Height / 5);
                ToolFunctions.DrawAutoTxt(dc, "0x" + renderedGraphic.Points.Last().SourcePoint.Value.ToString("X"), valueRect, Brushes.Brown, TextAlignment.Center, 1.95);
                dc.Pop();
            }
        }
        public double HeightBetweenGraphics { get; protected set; }
        public double HeightGraphic { get; protected set; }
        /// <summary>
        /// построение видимых участков графика и определение наличия аппроксимации
        /// </summary>
        protected void UpdateData()
        {
            CommonData.RenderedGraphics.Clear();
            IsAppearApproximation = false;
            CalculationHeightGraphic();
            Point locGraphic = this.Field.Location;
            foreach (SourceGraphic srcGraphic in CommonData.SourceGraphics)
            {
                bool flagApproximation = false;
                GeometryGraphic renderedGraphic = GeometryGraphicConstructor.Construct(srcGraphic,
                    new Rect(locGraphic, new Size(this.Field.Width, HeightGraphic)), CommonData.TimeInPoint,
                    CommonData.LeftTime, CommonData.RightTime);
                GeometryGraphic renderedGraphicApproximated = GeometryGraphicConstructor.Approximation(renderedGraphic, ref flagApproximation, 5);
                //GeometryGraphic renderedGraphicApproximated = GeometryGraphicConstructor.ConstructWithApproximation(srcGraphic,
                //    new Rect(locGraphic, new Size(this.Field.Width, HeightGraphic)), CommonData.TimeInPoint, 
                //    CommonData.LeftTime, CommonData.RightTime, 5, out flagApproximation);
                if (flagApproximation) IsAppearApproximation = true;
                CommonData.RenderedGraphics.Add(renderedGraphicApproximated);
                locGraphic.Y += (HeightGraphic + HeightBetweenGraphics);
            }
        }
/***************************************************************************************************/
        //List<Point> locs;
        //List<Thread> threads;
        //protected void UpdateData()
        //{
        //    int count = CommonData.SourceGraphics.Count;
        //    if (count == 0) return;
        //    CalculationHeightGraphic();
        //    locs = new List<Point>(count);

        //    threads = new List<Thread>(count);
        //    Point locGraphic = this.Field.Location;
        //    CommonData.RenderedGraphics.Clear();
        //    for (int i = 0; i < count; i++)
        //    {
        //        threads.Add(new Thread(RenderGraphic));
        //        locs.Add(locGraphic);
        //        locGraphic.Y += (HeightGraphic + HeightBetweenGraphics);
        //        threads[i].Start(i);
        //    }
        //    /*Проверка потоков на их работу*/
        //    bool IsRenderingYet = false;
        //    do
        //    {
        //        IsRenderingYet = false;
        //        for (int i = 0; i < count; i++)
        //        {
        //            if (threads[i].ThreadState == ThreadState.Running)
        //                IsRenderingYet = true;
        //        }
        //    }
        //    while (IsRenderingYet);
        //}

        //private void RenderGraphic(object obj)
        //{
        //    bool flagApproximation = false;
        //    GeometryGraphic renderedGraphic = GeometryGraphicConstructor.Construct(CommonData.SourceGraphics[(int)obj],
        //        new Rect(locs[(int)obj], new Size(this.Field.Width, HeightGraphic)), CommonData.TimeInPoint,
        //        CommonData.LeftTime, CommonData.RightTime);
        //    GeometryGraphic renderedGraphicApproximated = GeometryGraphicConstructor.Approximation(renderedGraphic, ref flagApproximation, 5);
        //    if (flagApproximation)
        //    {
        //        Dispatcher.CurrentDispatcher.Invoke((ThreadStart)delegate()
        //        {
        //            IsAppearApproximation = true;
        //        }, null);
        //    }
        //    Dispatcher.CurrentDispatcher.Invoke((ThreadStart)delegate()
        //    {
        //        CommonData.RenderedGraphics.Add(renderedGraphicApproximated);
        //    }, null);
        //}
        /// <summary>
        /// Вычисление Высоты графика и интервала между графиками
        /// </summary>
        protected void CalculationHeightGraphic()
        {
            switch (CommonData.SourceGraphics.Count)
            {
                case 0:
                case 1:
                    HeightBetweenGraphics = 0;
                    HeightGraphic = this.Field.Height;
                    break;
                default:
                    HeightBetweenGraphics = 30.0;
                    HeightGraphic = (this.Field.Height - HeightBetweenGraphics * ((double)(CommonData.SourceGraphics.Count - 1))) / (double)CommonData.SourceGraphics.Count;
                    if (HeightGraphic < 0)
                    {
                        HeightBetweenGraphics = 5;
                        HeightGraphic = (this.Field.Height - HeightBetweenGraphics * ((double)(CommonData.SourceGraphics.Count - 1))) / (double)CommonData.SourceGraphics.Count;
                    }
                    if (HeightGraphic < 0)
                    {
                        HeightBetweenGraphics = 0;
                        HeightGraphic = (this.Field.Height - HeightBetweenGraphics * ((double)(CommonData.SourceGraphics.Count - 1))) / (double)CommonData.SourceGraphics.Count;
                    }
                    if (HeightGraphic < 0)
                    {
                        HeightBetweenGraphics = 0;
                        HeightGraphic = 1;
                    }
                    break;
            }           
        }
    }
}
