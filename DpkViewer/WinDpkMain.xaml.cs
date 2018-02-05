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
using ListStringViewWPF;
using Microsoft.Win32;
using DPK;
using System.Windows.Controls.Primitives;
using DpkViewer.Tools;
using System.IO;
using ControlsLibrary;

namespace DpkViewer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class WinDpkMain : Window
    {
        const string Version = "1.2";
        //Список окон с результатами поиска
        public List<WinResultSearch> ListResultWindows { get; set; }
        //Ссылка на Application (В нём имя текущего файла и его содержимое)
        readonly DpkViewerApp App = ((DpkViewerApp)DpkViewerApp.Current);
        /*Подробное инфо о слове*/
        Popup InfoDpkTooltip { get; set; }
        DpkView DpkViewer { get; set; }
        /**/

        public WinDpkMain()
        {
            InitializeComponent();
            this.listStringViewDpkWords.BlockSize = new BlockSizeTemplate(20, new List<int> { 50, 140, 100, 250 });
            this.listStringViewDpkWords.BorderColor = SystemColors.GradientActiveCaptionBrush;
            this.listStringViewDpkWords.HeaderTemplate = new DataListTemplate(new ViewTemplate(
                new LinearGradientBrush(new GradientStopCollection() {new GradientStop(Colors.WhiteSmoke, 0), new GradientStop(SystemColors.ActiveCaptionColor, 1) },
                    new Point(0,0) , new Point(0,1)), 
                "Calibri", 16, Brushes.Black),
                new List<string> { "№", "Время [Ч:М:С:МС]", "Адрес [1-8]", "Данные [9-32]" });
            this.listStringViewDpkWords.SourceDataElementConvert = SourceDataElementConvert;
            this.listStringViewDpkWords.ClickItem += new EventHandler<ListStringView.ClickDataItemEventArgs>(listStringViewDpkWords_ClickItem);
            /**/
            ListResultWindows = new List<WinResultSearch>();
            //App = ((DpkViewerApp)DpkViewerApp.Current);
            /*PopupInit*/
            InfoDpkTooltip = new Popup();
            InfoDpkTooltip.MouseUp += new MouseButtonEventHandler(InfoDpkTooltip_MouseUp);
            //Создание ДПК слова
            //DpkWordInfo = new DpkEdit();
            //DpkWordInfo.IsEnabled = false;
            DpkViewer = new DpkView();
            DpkViewer.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
            DpkViewer.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            //Создание границы подсказки
            Grid grid = new Grid();
            grid.Height = 130;
            grid.Width = 500;
            grid.Background = Brushes.WhiteSmoke;
            Border border = new Border();
            border.BorderBrush = Brushes.DarkGray;
            border.BorderThickness = new Thickness(2);
            border.Child = DpkViewer;
            grid.Children.Add(border);
            InfoDpkTooltip.Child = grid;
            //
            InfoDpkTooltip.StaysOpen = false;
            InfoDpkTooltip.Placement = PlacementMode.Mouse;
        }

        void InfoDpkTooltip_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ((Popup)sender).IsOpen = false;//При клике по подробному инфо о слове - закрываем его
        }

        void listStringViewDpkWords_ClickItem(object sender, ListStringView.ClickDataItemEventArgs e)
        {
            /*При клике по слову из списка - создавать и открывать всплывающую подсказку с подробным инфо о слове*/
            if (e.ChangedButton == MouseButton.Right)
            {
                DpkWordItem item = (DpkWordItem)e.Item;
                int val = (item.ADR | (item.DATA << 8));
                DpkViewer.SetDpkWord(item);
                //DpkWordInfo.DpkWordValue = val;
                //DpkWordInfo.TxtMark = string.Format("Время: {0}:{1}:{2}:{3} -", item.Time.Hours.ToString().PadLeft(2, '0'), item.Time.Minutes.ToString().PadLeft(2, '0'),
                //    item.Time.Seconds.ToString().PadLeft(2, '0'), item.Time.Milliseconds.ToString().PadLeft(3, '0'));
                InfoDpkTooltip.IsOpen = true;
            }
        }

        /*Шаблоны цветовых расцыеток слов ДПК в списке*/
        ViewTemplate ViewMessage_1 = new ViewTemplate(SystemColors.ControlBrush, "Courier New", 14, Brushes.Black);
        ViewTemplate ViewMessage_2 = new ViewTemplate(Brushes.White, "Courier New", 14, Brushes.Black);
        ViewTemplate ViewSynchroImpulse = new ViewTemplate(Brushes.Yellow, "Courier New", 14, Brushes.Black);
        ViewTemplate ViewError = new ViewTemplate(Brushes.Red, "Courier New", 14, Brushes.Black);
        ViewTemplate ViewOver = new ViewTemplate(Brushes.Blue, "Courier New", 14, Brushes.Black);

        //Функция построения строки со словом ДПК в списке
        DataListTemplate SourceDataElementConvert(object item, int indexItem)
        {
            DpkWordItem dkpWord = (DpkWordItem)item;
            DataListTemplate data_l = new DataListTemplate();
            TimeSpan timeItem = dkpWord.Time;
            string adr_str = Convert.ToString(dkpWord.ADR, 2).PadLeft(8, '0');
            string adr = "";
            for (int i = 0; i < 8; i++)
            { adr = adr_str[i] + adr; }

            string data_str = Convert.ToString(dkpWord.DATA, 2).PadLeft(24, '0');
            string data = "";
            for (int i = 0; i < 24; i++)
            { data = data_str[i] + data; }

            data_l.ListColumnText = new List<string>() { 
                indexItem.ToString(), 
                string.Format("{0}:{1}:{2}:{3}", timeItem.Hours.ToString().PadLeft(2,'0'), timeItem.Minutes.ToString().PadLeft(2,'0'), 
                timeItem.Seconds.ToString().PadLeft(2,'0'), timeItem.Milliseconds.ToString().PadLeft(3,'0')), 
                adr, 
                data};
            if ((indexItem % 2) == 0)
                data_l.ViewT = ViewMessage_1;
            else
                data_l.ViewT = ViewMessage_2;
            if ((dkpWord.Flags & 0x1).Equals(0x1)) data_l.ViewT = ViewSynchroImpulse;
            if ((dkpWord.Flags & 0x2).Equals(0x2)) data_l.ViewT = ViewError;
            if ((dkpWord.Flags & 0x4).Equals(0x4)) data_l.ViewT = ViewOver;
            return data_l;
        }
        //Заполнение списка словами ДПК
        public void FillListDpkWord()
        {
            this.listStringViewDpkWords.ClearSourceData();
            int cnt = App.DpkLogFile.Count;
            for (int i = 0; i < cnt; i++)
            {
                if (App.ListFilteredAddresses.Contains(App.DpkLogFile[i].ADR))
                    this.listStringViewDpkWords.AddElementToSourceData(App.DpkLogFile[i]);
            }
        }

        private void OpenProtocolFile_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog win = new OpenFileDialog();
            win.Multiselect = false;
            win.Filter = DpkDataConstants.LOG_FILE_MASK;
            if (win.ShowDialog(this).Equals(false)) return;
            ((DpkViewerApp)DpkViewerApp.Current).LoadDpkLogFile(win.FileName);
            FillListDpkWord();
        }

        private void Exit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            switch (MessageBox.Show("Выйти из программы?", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                case MessageBoxResult.No:
                    e.Cancel = true;
                    break;
            }
        }

        private void FilterParametres_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            WinFilter win = new WinFilter(this);
            if (win.ShowDialog().Equals(true))
            {
                FillListDpkWord();
            }
        }

        WinResultSearch win { get; set; }

        private void Search_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            WinSearch searchWin = new WinSearch(this);
            if (searchWin.ShowDialog().Equals(true))
            {
                List<object> resultSearch = GenerateSearchList(searchWin.GetSearchTemplate());
                //WinResultSearch win = new WinResultSearch(this, resultSearch, searchWin.GetSearchTemplate().ListAddresses, searchWin.GetSearchTemplate().ListValues);
                if (win != null) win.Close();
                win = new WinResultSearch(this, resultSearch, searchWin.GetSearchTemplate());
                win.Show();
            }
        }

        List<object> GenerateSearchList(SearchTemplate_2 template)
        {
            List<object> resList = new List<object>();
            foreach (DpkWordItem item in this.listStringViewDpkWords.SourceData)
            {
                foreach (int address in template.ListAddresses)
                {
                    if (item.ADR.Equals(address))
                    {
                        List<bool> valueWord = Service.ConvertFromInt(item.DATA, 24);
                        bool badFlag = false;
                        for (int i = 0; i < template.Value.Count; i++)
                        {
                            if (template.Check[i])
                                if (!template.Value[i].Equals(valueWord[i]))
                                {
                                    badFlag = true;
                                    break;
                                }
                        }
                        if (badFlag) continue;
                        resList.Add(item);
                    }
                }
            }
            return resList;
        }

        private void SearchPreviousError(object sender, ExecutedRoutedEventArgs e)
        {
            int curIndex = (listStringViewDpkWords.IndexChoosenElementSourceData == -1) ? 0 : listStringViewDpkWords.IndexChoosenElementSourceData;
            for (int i = curIndex - 1; i >= 0; i--)
                if ((((DpkWordItem)listStringViewDpkWords.SourceData[i]).Flags & 0x2) > 0)
                {
                    listStringViewDpkWords.Select(i);
                    return;
                }
            MessageBox.Show(this, "Не найдено ни одного сообщения с ошибкой", "Результаты поиска", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SearchNextError(object sender, ExecutedRoutedEventArgs e)
        {
            int curIndex = (listStringViewDpkWords.IndexChoosenElementSourceData == -1) ? 0 : listStringViewDpkWords.IndexChoosenElementSourceData;
            for (int i = curIndex + 1; i < listStringViewDpkWords.SourceData.Count; i++)
                if ((((DpkWordItem)listStringViewDpkWords.SourceData[i]).Flags & 0x2) > 0)
                {
                    listStringViewDpkWords.Select(i);
                    return;
                }
            MessageBox.Show(this, "Не найдено ни одного сообщения с ошибкой", "Результаты поиска", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SearchPreviousSynchroImpulse(object sender, ExecutedRoutedEventArgs e)
        {
            int curIndex = (listStringViewDpkWords.IndexChoosenElementSourceData == -1) ? 0 : listStringViewDpkWords.IndexChoosenElementSourceData;
            for (int i = curIndex - 1; i >= 0; i--)
                if ((((DpkWordItem)listStringViewDpkWords.SourceData[i]).Flags & 0x1) > 0)
                {
                    listStringViewDpkWords.Select(i);
                    return;
                }
            MessageBox.Show(this, "Не найдено ни одного сообщения с синхроимпульсом", "Результаты поиска", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SearchNextSynchroImpulse(object sender, ExecutedRoutedEventArgs e)
        {
            int curIndex = (listStringViewDpkWords.IndexChoosenElementSourceData == -1) ? 0 : listStringViewDpkWords.IndexChoosenElementSourceData;
            for (int i = curIndex + 1; i < listStringViewDpkWords.SourceData.Count; i++)
                if ((((DpkWordItem)listStringViewDpkWords.SourceData[i]).Flags & 0x1) > 0)
                {
                    listStringViewDpkWords.Select(i);
                    return;
                }
            MessageBox.Show(this, "Не найдено ни одного сообщения с синхроимпульсом", "Результаты поиска", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void GraphicalAnalisys(object sender, ExecutedRoutedEventArgs e)
        {
            WinGraphicalAnalisys winAnalisys = new WinGraphicalAnalisys();
            winAnalisys.Owner = this;
            winAnalisys.Show();
        }

        private void SaveProtocolAsTxt(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog win = new SaveFileDialog();
            win.Filter = "Тексотвый файл|*.txt";
            win.Title = "Сохранить текстовый протокол";
            if (win.ShowDialog(this).Equals(false)) return;
            using (StreamWriter sw = new StreamWriter(win.FileName, false, Encoding.GetEncoding(1251)))
            {
                sw.WriteLine("*** Дата создания файла: " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " ***");
                sw.WriteLine(String.Format("*** Создан на основе файла: '{0}' ***", App.CurrentFileName));
                List<object> list_buf = App.DpkLogFile.GetBuf();
                int counter_word = 0;
                foreach (DpkWordItem item in list_buf)
                {
                    string line_str = string.Format("№ {0} - Вр {1} - Адр 0x{2} - Д 0x{3}",
                        counter_word,
                        string.Format("{0}:{1}:{2}:{3}", item.Time.Hours.ToString().PadLeft(2,'0'), item.Time.Minutes.ToString().PadLeft(2,'0'), 
                        item.Time.Seconds.ToString().PadLeft(2,'0'), item.Time.Milliseconds.ToString().PadLeft(3,'0')),
                        item.ADR.ToString("X").PadLeft(2, '0'),
                        item.DATA.ToString("X").PadLeft(8, '0'));
                    if ((item.Flags & DpkDataConstants.SYNC_FLAG) > 0)//синхроимп
                    { line_str += " - синхроимпульс"; }
                    else if ((item.Flags & DpkDataConstants.ERROR_FLAG) > 0)//ошибка
                    { line_str += " - ошибка"; }
                    else if ((item.Flags & DpkDataConstants.OVERFLOW_FLAG) > 0)//переполнение
                    { line_str += " - переполнение"; }
                    //
                    sw.WriteLine(line_str);
                    counter_word++;
                }
                sw.Close();
            }
        }

        private void AboutApp(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show(this, 
                "Название: \n   'DpkViewer'" + "\n\n" +
                "----- ----- ----- ----- -----\n" +
                "Назначение: \n   Программа для просмотра и анализа \n   протокола обмена по интерфейсу ДПК" + "\n\n" +
                "----- ----- ----- ----- -----\n" +
                "Конфигурация разработки: \n   MS Visual Studio 2010, \n   dotNetFramework 4.0, \n   WPF" + "\n\n" +
                "----- ----- ----- ----- -----\n" +
                "Автор: \n   Колосов Владимир Владимирович" + "\n\n" +
                "----- ----- ----- ----- -----\n" +
                "Версия: " + Version + "\n\n" +
                "----- ----- ----- ----- -----\n" +
                "Примечание: \n   Использование данной программы \n   без разрешения автора - \n   ЗАПРЕЩЕНО!" +
            "\n\n", 
                "О программе", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void FormatView(object sender, ExecutedRoutedEventArgs e)
        {
            WinFormatView win = new WinFormatView(this);
            win.Owner = this;
            win.Show();
        }
    }

    public class MainCommands
    {
        /*Файл*/
        public static RoutedUICommand OpenProtocolFile = new RoutedUICommand("Открыть протокол", "Открыть файл с протколом",
            typeof(MainCommands), new InputGestureCollection() { new KeyGesture(Key.O, ModifierKeys.Control) });
        public static RoutedUICommand Exit = new RoutedUICommand("Выход", "Выйти из программы",
            typeof(MainCommands), new InputGestureCollection() { new KeyGesture(Key.F4, ModifierKeys.Alt) });
        /*Фильтрация*/
        public static RoutedUICommand FilterParametres = new RoutedUICommand("Параметры фильтрации", "Задать параметры фильтрации",
            typeof(MainCommands), new InputGestureCollection() { new KeyGesture(Key.P, ModifierKeys.Alt) });
        public static RoutedUICommand FilterParametresLoad = new RoutedUICommand("Открыть параметры", "Загрузить параметры фильтрации",
            typeof(MainCommands), new InputGestureCollection() { new KeyGesture(Key.O, ModifierKeys.Alt) });
        public static RoutedUICommand FilterParametresSave = new RoutedUICommand("Сохранить параметры", "Сохранить параметры фильтрации",
            typeof(MainCommands), new InputGestureCollection() { new KeyGesture(Key.S, ModifierKeys.Alt) });
        /*Поиск*/
        public static RoutedUICommand Search = new RoutedUICommand("Общий поиск", "Поиск слов ДПК по параметрам",
            typeof(MainCommands), new InputGestureCollection() { new KeyGesture(Key.F3) });
        public static RoutedUICommand SearchPreviousError = new RoutedUICommand("Предыдущая ошибка", "Поиск Предыдущая ошибка",
            typeof(MainCommands));
        public static RoutedUICommand SearchNextError = new RoutedUICommand("Следующая ошибка", "Поиск Следующая ошибка",
            typeof(MainCommands));
        public static RoutedUICommand SearchPreviousSynchroImpulse = new RoutedUICommand("Предыдущий синхроимпульс", "Поиск Предыдущий синхроимпульс",
            typeof(MainCommands));
        public static RoutedUICommand SearchNextSynchroImpulse = new RoutedUICommand("Следующий синхроимпульс", "Поиск Следующий синхроимпульс",
            typeof(MainCommands));
        /*Операции*/
        public static RoutedUICommand GraphicalAnalisys = new RoutedUICommand("Графический анализ", "Открыть окно графического анализа",
            typeof(MainCommands));
        public static RoutedUICommand FormatView = new RoutedUICommand("Форматированный просмотр", "Открыть окно форматированного просмотра",
            typeof(MainCommands));
        public static RoutedUICommand SaveProtocolAsTxt = new RoutedUICommand("Сохранение протокола", "Сохранить протокола в *.txt",
            typeof(MainCommands));
        /*Справка*/
        public static RoutedUICommand AboutApp = new RoutedUICommand("О программе", "Информация о программе",
            typeof(MainCommands));
    }
}
