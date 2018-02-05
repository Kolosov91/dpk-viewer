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
using System.Globalization;
using System.Windows.Controls.Primitives;

namespace ListStringViewWPF
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class ListStringView : UserControl
    {
        /// <summary>
        /// данные для отображения
        /// </summary>
        public List<object> SourceData { get; protected set; }
        /// <summary>
        /// Добавить элемента в SourceData
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
        BlockSizeTemplate blockSize;
        /// <summary>
        /// Разметка блока компанента (размер и количество колонок)
        /// </summary>
        public BlockSizeTemplate BlockSize { get { return blockSize; } set { blockSize = value; this.InvalidateVisual(); } }
        DataListTemplate headerTemplate;
        /// <summary>
        /// Шаблон заголовка
        /// </summary>
        public DataListTemplate HeaderTemplate { get { return headerTemplate; } set { headerTemplate = value; this.InvalidateVisual(); } }
        /// <summary>
        /// Делегат на функцию преобразования
        /// </summary>
        /// <param name="item">элемент SourceData</param>
        /// <param name="indexItem">индекс элемента в списке SourceData</param>
        /// <returns>Шаблон данных блока компанента</returns>
        public delegate DataListTemplate SourceDataElementConvertDelegate(object item, int indexItem);
        /// <summary>
        /// Функция преобразования элемента SourceData в DataTemplate-объект (определяется пользователем)
        /// </summary>
        public SourceDataElementConvertDelegate SourceDataElementConvert { get; set; }
        /// <summary>
        /// Индекс текущего выбранного элемента (затенённый)
        /// </summary>
        public int IndexChoosenElementSourceData { get; protected set; }
        /// <summary>
        /// Индекс первого отображаемого элемента
        /// </summary>
        private int IndexCurrentFirstVisibleElementSourceData { get; set; }
        Brush borderColor;
        /// <summary>
        /// Цвет границ компанента
        /// </summary>
        public Brush BorderColor { get { return borderColor; } set { borderColor = value; this.InvalidateVisual(); } }
        Brush shadowColor;
        /// <summary>
        /// Цвет затенения
        /// </summary>
        public Brush ShadowColor { get { return shadowColor; } set { shadowColor = value; this.InvalidateVisual(); } }

        public ListStringView()
        {
            InitializeComponent();
            SourceData = new List<object>();
            IndexChoosenElementSourceData = 0;
            IndexCurrentFirstVisibleElementSourceData = 0;
            borderColor = Brushes.DarkGray;
            shadowColor = Brushes.Black;
            this.vScrollBar.Minimum = 0;
            this.vScrollBar.Maximum = 0;
            //по-умолчанию
            this.blockSize = new BlockSizeTemplate(20, new List<int>() { 50, 50 });
            this.headerTemplate = new DataListTemplate(new ViewTemplate(Brushes.White, "Arial", 12, Brushes.Black), new List<string>() { "NoNe", "NoNe" });
        }

        void DrawBoldTxt(DrawingContext dc, string str, Rect rect, Brush txtClr, Typeface font, double fntSz)
        {
            FormattedText txt = new FormattedText(str, System.Globalization.CultureInfo.CurrentCulture, System.Windows.FlowDirection.LeftToRight, new Typeface("Calibri"), fntSz, txtClr);
            txt.MaxTextWidth = (rect.Width == 0) ? 1 : rect.Width;
            txt.MaxTextHeight = (rect.Height == 0) ? 1 : rect.Height;
            txt.TextAlignment = TextAlignment.Justify;
            txt.SetFontWeight(FontWeights.UltraBlack);
            txt.SetFontStretch(FontStretches.UltraExpanded);
            dc.DrawText(txt, rect.Location);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            Size DrawSize = new Size(this.RenderSize.Width - this.vScrollBar.RenderSize.Width, this.RenderSize.Height - this.hScrollBar.RenderSize.Height);
            Rect DrawRect = new Rect(new Point(0, 0), DrawSize);
            hScrollBar.Maximum = ((this.blockSize.FullWidth - DrawSize.Width) >= 0) ? this.blockSize.FullWidth - DrawSize.Width : 0;
            if (hScrollBar.Maximum == hScrollBar.Minimum) hScrollBar.IsEnabled = false;
            else hScrollBar.IsEnabled = true;
            drawingContext.DrawRectangle(Brushes.White, null, DrawRect);
            drawingContext.PushClip(new RectangleGeometry(DrawRect));
            //отрисовка заголовка
            Point curLocCell = new Point(0, 0);
            curLocCell.X = -this.hScrollBar.Value;
            drawingContext.DrawRectangle(this.HeaderTemplate.ViewT.ColorBackground, null, new Rect(curLocCell, new Size(this.BlockSize.FullWidth, this.blockSize.Height)));
            for (int i = 0; i < this.blockSize.ListColumnWidth.Count; i++)
            {
                Rect cellRect = new Rect(new Point(curLocCell.X + 1, curLocCell.Y+1), new Size(this.blockSize.ListColumnWidth[i]-2, this.blockSize.Height-2));
                drawingContext.DrawRectangle(null, new Pen(BorderColor, 1), cellRect);
                DrawBoldTxt(drawingContext, this.HeaderTemplate.ListColumnText[i], cellRect, this.HeaderTemplate.ViewT.FontColor,
                    new Typeface(this.HeaderTemplate.ViewT.FontName), this.HeaderTemplate.ViewT.FontSize-2);
                curLocCell.X += this.blockSize.ListColumnWidth[i];
            }
            //отрисовка строк
            int MaxIndex = (int)(DrawSize.Height / this.blockSize.Height) + this.IndexCurrentFirstVisibleElementSourceData;
            curLocCell.Y += this.blockSize.Height;
            curLocCell.X = -this.hScrollBar.Value;
            for (int j = this.IndexCurrentFirstVisibleElementSourceData; j < MaxIndex; j++)
            {
                if (this.SourceDataElementConvert == null) return;
                if ((j >= this.SourceData.Count) || (j < 0)) break;
                DataListTemplate currentItem = this.SourceDataElementConvert(SourceData[j], j);
                drawingContext.DrawRectangle(currentItem.ViewT.ColorBackground, null, new Rect(curLocCell, new Size(this.BlockSize.FullWidth, this.blockSize.Height)));
                Point shadowLocPt = curLocCell;
                //отрисовка одной строки
                for (int i = 0; i < this.blockSize.ListColumnWidth.Count; i++)
                {
                    Rect cellRect = new Rect(curLocCell, new Size(this.blockSize.ListColumnWidth[i], this.blockSize.Height));
                    drawingContext.DrawRectangle(null, new Pen(BorderColor, 1), cellRect);
                    //форматируем текст для рисования
                    FormattedText txt = new FormattedText(currentItem.ListColumnText[i],
                        CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface(currentItem.ViewT.FontName),
                        currentItem.ViewT.FontSize, currentItem.ViewT.FontColor);
                    txt.MaxTextWidth = cellRect.Width; ;
                    txt.MaxTextHeight = cellRect.Height;
                    txt.TextAlignment = TextAlignment.Justify;
                    //рисуем текст
                    drawingContext.DrawText(txt, cellRect.Location);
                    curLocCell.X += this.blockSize.ListColumnWidth[i];
                }
                //отрисовка тени
                if (j == IndexChoosenElementSourceData)
                {
                    drawingContext.PushOpacity(0.5);
                    drawingContext.DrawRectangle(this.shadowColor, null, new Rect(shadowLocPt, new Size(this.BlockSize.FullWidth, this.blockSize.Height)));
                    drawingContext.Pop();
                }
                curLocCell.X = -this.hScrollBar.Value;
                curLocCell.Y += this.blockSize.Height;
            }
        }
        
        public void Select(int index)
        {
            IndexChoosenElementSourceData = index;
            this.vScrollBar.Value = index;
            if (index < SourceData.Count)
                SelectItemRaiseEvent(SourceData[index], index);
        }

        private void vScrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            IndexCurrentFirstVisibleElementSourceData = (int)((ScrollBar)sender).Value;
            this.InvalidateVisual();
        }

        private void hScrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.InvalidateVisual();
        }

        bool flagModeChangedColumn { get; set; }
        int indexChangedColumn { get; set; }
        const int borderWidthChangedColumn = 2;
        const int minWidthColumn = 10;

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            flagModeChangedColumn = false;
            this.Cursor = Cursors.Arrow;
            Point currentLocation = e.GetPosition(this);
            if (currentLocation.Y < this.blockSize.Height) return;
            IndexChoosenElementSourceData = ((int)currentLocation.Y - this.blockSize.Height) / this.blockSize.Height + IndexCurrentFirstVisibleElementSourceData;
            if (IndexChoosenElementSourceData < this.SourceData.Count)
            {
                this.InvalidateVisual();
                ClickItemRaiseEvent(this.SourceData[IndexChoosenElementSourceData], IndexChoosenElementSourceData, e.ChangedButton);
                SelectItemRaiseEvent(this.SourceData[IndexChoosenElementSourceData], IndexChoosenElementSourceData);
            }
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            Point currentLocation = e.GetPosition(this);
            int dX = (int)e.GetPosition(this).X + (int)hScrollBar.Value;
            int currentWidth = 0;
            for (int i = 0; i < blockSize.ListColumnWidth.Count; i++)
            {
                currentWidth += blockSize.ListColumnWidth[i];
                if ((dX >= (currentWidth - borderWidthChangedColumn)) && (dX <= (currentWidth + borderWidthChangedColumn)))
                {
                    flagModeChangedColumn = true;
                    indexChangedColumn = i;
                    this.Cursor = Cursors.SizeWE;
                    return;
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            Point currentLocation = e.GetPosition(this);
            int dX = (int)e.GetPosition(this).X + (int)hScrollBar.Value;
            int currentWidth = 0;

            if (flagModeChangedColumn)
            {
                int delta = dX - blockSize.GetWidthByIndex(indexChangedColumn);
                if (delta == 0) return;
                else if (delta < 0)
                {
                    blockSize.ListColumnWidth[indexChangedColumn] -= Math.Abs(delta);
                    if (blockSize.ListColumnWidth[indexChangedColumn] < minWidthColumn) blockSize.ListColumnWidth[indexChangedColumn] = minWidthColumn;
                }
                else if (delta > 0)
                {
                    blockSize.ListColumnWidth[indexChangedColumn] += Math.Abs(delta);
                }
                this.InvalidateVisual();
            }
            else
                for (int i = 0; i < blockSize.ListColumnWidth.Count; i++)
                {
                    currentWidth += blockSize.ListColumnWidth[i];
                    if ((dX >= (currentWidth - borderWidthChangedColumn)) && (dX <= (currentWidth + borderWidthChangedColumn)))
                    {
                        this.Cursor = Cursors.SizeWE;
                        break;
                    }
                    else
                        this.Cursor = Cursors.Arrow;
                }
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            if (flagModeChangedColumn)
                if (e.LeftButton == MouseButtonState.Released)
                    flagModeChangedColumn = false;
        }

        public class ClickDataItemEventArgs : EventArgs
        {
            public object Item { get; protected set; }
            public int Index { get; protected set; }
            public MouseButton ChangedButton { get; set; }
            public ClickDataItemEventArgs() : base() { Item = null; Index = -1; ChangedButton = MouseButton.Left; }
            public ClickDataItemEventArgs(object item, int index, MouseButton changedButton) : base() { Item = item; Index = index; ChangedButton = changedButton; }
        }
        /// <summary>
        /// Событие клика по элементу
        /// </summary>
        public event EventHandler<ClickDataItemEventArgs> ClickItem;
        protected void ClickItemRaiseEvent(object item, int index, MouseButton changedButton)
        {
            if (ClickItem != null)
                ClickItem(this, new ClickDataItemEventArgs(item, index, changedButton));
        }

        public class SelectItemEventArgs : EventArgs
        {
            public object Item { get; protected set; }
            public int Index { get; protected set; }
            public SelectItemEventArgs() : base() { Item = null; Index = -1;}
            public SelectItemEventArgs(object item, int index) : base() { Item = item; Index = index;}
        }
        public event EventHandler<SelectItemEventArgs> SelectItem;
        protected void SelectItemRaiseEvent(object item, int index)
        {
            if (SelectItem != null)
                SelectItem(this, new SelectItemEventArgs(item, index));
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
