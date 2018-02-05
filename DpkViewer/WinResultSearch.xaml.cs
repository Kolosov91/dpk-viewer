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
using ListStringViewWPF;
using DPK;
using DpkViewer.Tools;

namespace DpkViewer
{
    /// <summary>
    /// Логика взаимодействия для WinResultSearch.xaml
    /// </summary>
    public partial class WinResultSearch : Window
    {
        WinDpkMain win { get; set; }
        public WinResultSearch()
        {
            InitializeComponent();
            this.listStringViewResult.BlockSize = new BlockSizeTemplate(20, new List<int> { 50, 140, 100, 250 });
            this.listStringViewResult.BorderColor = SystemColors.ActiveBorderBrush;
            this.listStringViewResult.HeaderTemplate = new DataListTemplate(new ViewTemplate(
                new LinearGradientBrush(new GradientStopCollection() { new GradientStop(Colors.WhiteSmoke, 0), new GradientStop(SystemColors.ActiveCaptionColor, 1) },
                    new Point(0, 0), new Point(0, 1)),
                "Calibri", 16, Brushes.Black),
                new List<string> { "№", "Время [Ч:М:С:МС]", "Адрес [1-8]", "Данные [9-32]" });
            this.listStringViewResult.SourceDataElementConvert = SourceDataElementConvert;
            this.listStringViewResult.ClickItem += new EventHandler<ListStringView.ClickDataItemEventArgs>(listStringViewResult_ClickItem);
        }

        void listStringViewResult_ClickItem(object sender, ListStringView.ClickDataItemEventArgs e)
        {
            int index = win.listStringViewDpkWords.SourceData.IndexOf(e.Item);
            win.listStringViewDpkWords.Select(index);
        }

        public WinResultSearch(Window owner, List<object> items, SearchTemplate_2 searchT):this()
        { 
            this.Owner = owner; this.listStringViewResult.AddRangeToSourceData(items);
            win = (WinDpkMain)this.Owner;
            labelResultAddresses.Content = "Адреса:";
            foreach (int adr in searchT.ListAddresses)
                labelResultAddresses.Content += " " + adr.ToString() + ";";
            /*подчёркиваем значивые позиции - Выделяем */
            string strVal = Service.ConvertFrom(searchT.Value);
            for (int i = 0; i < searchT.Check.Count; i++)
                if (searchT.Check[i])
                {
                    textBlock_Data.TextEffects.Add(new TextEffect(null, Brushes.Green, null, i, 1));
                }
                /**/
            textBlock_Data.Text = strVal;
        }

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

        private void Cancel(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        private void Next(object sender, ExecutedRoutedEventArgs e)
        {
            int nextInd = ((this.listStringViewResult.IndexChoosenElementSourceData + 1) == this.listStringViewResult.SourceData.Count) ? this.listStringViewResult.IndexChoosenElementSourceData : this.listStringViewResult.IndexChoosenElementSourceData + 1;
            this.listStringViewResult.Select(nextInd);
            int index = win.listStringViewDpkWords.SourceData.IndexOf(this.listStringViewResult.SourceData[nextInd]);
            win.listStringViewDpkWords.Select(index);
        }

        private void Previous(object sender, ExecutedRoutedEventArgs e)
        {
            int nextInd = ((this.listStringViewResult.IndexChoosenElementSourceData - 1) == -1) ? this.listStringViewResult.IndexChoosenElementSourceData : this.listStringViewResult.IndexChoosenElementSourceData - 1;
            this.listStringViewResult.Select(nextInd);
            int index = win.listStringViewDpkWords.SourceData.IndexOf(this.listStringViewResult.SourceData[nextInd]);
            win.listStringViewDpkWords.Select(index);
        }
    }

    public class WinResultSearchCommands
    {
        public static RoutedUICommand Cancel = new RoutedUICommand("Закрыть", "Закрыть результаты поиска", typeof(WinResultSearchCommands));
        public static RoutedUICommand Next = new RoutedUICommand("Следующий", "Выбрать следующий пункт списка", typeof(WinResultSearchCommands));
        public static RoutedUICommand Previous = new RoutedUICommand("Предыдущий", "Выбрать предыдущий пункт списка", typeof(WinResultSearchCommands));
    }
}
