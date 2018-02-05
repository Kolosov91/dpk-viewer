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
using DpkViewer.Tools;

namespace DpkViewer
{
    /// <summary>
    /// Логика взаимодействия для WinSearch.xaml
    /// </summary>
    public partial class WinSearch : Window
    {
        SearchTemplate_2 searchTemplate { get; set; }

        public WinSearch()
        { 
            InitializeComponent();
            /**/
            binView_Check.CreateValues(24);
            binView_Check.SetVisibleText(false);
            binView_Check.SetColors(new LinearGradientBrush(new GradientStopCollection() { new GradientStop(Colors.WhiteSmoke, 0), new GradientStop(Colors.Green, 1) },
                    new Point(0, 0), new Point(0, 1)),
                    new LinearGradientBrush(new GradientStopCollection() { new GradientStop(Colors.WhiteSmoke, 0), new GradientStop(Colors.LightGray, 1) },
                    new Point(0, 0), new Point(0, 1)));
            /**/
            binView_DataValue.CreateValues(24);
            binView_DataValue.SetVisibleText(true);
            binView_DataValue.SetColors(new LinearGradientBrush(new GradientStopCollection() { new GradientStop(Colors.WhiteSmoke, 0), new GradientStop(Colors.Yellow, 1) },
                    new Point(0, 0), new Point(0, 1)),
                    new LinearGradientBrush(new GradientStopCollection() { new GradientStop(Colors.WhiteSmoke, 0), new GradientStop(Colors.LightGray, 1) },
                    new Point(0, 0), new Point(0, 1)));
            binView_DataValue.SetFirstNumber(9);
        }
        public WinSearch(Window owner):this()
        { this.Owner = owner; searchTemplate = new SearchTemplate_2(); }

        public SearchTemplate_2 GetSearchTemplate() { return searchTemplate; }

        private void AcceptParametres(object sender, ExecutedRoutedEventArgs e)
        {
            searchTemplate.Value = binView_DataValue.GetValues();
            searchTemplate.Check = binView_Check.GetValues();
            this.DialogResult = true;
            this.Close();
        }

        private void CancelParametres(object sender, ExecutedRoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void SetAddresses(object sender, ExecutedRoutedEventArgs e)
        {
            WinChooseAddresses win = new WinChooseAddresses(this);
            if (win.ShowDialog().Equals(true))
            {
                searchTemplate.ListAddresses.Clear();
                searchTemplate.AddListAddresses(win.ListChoosenAddresses);
                textBlockAddressValue.Text = "";
                foreach (int adr in searchTemplate.ListAddresses)
                {
                    textBlockAddressValue.Text += adr.ToString() + "; ";
                }
            }
        }
    }

    public class WinSearchCommands
    {
        public static RoutedUICommand AcceptParametres = new RoutedUICommand("Принять", "Принять параметры поиска", typeof(WinSearchCommands));
        public static RoutedUICommand CancelParametres = new RoutedUICommand("Закрыть", "Отменить параметры поиска", typeof(WinSearchCommands));
        public static RoutedUICommand SetAddresses = new RoutedUICommand("Задать адреса", "Задать адреса для поиска", typeof(WinSearchCommands));
    }
}
