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
using Microsoft.Win32;
using System.IO;
using DpkViewerTools.Descryptor;
using ListBlockViewLib.BlockTemplate;
using DPK;
using System.Windows.Controls.Primitives;
using DpkViewer.Tools;
using ControlsLibrary;

namespace DpkViewer
{
    /// <summary>
    /// Логика взаимодействия для WinFormatView.xaml
    /// </summary>
    public partial class WinFormatView : Window
    {
        WinDpkMain main { get; set; }
        /*Подробное инфо о слове*/
        Popup InfoDpkTooltip { get; set; }
        DpkView DpkViewer { get; set; }
        /**/
        readonly DpkViewerApp App = ((DpkViewerApp)DpkViewerApp.Current);

        string FullNameDescryptorFile { get; set; }
        int Status { get; set; }

        FDParser fdParser { get; set; }

        public static class StatusConst
        {
            public const int CODE_NOT_CHOOSE = 0;
            public const int CODE_NOT_IDENTIFY = 1;
            public const int CODE_GOOD_IDENTIFY = 2;
            /**/
            public const string STR_NOT_CHOOSE = "Не выбран файл";
            public const string STR_NOT_IDENTIFY = "Файл НЕ распознан";
            public const string STR_GOOD_IDENTIFY = "Файл распознан";
            /**/
            public static readonly Color CLR_NOT_CHOOSE = Colors.Yellow;
            public static readonly Color CLR_NOT_IDENTIFY = Colors.Red;
            public static readonly Color CLR_GOOD_IDENTIFY = Colors.Green;
        }

        public static class TextConst
        {
            public const string Status = "Статус:\n";
            /***/
            public const string NoChoose = "Не выбран Файл";
            public const string NotIdentify = "Файл НЕ распознан";
            public const string GoodIdentify = "Файл распознан";
        }

        public WinFormatView()
        {
            InitializeComponent();
            Status = StatusConst.CODE_NOT_CHOOSE;
            fdParser = new FDParser();
            listBlockView_Protocol.ConvertToBlockTemplate = Convert;
            listBlockView_Protocol.ClickItem += new EventHandler<ListBlockViewLib.ListBlockView.ClickDataItemEventArgs>(listBlockView_Protocol_ClickItem);
            /*PopupInit*/
            InfoDpkTooltip = new Popup();
            InfoDpkTooltip.MouseUp += new MouseButtonEventHandler(InfoDpkTooltip_MouseUp);
            //Создание ДПК слова
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

        public WinFormatView(Window win):this()
        {
            main = (WinDpkMain)win;
        }

        void listBlockView_Protocol_ClickItem(object sender, ListBlockViewLib.ListBlockView.ClickDataItemEventArgs e)
        {
            if (e.MouseEventArg.ChangedButton == MouseButton.Left)
            {
                int index = -1;
                for (int i = 0; i < main.listStringViewDpkWords.SourceData.Count; i++)
                {
                    DpkWordItem item = (DpkWordItem)e.Item;
                    DpkWordItem current = (DpkWordItem)main.listStringViewDpkWords.SourceData[i];
                    if (item.Time.Equals(current.Time) && item.ADR.Equals(current.ADR) && item.DATA.Equals(current.DATA) && item.Flags.Equals(current.Flags))
                    {
                        index = i;
                        break;
                    }
                }
                if (index >= 0)
                    main.listStringViewDpkWords.Select(index);
            }
            /*При клике по слову из списка - создавать и открывать всплывающую подсказку с подробным инфо о слове*/
            if (e.MouseEventArg.ChangedButton == MouseButton.Right)
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

        void InfoDpkTooltip_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ((Popup)sender).IsOpen = false;//При клике по подробному инфо о слове - закрываем его
        }

        BlockTemplateBase Convert(object item, int index)
        {
            BlockTemplateListString block = new BlockTemplateListString();          
            if (item is DpkWordItem)
            {
                DpkWordItem word = (DpkWordItem)item;
                List<string> lines = new List<string>();
                foreach (FormatDescryptorWord fdw in fdParser.Descryptors)
                {
                    if (Service.IsEqual(fdw.Address, Service.ConvertFromInt(word.ADR,8)))
                    {
                        lines.Add(fdw.GetText(index, word));
                        foreach (ILineDescryptor lineDescr in fdw.ListLines)
                        {
                            string text = lineDescr.GetText(word);
                            if (text.Length == 0) continue;
                            lines.Add(text);
                        }
                    }
                }
                //форматирование текста
                if ((word.Flags & DpkDataConstants.SYNC_FLAG) > 0)
                {
                    block.ColorBackground = Brushes.Yellow;
                    block.ColorBorder = block.ColorBackground;
                }
                else if ((index % 2) == 0)
                {
                    block.ColorBackground = Brushes.WhiteSmoke;
                    block.ColorBorder = block.ColorBackground;
                }
                else
                {
                    block.ColorBackground = Brushes.LightGray;
                    block.ColorBorder = block.ColorBackground;
                }
                block.ColorFont = Brushes.Black;
                block.FontName = "Courier New";
                block.FontSize = 11;
                //расчёт размеров
                        /*поиск наибольшей ширины*/
                        int maxW = 1;
                        for(int i=0;i<lines.Count;i++)
                            if (lines[i].Length > maxW) maxW = lines[i].Length;
                block.HeightLine = 11;
                block.RenderSize = new Size(maxW * 7, block.HeightLine * lines.Count);
                block.Strings = lines;
            }
            return block;
        }

        void UpdateStatus()
        {
            switch (Status)
            {
                case StatusConst.CODE_NOT_CHOOSE:
                    {
                        txtBlock_Status.Text = TextConst.Status + TextConst.NoChoose;
                        TextDecoration textDecor = new TextDecoration(TextDecorationLocation.Underline, new Pen(Brushes.Yellow, 1), -1, TextDecorationUnit.Pixel, TextDecorationUnit.Pixel);
                        txtBlock_Status.TextDecorations.Add(textDecor);
                        bt_Save.IsEnabled = false;
                    }
                    break;
                case StatusConst.CODE_NOT_IDENTIFY:
                    {
                        txtBlock_Status.Text = TextConst.Status + TextConst.NotIdentify;
                        TextDecoration textDecor = new TextDecoration(TextDecorationLocation.Underline, new Pen(Brushes.Red, 1), -1, TextDecorationUnit.Pixel, TextDecorationUnit.Pixel);
                        txtBlock_Status.TextDecorations.Add(textDecor);
                        bt_Save.IsEnabled = false;
                    }
                    break;
                case StatusConst.CODE_GOOD_IDENTIFY:
                    {
                        txtBlock_Status.Text = TextConst.Status + TextConst.GoodIdentify;
                        TextDecoration textDecor = new TextDecoration(TextDecorationLocation.Underline, new Pen(Brushes.Green, 1), -1, TextDecorationUnit.Pixel, TextDecorationUnit.Pixel);
                        txtBlock_Status.TextDecorations.Add(textDecor);
                        bt_Save.IsEnabled = true;
                    }
                    break;
            }
            txtBlock_CurrentDescryptionFileName.Text = FullNameDescryptorFile;
        }

        private void SaveProtolocInTxt(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog win = new SaveFileDialog();
            win.Filter = "Текстовый файл|*.txt";
            win.Title = "Сохранить текстовый протокол";
            if (win.ShowDialog(this).Equals(false)) return;
            using (StreamWriter sw = new StreamWriter(win.FileName, false, Encoding.GetEncoding(1251)))
            {
                sw.WriteLine("*** Дата создания файла: " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " ***");
                sw.WriteLine(String.Format("*** Исходный файл протокола ДПК: '{0}' ***", App.CurrentFileName));
                sw.WriteLine(String.Format("*** Файл с описателем формата вывода: '{0}' ***", FullNameDescryptorFile));
                sw.WriteLine("***** ***** ***** ***** *****");
                for (int i = 0; i < this.listBlockView_Protocol.SourceData.Count; i++)
                {
                    DpkWordItem word = (DpkWordItem)this.listBlockView_Protocol.SourceData[i];
                    List<string> lines = new List<string>();
                    foreach (FormatDescryptorWord fdw in fdParser.Descryptors)
                    {
                        if (Service.IsEqual(fdw.Address, Service.ConvertFromInt(word.ADR, 8)))
                        {
                            lines.Add(fdw.GetText(i, word));
                            foreach (ILineDescryptor lineDescr in fdw.ListLines)
                            {
                                string text = lineDescr.GetText(word);
                                if (text.Length == 0) continue;
                                lines.Add(text);
                            }
                        }
                    }
                    foreach (string str in lines) sw.WriteLine(str);
                    sw.WriteLine("----- ----- -----");
                }
                sw.Close();
            }
        }

        private void OpenDescryptorFile(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog win = new OpenFileDialog();
            win.Multiselect = false;
            win.Filter = "Файлы с описателем формата вывода|*.fdwld";
            win.Title = "Открыть файл с описателем формата вывода";
            Status = StatusConst.CODE_NOT_CHOOSE;
            FullNameDescryptorFile = "";
            if (win.ShowDialog(this).Equals(false)) goto end;
            FullNameDescryptorFile = win.FileName;
            using (StreamReader sr = new StreamReader(FullNameDescryptorFile, Encoding.GetEncoding(1251)))
            {
                List<string> strings = new List<string>();
                while (!sr.EndOfStream)
                {
                    strings.Add(sr.ReadLine());
                }
                sr.Close();
                fdParser.Parse(strings);
                if (fdParser.IsValidParse)
                {
                    Status = StatusConst.CODE_GOOD_IDENTIFY;
                    List<object> words = new List<object>();
                    List<object> srcW = App.DpkLogFile.GetBuf();
                    foreach (DpkWordItem word in srcW)
                    {
                        foreach (FormatDescryptorWord fdw in fdParser.Descryptors)
                            if (Service.IsEqual(fdw.Address, Service.ConvertFromInt(word.ADR, 8))) { words.Add(word); break; }
                    }
                    listBlockView_Protocol.AddRangeToSourceData(words);
                }
                else
                {
                    Status = StatusConst.CODE_NOT_IDENTIFY;
                    listBlockView_Protocol.ClearSourceData(); ;
                }
            }
            end:
            UpdateStatus();
        }

        private void Clear(object sender, ExecutedRoutedEventArgs e)
        {
            FullNameDescryptorFile = "";
            Status = StatusConst.CODE_NOT_CHOOSE;
            UpdateStatus();
            bt_Save.IsEnabled = false;
        }
    }

    public class FormatViewCommands
    {
        public static RoutedUICommand OpenDescryptorFile = new RoutedUICommand("Открыть файл", "Открыть файл с описателем формата вывода",
            typeof(FormatViewCommands));
        public static RoutedUICommand Clear = new RoutedUICommand("Сброс", "Сбросить выбранный файл",
            typeof(FormatViewCommands));
        public static RoutedUICommand SaveProtolocInTxt = new RoutedUICommand("Сохранить протокол", "Сохранить протокол в текстовом виде",
            typeof(FormatViewCommands));
    }
}
