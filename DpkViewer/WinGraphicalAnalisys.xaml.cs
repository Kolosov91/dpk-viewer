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
using System.Windows.Shapes;
using ConstructGraphicLibrary.Data;
using DpkViewer.Tools;
using System.Collections.ObjectModel;
using ConstructGraphicLibrary.BaseTools;
using System.Reflection;

namespace DpkViewer
{
    /// <summary>
    /// Логика взаимодействия для WinGraphicalAnalisys.xaml
    /// </summary>
    public partial class WinGraphicalAnalisys : Window
    {
        readonly DpkViewerApp App = ((DpkViewerApp)DpkViewerApp.Current);

        ObservableCollection<SourceGraphic> NamedGraphics;

        ObservableCollection<TimeMark> TimeMarks;

        public WinGraphicalAnalisys()
        {
            InitializeComponent();
            TimeMarks = new ObservableCollection<TimeMark>();
            NamedGraphics = new ObservableCollection<SourceGraphic>();
            if (App.DpkLogFile.Count == 0) return;
            TimeSpan leftOfLeftTime = App.DpkLogFile[0].Time;
            TimeSpan rightOfRightTime = App.DpkLogFile[App.DpkLogFile.Count-1].Time;
            //расчёт видимой временной границы
            TimeSpan leftT = leftOfLeftTime;
            TimeSpan rightT = leftT.Add(new TimeSpan( (rightOfRightTime.Subtract(leftOfLeftTime)).Ticks / 600));
            //установка временных границ компонентов
            multiLayerGraphic_Canvas.LeftTime = leftT;
            multiLayerGraphic_Canvas.RightTime = rightT;
            //
            comboBoxMarks.ItemsSource = TimeMarks;
            comboBoxMarkBegin.ItemsSource = TimeMarks;
            comboBoxMarkEnd.ItemsSource = TimeMarks;
            comboBoxGraphics.ItemsSource = NamedGraphics;
            //
            horizontalTimeScrollBar.LeftTimeBorder = leftOfLeftTime;
            horizontalTimeScrollBar.RightTimeBorder = rightOfRightTime;
            horizontalTimeScrollBar.VisibilityLeftTimeBorder = leftT;
            horizontalTimeScrollBar.VisibilityTimeInterval = rightT.Subtract(leftT);
        }

        private void horizontalTimeScrollBar_ScrolledEvent(object sender, EventArgs<HorizontalTimeScrollBarData> e)
        {
            multiLayerGraphic_Canvas.SetTime(e.Value.CurrentLeftTime, e.Value.CurrentRightTime);//при скроллинге меняем содержимое графика (соответвует видимому интервалу на скроллбаре)
        }

        private void comboBoxMarks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GraphicalAnalisysCommands.ChooseActiveMark.Execute(((ComboBox)sender).SelectedIndex, null);
        }

        private void ChooseActiveMark(object sender, ExecutedRoutedEventArgs e)
        {
            multiLayerGraphic_Canvas.IndexActiveMark = ((int)e.Parameter);
        }

        private void AddGraphic(object sender, ExecutedRoutedEventArgs e)
        {
            WinAddGraphic win = new WinAddGraphic(this);
            if (win.ShowDialog().Equals(true))
            {
                NamedGraphics.Add(SourceBitGraphicConstructor.Construct(App.DpkLogFile, win.Address, win.NumBit));
                multiLayerGraphic_Canvas.AddSourceGraphic(NamedGraphics.Last());
            }
        }

        readonly TimeSpan timeShift = new TimeSpan(0, 0, 0, 0, 10);

        private void ScaleUpTimeInterval(object sender, ExecutedRoutedEventArgs e)
        {  
            TimeSpan newLeftTime = horizontalTimeScrollBar.VisibilityLeftTimeBorder.Subtract(timeShift);
            TimeSpan newRightTime = horizontalTimeScrollBar.VisibilityRightTimeBorder.Add(timeShift);
            if (newLeftTime.CompareTo(horizontalTimeScrollBar.LeftTimeBorder) >= 0)
            { 
                horizontalTimeScrollBar.VisibilityLeftTimeBorder = newLeftTime;
                multiLayerGraphic_Canvas.LeftTime = newLeftTime;
            }
            if (newRightTime.CompareTo(horizontalTimeScrollBar.RightTimeBorder) <= 0)
            {
                horizontalTimeScrollBar.VisibilityTimeInterval = newRightTime.Subtract(newLeftTime);
                multiLayerGraphic_Canvas.RightTime = newRightTime;
            }
        }

        private void ScaleDownTimeInterval(object sender, ExecutedRoutedEventArgs e)
        {
            TimeSpan newLeftTime = horizontalTimeScrollBar.VisibilityLeftTimeBorder.Add(timeShift);
            TimeSpan newRightTime = horizontalTimeScrollBar.VisibilityRightTimeBorder.Subtract(timeShift);
            if (newRightTime.Subtract(newLeftTime).CompareTo(TimeSpan.Zero) >= 0)
            {
                horizontalTimeScrollBar.VisibilityLeftTimeBorder = newLeftTime;
                multiLayerGraphic_Canvas.LeftTime = newLeftTime;
                horizontalTimeScrollBar.VisibilityTimeInterval = newRightTime.Subtract(newLeftTime);
                multiLayerGraphic_Canvas.RightTime = newRightTime;
            }
        }

        private void SetActiveGraphic(object sender, ExecutedRoutedEventArgs e)
        {
            multiLayerGraphic_Canvas.IndexActiveGraphic = comboBoxGraphics.SelectedIndex;
        }

        private void comboBoxGraphics_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GraphicalAnalisysCommands.SetActiveGraphic.Execute(null, null);
        }

        private void RemoveGraphic(object sender, ExecutedRoutedEventArgs e)
        {
            int index = comboBoxGraphics.SelectedIndex;
            NamedGraphics.RemoveAt(index);
            multiLayerGraphic_Canvas.RemoveSourceGraphic(index);
        }

        private void RemoveGraphicCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (comboBoxGraphics == null) { e.CanExecute = false; return; }
            if (comboBoxGraphics.SelectedIndex == -1) e.CanExecute = false;
            else e.CanExecute = true;
        }

        private void AddMark(object sender, ExecutedRoutedEventArgs e)
        {
            Point locMark = (Point)e.Parameter;
            TimeMark mark = new TimeMark(ToolFunctions.GetTimeByPoint(locMark,
                new Point(multiLayerGraphic_Canvas.CommonData.Width_LeftBorder, 0),
                multiLayerGraphic_Canvas.CommonData.TimeInPoint, multiLayerGraphic_Canvas.LeftTime), "Метка " + TimeMarks.Count);
            TimeMarks.Add(mark);
            multiLayerGraphic_Canvas.AddTimeMark(mark);
        }

        private void multiLayerGraphic_Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            GraphicalAnalisysCommands.AddMark.Execute(e.GetPosition(multiLayerGraphic_Canvas), null);
        }

        private void RemoveActiveMark(object sender, ExecutedRoutedEventArgs e)
        {
            int index = comboBoxMarks.SelectedIndex;
            if (index == -1) return;
            TimeMarks.RemoveAt(index);
            multiLayerGraphic_Canvas.RemoveTimeMark(index);
        }

        private void ShowActiveMark(object sender, ExecutedRoutedEventArgs e)
        {
            TimeSpan halhInterval = new TimeSpan(horizontalTimeScrollBar.VisibilityTimeInterval.Ticks / 2);
            TimeSpan visibilityLeftTime = (TimeMarks[comboBoxMarks.SelectedIndex].Time.Subtract(halhInterval).CompareTo(horizontalTimeScrollBar.LeftTimeBorder) < 0) ?
                horizontalTimeScrollBar.LeftTimeBorder : TimeMarks[comboBoxMarks.SelectedIndex].Time.Subtract(halhInterval);
            TimeSpan newInterval = (visibilityLeftTime.Add(horizontalTimeScrollBar.VisibilityTimeInterval).CompareTo(horizontalTimeScrollBar.RightTimeBorder) > 0)?
                horizontalTimeScrollBar.RightTimeBorder.Subtract(visibilityLeftTime) : horizontalTimeScrollBar.VisibilityTimeInterval;
            horizontalTimeScrollBar.VisibilityTimeInterval = newInterval;
            horizontalTimeScrollBar.VisibilityLeftTimeBorder = visibilityLeftTime;
        }

        private void ShowActiveMarkCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (comboBoxMarks.SelectedIndex == -1)
                e.CanExecute = false;
            else
                e.CanExecute = true;
        }

        private void ShowMarkInterval(object sender, ExecutedRoutedEventArgs e)
        {
            if (TimeMarks[comboBoxMarkBegin.SelectedIndex].Time.CompareTo(TimeMarks[comboBoxMarkEnd.SelectedIndex].Time) < 0)
            {
                horizontalTimeScrollBar.VisibilityLeftTimeBorder = TimeMarks[comboBoxMarkBegin.SelectedIndex].Time;
                horizontalTimeScrollBar.VisibilityTimeInterval = TimeMarks[comboBoxMarkEnd.SelectedIndex].Time.Subtract(TimeMarks[comboBoxMarkBegin.SelectedIndex].Time);
            }
            else
            {
                horizontalTimeScrollBar.VisibilityLeftTimeBorder = TimeMarks[comboBoxMarkEnd.SelectedIndex].Time;
                horizontalTimeScrollBar.VisibilityTimeInterval = TimeMarks[comboBoxMarkBegin.SelectedIndex].Time.Subtract(TimeMarks[comboBoxMarkEnd.SelectedIndex].Time);
            }
        }

        private void ShowMarkIntervalCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if ((comboBoxMarkBegin.SelectedIndex == -1) || (comboBoxMarkEnd.SelectedIndex == -1)) { e.CanExecute = false; return; }
            else e.CanExecute = true;
            if (comboBoxMarkBegin.SelectedIndex == comboBoxMarkEnd.SelectedIndex) e.CanExecute = false;
            else e.CanExecute = true;
        }

        private void ResetInterval(object sender, ExecutedRoutedEventArgs e)
        {
            if (App.DpkLogFile.Count == 0) return;
            TimeSpan leftOfLeftTime = App.DpkLogFile[0].Time;
            TimeSpan rightOfRightTime = App.DpkLogFile[App.DpkLogFile.Count - 1].Time;
            //расчёт видимой временной границы
            TimeSpan leftT = leftOfLeftTime;
            TimeSpan rightT = leftT.Add(new TimeSpan((rightOfRightTime.Subtract(leftOfLeftTime)).Ticks / 600));
            horizontalTimeScrollBar.VisibilityLeftTimeBorder = leftT;
            horizontalTimeScrollBar.VisibilityTimeInterval = rightT.Subtract(leftT);
        }

        private void comboBoxMark_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((comboBoxMarkBegin.SelectedIndex == -1) || (comboBoxMarkEnd.SelectedIndex == -1))
            { textBlockIntervalValue.Text = "00:00:00:000"; return; }
            if (TimeMarks[comboBoxMarkBegin.SelectedIndex].Time.CompareTo(TimeMarks[comboBoxMarkEnd.SelectedIndex].Time) < 0)
                textBlockIntervalValue.Text = Macros.TimeSpanToStringLine(TimeMarks[comboBoxMarkEnd.SelectedIndex].Time.Subtract(TimeMarks[comboBoxMarkBegin.SelectedIndex].Time));
            else
                textBlockIntervalValue.Text = Macros.TimeSpanToStringLine(TimeMarks[comboBoxMarkBegin.SelectedIndex].Time.Subtract(TimeMarks[comboBoxMarkEnd.SelectedIndex].Time));
        }

        private void multiLayerGraphic_Canvas_ApproximationChangedEvent(object sender, EventArgs<bool> e)
        {
            if (e.Value) imageApproximation.Source = ToolFunctions.GetImage(Assembly.GetExecutingAssembly().FullName, "Resources/Other/Approximation.ico");
            else imageApproximation.Source = ToolFunctions.GetImage(Assembly.GetExecutingAssembly().FullName, "Resources/Other/NoApproximation.ico");
        }
    }

    public class GraphicalAnalisysCommands
    {
        public static RoutedUICommand ScaleUpTimeInterval = new RoutedUICommand("Увеличить временной интервал", "Увеличить временной интервал",
            typeof(GraphicalAnalisysCommands));
        public static RoutedUICommand ScaleDownTimeInterval = new RoutedUICommand("Уменьшить временной интервал", "Уменьшить временной интервал",
            typeof(GraphicalAnalisysCommands));
        public static RoutedUICommand AddMark = new RoutedUICommand("Добавить метку", "Добавить метку",
            typeof(GraphicalAnalisysCommands));
        public static RoutedUICommand ChooseActiveMark = new RoutedUICommand("Выбрать активную метку", "Выбрать активную метку",
            typeof(GraphicalAnalisysCommands));
        public static RoutedUICommand ShowActiveMark = new RoutedUICommand("Показать активную метку", "Показать активную метку",
            typeof(GraphicalAnalisysCommands));
        public static RoutedUICommand RemoveActiveMark = new RoutedUICommand("Удалить активную метку", "Удалить активную метку",
            typeof(GraphicalAnalisysCommands));
        public static RoutedUICommand ShowMarkInterval = new RoutedUICommand("Показать интервал между метками", "Показать интервал между метками",
            typeof(GraphicalAnalisysCommands));
        public static RoutedUICommand AddGraphic = new RoutedUICommand("Добавить график", "Добавить график в список",
            typeof(GraphicalAnalisysCommands));
        public static RoutedUICommand RemoveGraphic = new RoutedUICommand("Удалить график", "Удалить график из списка",
            typeof(GraphicalAnalisysCommands));
        public static RoutedUICommand SetActiveGraphic = new RoutedUICommand("Выбрать активный график", "Выбрать активный график из списка",
            typeof(GraphicalAnalisysCommands));
        public static RoutedUICommand ResetInterval = new RoutedUICommand("Сброс временного интервала", "Сбрасывает временной интервал к оптимальному",
            typeof(GraphicalAnalisysCommands));
    }
}
