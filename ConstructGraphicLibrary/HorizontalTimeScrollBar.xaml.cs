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
using ConstructGraphicLibrary.BaseTools;
using System.ComponentModel;

namespace ConstructGraphicLibrary
{
    /// <summary>
    /// Логика взаимодействия для HorizontalTimeScrollBar.xaml
    /// </summary>
    public partial class HorizontalTimeScrollBar : UserControl, INotifyPropertyChanged
    {
        HorizontalTimeScrollBarData scrollBarData = new HorizontalTimeScrollBarData();
        /// <summary>
        /// Левая временная граница
        /// </summary>
        public TimeSpan LeftTimeBorder { get { return scrollBarData.LeftTimeBorder; } 
            set { 
                scrollBarData.LeftTimeBorder = value; 
                RaiseChangeTimeInPointEvent();
                InvalidateVisual();
            }
        }
        /// <summary>
        /// Правая временная граница
        /// </summary>
        public TimeSpan RightTimeBorder { get { return scrollBarData.RightTimeBorder; } 
            set { 
                scrollBarData.RightTimeBorder = value;
                RaiseChangeTimeInPointEvent();
                InvalidateVisual();
            }
        }
        /// <summary>
        /// Левая временная граница видимого интервала
        /// </summary>
        public TimeSpan VisibilityLeftTimeBorder { get { return scrollBarData.CurrentLeftTime; } 
            set { 
                scrollBarData.CurrentLeftTime = value;
                InvalidateVisual();
                RaiseScrolledEvent(this.scrollBarData);
                NotifyPropertyChanged("VisibilityLeftTimeBorder");
            }
        }
        /// <summary>
        /// Правая временная граница видимого интервала
        /// </summary>
        public TimeSpan VisibilityRightTimeBorder { get { return scrollBarData.CurrentRightTime; } }
        /// <summary>
        /// Видимый временной интервал
        /// </summary>
        public TimeSpan VisibilityTimeInterval
        {
            get { return scrollBarData.VisibilityTimeInterval; }
            set
            {
                scrollBarData.VisibilityTimeInterval = value;
                if (scrollBarData.CurrentLeftTime.Add(scrollBarData.VisibilityTimeInterval).CompareTo(scrollBarData.RightTimeBorder) > 0)
                    scrollBarData.VisibilityTimeInterval = scrollBarData.RightTimeBorder.Subtract(scrollBarData.CurrentLeftTime);
                InvalidateVisual();
                RaiseScrolledEvent(this.scrollBarData);
                NotifyPropertyChanged("VisibilityTimeInterval");
                NotifyPropertyChanged("VisibilityRightTimeBorder");
            }
        }

        /// <summary>
        /// Коэффициент TimeInPoint (необходим для рендеринга)
        /// (связывает экранные координаты и значения времени)
        /// </summary>
        double TimeInPoint = 0;
        /// <summary>
        /// Событие пересчёта размеров компонентов
        /// </summary>
        event EventHandler ChangeTimeInPointEvent;
        void RaiseChangeTimeInPointEvent()
        {
            if (ChangeTimeInPointEvent != null)
                ChangeTimeInPointEvent(this, EventArgs.Empty);
        }
        /// <summary>
        /// Расстояние между временными линиями в пикселях
        /// </summary>
        double dxTimeLine { get; set; }
        /// <summary>
        /// Размер шрифта
        /// </summary>
        double fontSize { get; set; }
        /// <summary>
        /// Конструктор
        /// </summary>
        public HorizontalTimeScrollBar()
        {
            InitializeComponent();
            ChangeTimeInPointEvent += new EventHandler(HorizontalTimeScrollBar_ChangeTimeInPointEvent);
            dxTimeLine = 80;
            fontSize = 10;
        }
        /// <summary>
        /// Расчёт коэффициента TimeInPoint
        /// </summary>
        void HorizontalTimeScrollBar_ChangeTimeInPointEvent(object sender, EventArgs e)
        {
            TimeInPoint = ToolFunctions.GetTimeInPt(btScrollLeft.Width, RenderSize.Width - btScrollLeft.Width - btScrollRight.Width, scrollBarData.LeftTimeBorder, scrollBarData.RightTimeBorder);
        }
        /// <summary>
        /// Обработка изменений размера
        /// </summary>
        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            RaiseChangeTimeInPointEvent();
            InvalidateVisual();
        }
        /// <summary>
        /// Рендеринг временной шкалы и прямоугольника выбранного интервала
        /// </summary>
        /// <param name="drawingContext">контекст рисования</param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            Size szCellTxt = new Size(6 * (fontSize / 2), RenderSize.Height);
            Rect rect = new Rect(new Point(btScrollLeft.Width, 0), new Size(RenderSize.Width - btScrollLeft.Width - btScrollRight.Width, RenderSize.Height));
            drawingContext.DrawRoundedRectangle(new LinearGradientBrush(new GradientStopCollection() { new GradientStop(Colors.LightGray, 0), new GradientStop(Colors.WhiteSmoke, 0.5), new GradientStop(Colors.LightGray, 1) },
                    new Point(0, 0), new Point(0, 1)), new Pen(Brushes.DarkGray, 1), new Rect(new Point(0, 0), RenderSize),5,5);
            drawingContext.PushClip(new RectangleGeometry(rect));    
            /*Левая временная полоса*/
            ToolFunctions.DrawVTimeLine(drawingContext, rect.Location, szCellTxt,
                ToolFunctions.TimeSpanToString(ToolFunctions.GetTimeByPoint(rect.Location, rect.Location, TimeInPoint, LeftTimeBorder)), rect.Height, true, fontSize);
            /*Правая временная полоса*/
            ToolFunctions.DrawVTimeLine(drawingContext, new Point(rect.X + rect.Width, rect.Y), szCellTxt,
                ToolFunctions.TimeSpanToString(ToolFunctions.GetTimeByPoint(rect.Location, rect.Location, TimeInPoint, RightTimeBorder)), rect.Height, false, fontSize);
            /*количество временных линий*/
            double cnt = (int)(rect.Width / dxTimeLine);
            double wSmallInterval = rect.Width / cnt;
            Point linePt = new Point(rect.X + wSmallInterval, rect.Y);
            /*отрисовка временных линий*/
            for (int i = 0; i < (int)cnt - 1; i++)
            {
                ToolFunctions.DrawVTimeLine(drawingContext, linePt, szCellTxt,
                    ToolFunctions.TimeSpanToString(ToolFunctions.GetTimeByPoint(linePt, rect.Location, TimeInPoint, LeftTimeBorder)), rect.Height, true, fontSize);
                linePt.X += wSmallInterval;
            }
            drawingContext.PushOpacity(0.5);
            Rect visibleIntervalRect = new Rect(
            new Point(rect.X + ToolFunctions.GetDxByTime(VisibilityLeftTimeBorder, TimeInPoint, LeftTimeBorder), 0),
            new Size(ToolFunctions.GetDxByTime(VisibilityRightTimeBorder, TimeInPoint, LeftTimeBorder) - ToolFunctions.GetDxByTime(VisibilityLeftTimeBorder, TimeInPoint, LeftTimeBorder),
            rect.Height));
            drawingContext.DrawRoundedRectangle(Brushes.LightCoral, new Pen(Brushes.DarkRed, 1), visibleIntervalRect, 5,5);
        }

        /// <summary>
        /// внутренний флаг состояния скроллинга
        /// </summary>
        bool ScrollingFlag { get; set; }
        /// <summary>
        /// Событие, возникающее после скроллинга (уже с новыми данными)
        /// </summary>
        public event EventHandler<EventArgs<HorizontalTimeScrollBarData>> ScrolledEvent;
        /// <summary>
        /// Генератор события скроллинга
        /// </summary>
        /// <param name="data">Данные о временных границах</param>
        void RaiseScrolledEvent(HorizontalTimeScrollBarData data)
        {
            if (ScrolledEvent != null)
                ScrolledEvent(this, new EventArgs<HorizontalTimeScrollBarData>(data));
        }
        /// <summary>
        /// Установка видимого интервала (когда кнопка мыши вернулась в состояние Up)
        /// </summary>
        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);
            if (ScrollingFlag)
            {
                Point position = e.GetPosition(this);
                TimeSpan newLeftTime = ToolFunctions.GetTimeByPoint(position, new Point(btScrollLeft.Width, 0), TimeInPoint, LeftTimeBorder);
                if (newLeftTime.CompareTo(LeftTimeBorder) < 0) return;
                if (newLeftTime.CompareTo(RightTimeBorder.Subtract(VisibilityTimeInterval)) > 0) { VisibilityLeftTimeBorder = RightTimeBorder.Subtract(VisibilityTimeInterval); }
                else { VisibilityLeftTimeBorder = newLeftTime; }
                RaiseScrolledEvent(this.scrollBarData);
            }
            ScrollingFlag = false;
        }
        /// <summary>
        /// Передвижение прямоугольника видимого интервала времени
        /// </summary>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (ScrollingFlag)
            {
                Point position = e.GetPosition(this);
                TimeSpan newLeftTime = ToolFunctions.GetTimeByPoint(position, new Point(btScrollLeft.Width, 0), TimeInPoint, LeftTimeBorder);
                if (newLeftTime.CompareTo(LeftTimeBorder) < 0) return;
                if (newLeftTime.CompareTo(RightTimeBorder.Subtract(VisibilityTimeInterval)) > 0) { VisibilityLeftTimeBorder = RightTimeBorder.Subtract(VisibilityTimeInterval); }
                else { VisibilityLeftTimeBorder = newLeftTime; }
                RaiseScrolledEvent(this.scrollBarData);
            }
        }
        /// <summary>
        /// Вход в режим перемещения прямоугольника видимого интервала времени
        /// </summary>
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            ScrollingFlag = true;
        }
        /// <summary>
        /// если мы случайно вышли во время передвижения ползунка и вернулись обратно
        /// </summary>
        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            if (e.LeftButton == MouseButtonState.Released)
                ScrollingFlag = false;
        }
        /// <summary>
        /// Скролл влево
        /// </summary>
        private void btScrollLeft_Click(object sender, RoutedEventArgs e)
        {
            if (VisibilityLeftTimeBorder.CompareTo(LeftTimeBorder) <= 0) { VisibilityLeftTimeBorder = LeftTimeBorder; return; }
            TimeSpan newLeftTime = VisibilityLeftTimeBorder.Subtract(VisibilityTimeInterval);
            if (newLeftTime.CompareTo(LeftTimeBorder) <= 0)
            {
                newLeftTime = LeftTimeBorder;
            }
            VisibilityLeftTimeBorder = newLeftTime;
            RaiseScrolledEvent(this.scrollBarData);
        }
        /// <summary>
        /// Скролл вправо
        /// </summary>
        private void btScrollRight_Click(object sender, RoutedEventArgs e)
        {
            if (VisibilityRightTimeBorder.CompareTo(RightTimeBorder.Subtract(VisibilityTimeInterval)) >= 0)
            { VisibilityLeftTimeBorder = RightTimeBorder.Subtract(VisibilityTimeInterval); return; }
            TimeSpan newLeftTime = VisibilityLeftTimeBorder.Add(VisibilityTimeInterval);
            TimeSpan newRightTime = VisibilityRightTimeBorder.Add(VisibilityTimeInterval);
            if (newRightTime.CompareTo(RightTimeBorder) >= 0)
            {
                newRightTime = RightTimeBorder;
                newLeftTime = VisibilityLeftTimeBorder.Add(newRightTime.Subtract(VisibilityRightTimeBorder));
            }
            VisibilityLeftTimeBorder = newLeftTime;
            RaiseScrolledEvent(this.scrollBarData);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
