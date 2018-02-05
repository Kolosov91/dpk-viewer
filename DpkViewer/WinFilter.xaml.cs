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
    /// Логика взаимодействия для WinFilter.xaml
    /// </summary>
    public partial class WinFilter : Window
    {
        public WinFilter()
        {
            InitializeComponent();
        }
        public WinFilter(Window owner) : this()
        {
            this.Owner = owner;
            CreateListAddresses();
        }
        void CreateListAddresses()
        {
            DpkViewerApp App = (DpkViewerApp)Application.Current;
            for (int i = App.ListAddressesInDpkLogFile.Count - 1; i >= 0; i--)
            {
                CheckBox item = new CheckBox();
                item.Content = "0x" + App.ListAddressesInDpkLogFile[i].ToString("X").PadLeft(2, '0') + " [" + App.ListAddressesInDpkLogFile[i].ToString()+ "]";
                item.Tag = (int)i;
                item.FontFamily = new System.Windows.Media.FontFamily("Courier New");
                item.FontSize = 16;
                item.IsChecked = App.ListFilteredAddresses.Contains(App.ListAddressesInDpkLogFile[i]);
                this.spAddresses.Children.Add(item);
            }
        }
        void SaveListAddresses()
        {
            DpkViewerApp App = (DpkViewerApp)Application.Current;
            App.ListFilteredAddresses.Clear();
            foreach (CheckBox item in spAddresses.Children)
            {
                if (item.IsChecked.Equals(true))
                    App.ListFilteredAddresses.Add(App.ListAddressesInDpkLogFile[(int)item.Tag]);
            }
        }

        private void WinFilterCommands_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SaveListAddresses();
            this.DialogResult = true;
            this.Close();
        }
        private void CancelParametres_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
        private void ChooseAllAddresses_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            foreach (CheckBox item in this.spAddresses.Children)
                item.IsChecked = true;
        }
        private void ResetAllAddresses_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            foreach (CheckBox item in this.spAddresses.Children)
                item.IsChecked = false;
        }
    }

    public class WinFilterCommands
    {
        public static RoutedUICommand AcceptParametres = new RoutedUICommand("Принять", "Принять параметры фильтрации", typeof(WinFilterCommands));
        public static RoutedUICommand CancelParametres = new RoutedUICommand("Закрыть", "Отменить параметры фильтрации", typeof(WinFilterCommands));
        public static RoutedUICommand ChooseAllAddresses = new RoutedUICommand("Выбрать все", "Выбрать все адреса", typeof(WinFilterCommands));
        public static RoutedUICommand ResetAllAddresses = new RoutedUICommand("Сбросить все", "Сбросить все адреса", typeof(WinFilterCommands));
    }
}
