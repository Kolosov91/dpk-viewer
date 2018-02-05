using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ConstructGraphicLibrary.Data;
using ConstructGraphicLibrary.Components;
using ConstructGraphicLibrary.BaseTools;

namespace ConstructGraphicLibrary
{
    /// <summary>
    /// Список графиков, отображаемых сверху-вниз с автомасштабированием
    /// </summary>
    public partial class AutoScaleListGraphic : UserControl
    {
        /// <summary>
        /// Общие данные
        /// </summary>
        public CommonData CommonData { get; protected set; }
        /// <summary>
        /// "Временная шкала"
        /// </summary>
        TimeScale timeScale { get; set; }
        /// <summary>
        /// "Визуализатор графиков"
        /// </summary>
        ListGraphicVisualizer graphicVisualizer { get; set; }
        /// <summary>
        /// "Временные метки"
        /// </summary>
        TimeMarksScale timeMarksScale { get; set; }
        /// <summary>
        /// исходные данные для построения графиков (время-значение)
        /// </summary>
        public List<SourceGraphic> Graphics { get { return graphicVisualizer.CommonData.SourceGraphics; } }
        public int IndexActiveGraphic
        {
            get { return graphicVisualizer.CommonData.IndexActiveGraphic; }
            set {
                graphicVisualizer.CommonData.IndexActiveGraphic = value; 
                this.InvalidateVisual(); }
            }
        public List<TimeMark> Marks { get { return timeMarksScale.Marks; } }
        public int IndexActiveMark
        {
            get { return timeMarksScale.IndexTimeMark; }
            set { timeMarksScale.IndexTimeMark = value;
            this.InvalidateVisual();
            }
        }
        /// <summary>
        /// Левая видимая временная граница
        /// </summary>
        public TimeSpan LeftTime { get { return CommonData.LeftTime; } set { CommonData.LeftTime = value; RaiseRecalculationSizesEvent(); } }
        /// <summary>
        /// правая видимая временная граница
        /// </summary>
        public TimeSpan RightTime { get { return CommonData.RightTime; } set { CommonData.RightTime = value; RaiseRecalculationSizesEvent(); } }
        /// <summary>
        /// Конструктор
        /// </summary>
        public AutoScaleListGraphic()
        {
            InitializeComponent();
            //Инициализация общих данных
            CommonData = new CommonData();
            CommonData.Width_DownBorder = CommonData.Width_UpBorder = CommonData.Width_LeftBorder = CommonData.Width_RightBorder = 50;
            //Создание и инициализация "Временной шкалы"
            timeScale = new TimeScale();
            timeScale.CommonData = CommonData;
            //Создание и инициализация "Визуализатор графиков"
            graphicVisualizer = new ListGraphicVisualizer();
            graphicVisualizer.CommonData = CommonData;
            //Создание и инициализация "Временных меток"
            timeMarksScale = new TimeMarksScale();
            timeMarksScale.CommonData = CommonData;
            //Установка положения Компонентов
            timeScale.Field.Location = new Point(CommonData.Width_LeftBorder, 0);//Положение "Временной шкалы"
            graphicVisualizer.Field.Location = new Point(CommonData.Width_LeftBorder, CommonData.Width_UpBorder);//Положение области "Двумерных графиков"
            timeMarksScale.Field.Location = new Point(CommonData.Width_LeftBorder, 0);//Положение "Временных меток"

            RecalculationSizesEvent += new EventHandler(MultiLayerGraphic_RecalculationSizesEvent);
        }
        /// <summary>
        /// Пересчёт размеров компонентов типа BaseVisualComponent
        /// </summary>
        void MultiLayerGraphic_RecalculationSizesEvent(object sender, EventArgs e)
        {
            if ((RenderSize.Width == 0) || (RenderSize.Height == 0)) return;
            //вычисление коэффициента при каждом измененнии размера компонента
            CommonData.TimeInPoint = ToolFunctions.GetTimeInPt(CommonData.Width_LeftBorder, RenderSize.Width - CommonData.Width_RightBorder,
                CommonData.LeftTime, CommonData.RightTime);
            // пересчёт размеров на основе формул
            timeScale.Field.Size = new Size(RenderSize.Width - CommonData.Width_LeftBorder - CommonData.Width_RightBorder, RenderSize.Height);//формула размера "Временной шкалы"
            graphicVisualizer.Field.Size = new Size(RenderSize.Width - CommonData.Width_LeftBorder - CommonData.Width_RightBorder,
                RenderSize.Height - CommonData.Width_UpBorder - CommonData.Width_DownBorder);//формула размера области "Двумерных графиков"
            timeMarksScale.Field.Size = new Size(RenderSize.Width - CommonData.Width_LeftBorder - CommonData.Width_RightBorder,
                RenderSize.Height);//формула размера области "Временных меток"
            this.InvalidateVisual();//вызов рендеринга
        }
        /// <summary>
        /// Событие пересчёта размеров
        /// </summary>
        event EventHandler RecalculationSizesEvent;
        /// <summary>
        /// Генератор события пересчёта размеров
        /// </summary>
        void RaiseRecalculationSizesEvent()
        {
            if (RecalculationSizesEvent != null)
                RecalculationSizesEvent(this, EventArgs.Empty);
        }
        /// <summary>
        /// Установка списка исходных отображаемых графиков (на исходных точках)
        /// </summary>
        /// <param name="srcGraphics">список графиков</param>
        public void SetSourceGraphics(List<SourceGraphic> srcGraphics)
        {
            graphicVisualizer.CommonData.SourceGraphics.Clear();
            graphicVisualizer.CommonData.SourceGraphics.AddRange(srcGraphics);
            this.InvalidateVisual();
        }
        public void AddSourceGraphic(SourceGraphic graphic)
        {
            graphicVisualizer.CommonData.SourceGraphics.Add(graphic);
            this.InvalidateVisual();
        }
        public void RemoveSourceGraphic(int index)
        {
            if ((index >= 0) && (index < graphicVisualizer.CommonData.SourceGraphics.Count))
            {
                graphicVisualizer.CommonData.SourceGraphics.RemoveAt(index);
                this.InvalidateVisual();
            }
        }
        /// <summary>
        /// Установить видимые границы времени
        /// </summary>
        /// <param name="leftT">левая временная граница</param>
        /// <param name="rightT">правая временная граница</param>
        public void SetTime(TimeSpan leftT, TimeSpan rightT)
        {
            LeftTime = leftT;
            RightTime = rightT;
            RaiseRecalculationSizesEvent();
        }
        /// <summary>
        /// Обработка изменения размеров
        /// </summary>
        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            RaiseRecalculationSizesEvent();
        }
        /// <summary>
        /// Рендеринг "шкалы времени", графиков
        /// </summary>
        /// <param name="drawingContext">контекст рисования</param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            drawingContext.PushClip(new RectangleGeometry(new Rect(new Point(0,0), RenderSize)));
            //Отрисовка фона и границ
            drawingContext.DrawRoundedRectangle(new LinearGradientBrush(new GradientStopCollection() { new GradientStop(Colors.WhiteSmoke, 0.5), new GradientStop(Colors.Gray, 1) },
                    new Point(0, 0), new Point(0, 1)), new Pen(Brushes.DarkGray, 1), new Rect(new Point(0, 0), RenderSize), 5,5);
            /*Рендеринг и вывод "Временной шкалы"*/
            timeScale.SelfRender();//рендеринг
            drawingContext.DrawDrawing(timeScale.RenderingBuffer.Drawing);//вывод
            /*Рендеринг и вывод области "Двумерных графиков"*/
            graphicVisualizer.SelfRender();
            drawingContext.DrawDrawing(graphicVisualizer.RenderingBuffer.Drawing);
            /*Рендеринг и вывод области "Временных меток"*/
            timeMarksScale.SelfRender();
            drawingContext.DrawDrawing(timeMarksScale.RenderingBuffer.Drawing);
            //Аппроксимация
            RaiseApproximationChangedEvent(graphicVisualizer.IsAppearApproximation);
        }
        public void AddTimeMark(TimeMark mark)
        {
            timeMarksScale.Marks.Add(mark);
            this.InvalidateVisual();
        }
        public void RemoveTimeMark(int index)
        {
            if ((index >= 0) && (index < timeMarksScale.Marks.Count))
            {
                timeMarksScale.Marks.RemoveAt(index);
                this.InvalidateVisual();
            }
        }
        public event EventHandler<EventArgs<bool>> ApproximationChangedEvent;
        void RaiseApproximationChangedEvent(bool isApproximation)
        {
            if (ApproximationChangedEvent != null)
                ApproximationChangedEvent(this, new EventArgs<bool>(isApproximation));
        }
    }
}
