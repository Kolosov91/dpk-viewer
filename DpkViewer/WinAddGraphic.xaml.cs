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
using System.Collections.ObjectModel;

namespace DpkViewer
{
    /// <summary>
    /// Логика взаимодействия для WinAddGraphic.xaml
    /// </summary>
    public partial class WinAddGraphic : Window
    {
        public int Address { get; protected set; }
        public int NumBit { get; protected set; }

        ObservableCollection<int> Addresses = new ObservableCollection<int>();
        ObservableCollection<int> NumBits = new ObservableCollection<int>();

        public WinAddGraphic() 
        { 
            InitializeComponent();
            DpkViewerApp App = (DpkViewerApp)Application.Current;
            foreach (int adr in App.ListAddressesInDpkLogFile) { Addresses.Add(adr); }
            for (int i = 0; i < 24; i++) { NumBits.Add(i); }
            comboBox_Addresses.ItemsSource = Addresses;
            comboBox_NumBits.ItemsSource = NumBits;
        }

        public WinAddGraphic(Window owner) : this() { this.Owner = owner; }

        private void AcceptParametres(object sender, ExecutedRoutedEventArgs e)
        {
            Address = Addresses[comboBox_Addresses.SelectedIndex];
            NumBit = NumBits[comboBox_NumBits.SelectedIndex];
            this.DialogResult = true;
            this.Close();
        }

        private void CancelParametres(object sender, ExecutedRoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void CanAcceptParametres(object sender, CanExecuteRoutedEventArgs e)
        {
            if ((comboBox_Addresses.SelectedIndex == -1) || (comboBox_NumBits.SelectedIndex == -1))
                e.CanExecute = false;
            else
                e.CanExecute = true;
        }
    }

    public class WinAddGraphicCommands
    {
        public static RoutedUICommand AcceptParametres = new RoutedUICommand("Выбрать", "Принять параметры выбора", typeof(WinAddGraphicCommands));
        public static RoutedUICommand CancelParametres = new RoutedUICommand("Закрыть", "Отменить параметры выбора", typeof(WinAddGraphicCommands));
    }
}
