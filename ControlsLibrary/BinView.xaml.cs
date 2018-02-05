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
using DpkViewer.Tools;

namespace ControlsLibrary
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class BinView : UserControl
    {
        List<bool> Values { get; set; }
        public List<bool> GetValues() { List<bool> result = new List<bool>(); result.AddRange(Values); return result; }
        public void SetValues(List<bool> values) { Values.Clear(); Values.AddRange(values); InvalidateVisual(); }
        public void CreateValues(int cnt) { Values = new List<bool>(); for (int i = 0; i < cnt; i++) { Values.Add(false); } InvalidateVisual(); }

        public int FirstNumber { get; protected set; }
        public void SetFirstNumber(int number) { FirstNumber = number; InvalidateVisual(); }

        public Brush ColorTrue { get; protected set; }
        public Brush ColorFalse { get; protected set; }
        public void SetColors(Brush trueClr, Brush falseClr) { ColorTrue = trueClr; ColorFalse = falseClr; InvalidateVisual(); }

        public String FontName { get; protected set; }
        public void SetFontName(string fontName) { FontName = fontName; InvalidateVisual(); }

        public bool IsVisibleText { get; protected set; }
        public void SetVisibleText(bool value) { IsVisibleText = value; InvalidateVisual(); }

        public BinView()
        {
            InitializeComponent();
            Values = new List<bool>();
            FirstNumber = 0;
            ColorTrue = new LinearGradientBrush(new GradientStopCollection() { new GradientStop(Colors.WhiteSmoke, 0), new GradientStop(Colors.Yellow, 1) },
                    new Point(0, 0), new Point(0, 1));
            ColorFalse = new LinearGradientBrush(new GradientStopCollection() { new GradientStop(Colors.WhiteSmoke, 0), new GradientStop(Colors.LightGray, 1) },
                    new Point(0, 0), new Point(0, 1));
            Background = null;
            BorderBrush = null;
            IsVisibleText = true;
            FontName = "Courier New";
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            InvalidateVisual();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            DrawingContext dc = drawingContext;
            dc.DrawRectangle(Background, new Pen(BorderBrush, 1), new Rect(new Point(0, 0), RenderSize));
            if ((Values == null) || (Values.Count == 0)) return;
            double FullWidthCell = RenderSize.Width / (double)(Values.Count);
            double WidthCell = FullWidthCell;
            double HeightCell = RenderSize.Height;
            if ((WidthCell > 2) && (HeightCell > 2)) { WidthCell -= 2; HeightCell -= 2; }
            int currentNumber = FirstNumber;
            Point currentPointCell = new Point(1, 1);
            foreach (bool item in Values)
            {
                dc.DrawRoundedRectangle((item) ? ColorTrue : ColorFalse, new Pen(BorderBrush, 1),
                    new Rect(currentPointCell, new Size(WidthCell, HeightCell)),
                    0.15 * WidthCell, 0.15 * HeightCell);
                if (IsVisibleText) Service.DrawTxt(dc, currentNumber.ToString().PadLeft(2, '0'), currentPointCell, new Size(WidthCell, HeightCell), Foreground, FontName);
                currentPointCell.X += FullWidthCell;
                currentNumber++;
            }
        }

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
            if ((Values == null) || (Values.Count == 0)) return;
            double FullWidthCell = RenderSize.Width / (double)(Values.Count);
            double WidthCell = FullWidthCell;
            double HeightCell = RenderSize.Height;
            if ((WidthCell > 2) && (HeightCell > 2)) { WidthCell -= 2; HeightCell -= 2; }
            Point currentPointCell = new Point(1, 1);
            for (int currentNumber = 0; currentNumber < Values.Count; currentNumber++, currentPointCell.X += FullWidthCell)
            {
                if (new Rect(currentPointCell, new Size(WidthCell, HeightCell)).Contains(e.GetPosition(this)))
                {
                    Values[currentNumber] = !Values[currentNumber];
                    this.InvalidateVisual();
                    this.ClickItemRaiseEvent(Values[currentNumber], currentNumber, e);
                }
            }
        }
    }
}
