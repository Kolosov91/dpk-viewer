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
using ListBlockViewLib.BlockTemplate;

namespace ListBlockViewLib
{
    /// <summary>
    /// Логика взаимодействия для ListBlockView.xaml
    /// </summary>
    public partial class ListBlockView : UserControl
    {
        /// <summary>
        /// Список объектов, которые являются исходными данными (реализуем требование "неоднородности элементов")
        /// </summary>
        public List<object> SourceData { get; set; }

        protected Brush colorChoosenBlockShadow;
        /// <summary>
        /// Цвет затенения выбранного элемента (по которому "кликнули")
        /// </summary>
        public Brush ColorChoosenBlockShadow { get { return colorChoosenBlockShadow; } set { colorChoosenBlockShadow = value; this.InvalidateVisual(); } }

        public delegate BlockTemplateBase ConvertObjectToBlockTemplateDelegate(object item, int index);
        /// <summary>
        /// Указатель на функцию, в которой будет описан пользовательский алгоритм для отображения (рисвоания) данных,
        /// т.е. управление "раскраской" вида элемента на основе их значений
        /// (требование о "раскраске" элементов)
        /// (Далее будет подробнее о BlockTemplateBase)
        /// </summary>
        public ConvertObjectToBlockTemplateDelegate ConvertToBlockTemplate { get; set; }
        
        /// <summary>
        /// Список областей (прямоугольников), в которых отрисованы видымые элементы списка SourceData,
        /// начиная с индекса IndexCurrentFirstVisibleBlock
        /// (необходимо для реализации клика по элементу)
        /// </summary>
        protected List<Rect> CurrentVisibleListBlockRect { get; set; }
        /// <summary>
        /// Индекс первого видимого элемента (необходимо для реализации вертикального скроллнига)
        /// </summary>
        protected int IndexCurrentFirstVisibleBlock { get; set; }
        /// <summary>
        /// Индекс текущего выбранного элемента (который будет "затеняться")
        /// </summary>
        public int IndexCurrentChoosenBlock { get; protected set; }

        /* Маршрутизируемое событие "Клик по элементу"
        public class ClickItemRoutedEventArgs : RoutedEventArgs
        {
            public int Index { get; protected set; }
            public object Item { get; protected set; }
            public ClickItemRoutedEventArgs(RoutedEvent routedEvent, object item, int index)
                : base(routedEvent)
            {
                Item = item;
                Index = index;
            }
            public ClickItemRoutedEventArgs()
                : base()
            {
                Item = null;
                Index = -1;
            }

        }
        public static readonly RoutedEvent ClickItemEvent = EventManager.RegisterRoutedEvent("ClickItem", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ListBlockView));
        public event RoutedEventHandler ClickItem
        {
            add { base.AddHandler(ClickItemEvent, value); }
            remove { base.RemoveHandler(ClickItemEvent, value); }
        }
        void RaiseClickItem(object item, int index)
        {
            ClickItemRoutedEventArgs args = new ClickItemRoutedEventArgs(ClickItemEvent, item, index);
            RaiseEvent(args);
        }
         * */

        public class ClickDataItemEventArgs : EventArgs
        {
            public object Item { get; protected set; }
            public int Index { get; protected set; }
            public MouseButtonEventArgs MouseEventArg { get; protected set; }
            public ClickDataItemEventArgs() : base() { Item = null; Index = -1; MouseEventArg = null; }
            public ClickDataItemEventArgs(object item, int index, MouseButtonEventArgs arg) : base() { Item = item; Index = index; MouseEventArg = arg; }
        }
        /// <summary>
        /// Событие клика по элементу
        /// </summary>
        public event EventHandler<ClickDataItemEventArgs> ClickItem;
        protected void ClickItemRaiseEvent(object item, int index, MouseButtonEventArgs arg)
        {
            if (ClickItem != null)
                ClickItem(this, new ClickDataItemEventArgs(item, index, arg));
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            //обходим список с областями рендеринга компонента
            for (int i = this.CurrentVisibleListBlockRect.Count - 1; i >= 0; i--)
            {
                //если курсор в области элемента, то выбираем его
                if (this.CurrentVisibleListBlockRect[i].Contains(e.GetPosition(this)))
                {
                    IndexCurrentChoosenBlock = i + IndexCurrentFirstVisibleBlock;
                    this.InvalidateVisual();
                    this.ClickItemRaiseEvent(this.SourceData[IndexCurrentChoosenBlock], IndexCurrentChoosenBlock, e);
                }
            }
        }
        /// <summary>
        /// Программный выбор элемента по индексу и генерация события клика по нему
        /// </summary>
        /// <param name="index"> индекс элемента </param>
        public void Select(int index)
        {
            int tempIndex = index;
            if (this.SourceData.Count.Equals(0)) return;
            if ((index < 0) && (index >= this.SourceData.Count))
                tempIndex = 0;
            this.IndexCurrentChoosenBlock = tempIndex;
            this.IndexCurrentFirstVisibleBlock = tempIndex;
            this.InvalidateVisual();
            ClickItemRaiseEvent(this.SourceData[this.IndexCurrentChoosenBlock], this.IndexCurrentChoosenBlock, null);
        }
        
        /// <summary>
        /// Добавить элемент в список
        /// </summary>
        /// <param name="item">элемент</param>
        public void AddElementToSourceData(object item) { SourceData.Add(item); this.vScrollBar.Minimum = 0; this.vScrollBar.Maximum = this.SourceData.Count - 1; /*this.vScrollBar.Value = this.vScrollBar.Maximum;*/ this.InvalidateVisual(); }
        /// <summary>
        /// Добавить список элементов в SourceData
        /// </summary>
        /// <param name="listItem">список элементов</param>
        public void AddRangeToSourceData(List<object> listItem) { this.vScrollBar.Minimum = 0; SourceData.AddRange(listItem); this.vScrollBar.Maximum = this.SourceData.Count - 1; /*this.vScrollBar.Value = this.vScrollBar.Maximum;*/ this.InvalidateVisual(); }
        /// <summary>
        /// Очистить SourceData
        /// </summary>
        public void ClearSourceData() { SourceData.Clear(); this.vScrollBar.Maximum = this.SourceData.Count - 1; this.vScrollBar.Value = this.vScrollBar.Maximum; this.InvalidateVisual(); }
        
        public ListBlockView()
        {
            SourceData = new List<object>();
            colorChoosenBlockShadow = Brushes.Black;
            CurrentVisibleListBlockRect = new List<Rect>();
            IndexCurrentFirstVisibleBlock = 0;
            IndexCurrentChoosenBlock = -1;
            InitializeComponent();
            this.vScrollBar.ValueChanged += new RoutedPropertyChangedEventHandler<double>(vScrollBar_ValueChanged);
        }

        /// <summary>
        /// вертикальный скроллинг
        /// </summary>
        private void vScrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.IndexCurrentFirstVisibleBlock = (int)e.NewValue;
            this.InvalidateVisual();
        }
        /// <summary>
        /// Алгоритм отрисовки компонента
        /// </summary>
        /// <param name="drawingContext">контекст рисования</param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            //Текущая фактическая высота области рисования
            double currentHeight = this.RenderSize.Height - this.hScrollBar.RenderSize.Height;
            //Текущая фактическая ширина области рисования
            double currentWidth = this.RenderSize.Width - this.vScrollBar.RenderSize.Width;
            //Область рисования (вне этой области рисовать нельзя)
            Size clipSize = new Size(currentWidth, currentHeight);
            //ограничиваем
            drawingContext.PushClip(new RectangleGeometry(new Rect(new Point(0, 0), clipSize)));
            if (this.SourceData.Count <= 0) return;
            //текущий индекс рисуемого элемента (блока)
            int tempIndex = this.IndexCurrentFirstVisibleBlock;
            //текущая точка рисования на канвасе компонента
            Point currentBlockRenderLocation = new Point(0,0);
            //учёт горизонтального скроллинга (если ползунок передвинут) (в случае когда не помещается полностью элемент)
            currentBlockRenderLocation.X = - this.hScrollBar.Value;
            this.hScrollBar.Maximum = 0;
            //очистка Списка областей (прямоугольников), в которых отрисованы видымые элементы списка SourceData
            this.CurrentVisibleListBlockRect.Clear();
            //рисуем блоки (элементы) пока они видны на экране (канвасе компонента)
            while (currentBlockRenderLocation.Y < currentHeight)
            {
                if (this.ConvertToBlockTemplate == null) return;
                if (tempIndex >= this.SourceData.Count) return;
                
                //преобразоваем элемент пользовательского типа в универсальный шаблон отображения
                //данную функцию описывает пользователь компонента
                BlockTemplateBase currentRenderedBlock = this.ConvertToBlockTemplate(this.SourceData[tempIndex], tempIndex);
                
                //рендерим шаблон
                DrawingVisual currentBlockBuffer =  currentRenderedBlock.Render(currentBlockRenderLocation);
                //рисуем его на канвасе компонента
                drawingContext.DrawDrawing(currentBlockBuffer.Drawing);

                //область рисования текущего шаблона
                Rect currentBlockRect = new Rect(currentBlockRenderLocation, currentRenderedBlock.RenderSize);
                //добавляем его в список (пригодится для реализации клика по элементу)
                this.CurrentVisibleListBlockRect.Add(currentBlockRect);

                //подкрашиваем выбранный элемент
                if (this.IndexCurrentChoosenBlock.Equals(tempIndex))
                {
                    drawingContext.PushOpacity(0.5);
                    drawingContext.DrawRectangle(this.ColorChoosenBlockShadow, null, currentBlockRect);
                    drawingContext.Pop();
                }

                //выбираем самую длинную ширину (самы длинный элемент) (для реализации горизонтального скроллинга)
                double deltaWidth = currentRenderedBlock.RenderSize.Width - currentWidth;
                if (deltaWidth > 0)
                    if (this.hScrollBar.Maximum <= deltaWidth) { this.hScrollBar.Maximum = deltaWidth; }

                //переходим вниз, на свободное место для рисования
                currentBlockRenderLocation.Y += currentRenderedBlock.RenderSize.Height;
                tempIndex++;
            }
        }
        /// <summary>
        /// горизонтальный скроллинг
        /// </summary>
        private void hScrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.InvalidateVisual();
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);
            int oldVal = (int)vScrollBar.Value;
            int DELTA = e.Delta;
            if (DELTA < 0)
            {
                if ((vScrollBar.Value + vScrollBar.LargeChange) >= vScrollBar.Maximum)
                {
                    vScrollBar.Value = vScrollBar.Maximum;
                    return;
                }
                vScrollBar.Value += vScrollBar.LargeChange;
            }
            else
            {
                if ((vScrollBar.Value - vScrollBar.LargeChange) <= vScrollBar.Minimum)
                {
                    vScrollBar.Value = vScrollBar.Minimum;
                    return;
                }
                vScrollBar.Value -= vScrollBar.LargeChange;
            }
        }
    }
}
