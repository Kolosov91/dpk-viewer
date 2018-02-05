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

namespace DpkViewer
{
    /// <summary>
    /// Логика взаимодействия для WinChooseAddresses.xaml
    /// </summary>
    public partial class WinChooseAddresses : Window
    {
        public List<int> ListChoosenAddresses { get; protected set; }
        public WinChooseAddresses()
        {
            InitializeComponent();
        }
        public WinChooseAddresses(Window owner):this()
        { this.Owner = owner; CreateChoosenListAddresses(); }

        void CreateChoosenListAddresses()
        {
            DpkViewerApp App = (DpkViewerApp)Application.Current;
            for (int i = App.ListAddressesInDpkLogFile.Count - 1; i >= 0; i--)
            {
                CheckBox item = new CheckBox();
                item.Content = "0x" + App.ListAddressesInDpkLogFile[i].ToString("X").PadLeft(2, '0') + " [" + App.ListAddressesInDpkLogFile[i].ToString() + "]";
                item.Tag = (int)i;
                item.FontFamily = new System.Windows.Media.FontFamily("Courier New");
                item.FontSize = 16;
                this.spAddresses.Children.Add(item);
            }
        }
        void SaveChoosenListAddresses()
        {
            DpkViewerApp App = (DpkViewerApp)Application.Current;
            ListChoosenAddresses = new List<int>();
            foreach (CheckBox item in spAddresses.Children)
            {
                if (item.IsChecked.Equals(true))
                    ListChoosenAddresses.Add(App.ListAddressesInDpkLogFile[(int)item.Tag]);
            }
        }

        private void AcceptParametres(object sender, ExecutedRoutedEventArgs e)
        {
            SaveChoosenListAddresses();
            this.DialogResult = true;
            this.Close();
        }

        private void CancelParametres(object sender, ExecutedRoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void ChooseAllAddresses(object sender, ExecutedRoutedEventArgs e)
        {
            foreach (CheckBox item in this.spAddresses.Children)
                item.IsChecked = true;
        }

        private void ResetAllAddresses(object sender, ExecutedRoutedEventArgs e)
        {
            foreach (CheckBox item in this.spAddresses.Children)
                item.IsChecked = false;
        }
    }

    public class WinChooseAddressesCommands
    {
        public static RoutedUICommand AcceptParametres = new RoutedUICommand("Выбрать", "Принять параметры выбора", typeof(WinChooseAddressesCommands));
        public static RoutedUICommand CancelParametres = new RoutedUICommand("Закрыть", "Отменить параметры выбора", typeof(WinChooseAddressesCommands));
        public static RoutedUICommand ChooseAllAddresses = new RoutedUICommand("Выбрать все", "Выбрать все адреса", typeof(WinChooseAddressesCommands));
        public static RoutedUICommand ResetAllAddresses = new RoutedUICommand("Сбросить все", "Сбросить все адреса", typeof(WinChooseAddressesCommands));
    }
}
