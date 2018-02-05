using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConstructGraphicLibrary.BaseTools;
using ConstructGraphicLibrary.Data;
using System.Windows.Media;
using ConstructGraphicLibrary.StaticConstructors;
using ConstructGraphicLibrary.StaticConstructors.RenderedGeometry;
using System.Windows;

namespace ConstructGraphicLibrary.Components
{
    /// <summary>
    /// Компонент "Рисовальщик графиков" (списка графиков - послойно)
    /// </summary>
    public class GraphicVisualizer : BaseVisualComponent, ICommonData
    {
        /// <summary>
        /// Ссылка на общие данные
        /// </summary>
        public CommonData CommonData { get; set; }
        /// <summary>
        /// Флаг наличия аппроксимации графиков
        /// </summary>
        public bool IsAppearApproximation { get; protected set; }
        /// <summary>
        /// Конструктор
        /// </summary>
        public GraphicVisualizer()
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
            dc.DrawRectangle(null, new Pen(Brushes.Gray, 0.25), this.Field);
            for (int i = CommonData.RenderedGraphics.Count - 1; i >= 0; i--)
            {
                if (CommonData.IndexActiveGraphic.Equals(i)) continue;
                DrawGraphic(dc, CommonData.RenderedGraphics[i], Brushes.Blue);
            }
            if ((CommonData.IndexActiveGraphic >= 0) && (CommonData.IndexActiveGraphic < CommonData.RenderedGraphics.Count))
                DrawGraphic(dc, CommonData.RenderedGraphics[CommonData.IndexActiveGraphic], Brushes.Aqua);
        }
        /// <summary>
        /// Отрисовка графика
        /// </summary>
        /// <param name="dc">контекст рисования</param>
        /// <param name="renderedGraphic"></param>
        protected void DrawGraphic(DrawingContext dc, GeometryGraphic renderedGraphic, Brush lineBrush)
        {   
            PathGeometry graphic;
            graphic = GeometryLinesConstructor.ConstructGeometry(renderedGraphic, this.Field);
            dc.DrawGeometry(null, new Pen(lineBrush, 0.7), graphic);
            graphic = GeometryPointsConstructor.ConstructGeometry(renderedGraphic, this.Field, CommonData.LeftTime, CommonData.RightTime);
            dc.DrawGeometry(null, new Pen(Brushes.BlueViolet, 2.0), graphic);
            DrawValuePoint(dc, renderedGraphic);
        }
        /// <summary>
        /// Рисование значения в точке
        /// </summary>
        /// <param name="dc">контекст рисования</param>
        /// <param name="renderedGraphic"></param>
        protected void DrawValuePoint(DrawingContext dc, GeometryGraphic renderedGraphic)
        {
            int countPoints = renderedGraphic.Points.Count;
            bool IsTextUnderPrevPoint = true;
            for (int i = 0; i < countPoints - 1; i++)
            {
                Size cellSize = new Size(renderedGraphic.Points[i].SourcePoint.Value.ToString().Length * 5 + 10, 13);
                IsTextUnderPrevPoint = ToolFunctions.DrawTxtValue(dc, renderedGraphic.Points[i].Position, renderedGraphic.Points[i + 1].Position,
                    renderedGraphic.Points[i].SourcePoint.Value, IsTextUnderPrevPoint, cellSize);
            }
            //Last point
            if (renderedGraphic.Points[countPoints - 1].SourcePoint.Time.CompareTo(CommonData.RightTime) > 0) return;
            Size cellSizeLast = new Size(renderedGraphic.Points[countPoints - 1].SourcePoint.Value.ToString().Length * 5 + 10, 13);
            IsTextUnderPrevPoint = ToolFunctions.DrawTxtValue(dc, renderedGraphic.Points[countPoints - 1].Position, renderedGraphic.Points[countPoints - 1].Position,
                renderedGraphic.Points[countPoints - 1].SourcePoint.Value, IsTextUnderPrevPoint, cellSizeLast);
        }
        /// <summary>
        /// построение видимых участков графика и определение наличия аппроксимации
        /// </summary>
        protected void UpdateData()
        {
            CommonData.RenderedGraphics.Clear();
            IsAppearApproximation = false;
            foreach (SourceGraphic srcGraphic in CommonData.SourceGraphics)
            {
                bool flagApproximation = false;
                GeometryGraphic renderedGraphic = GeometryGraphicConstructor.Construct(srcGraphic, this.Field, CommonData.TimeInPoint,
                    CommonData.LeftTime, CommonData.RightTime);
                GeometryGraphic renderedGraphicApproximated = GeometryGraphicConstructor.Approximation(renderedGraphic, ref flagApproximation, 5);
                if (flagApproximation) IsAppearApproximation = true;
                CommonData.RenderedGraphics.Add(renderedGraphicApproximated);
            }
        }
    }
}
