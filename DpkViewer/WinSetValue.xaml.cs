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
    /// Логика взаимодействия для WinSetValue.xaml
    /// </summary>
    public partial class WinSetValue : Window
    {
        List<int> listValue { get; set; }
        public int Value {
            get 
            { 
                int value = 0;
                for (int i = 0; i <= listValue.Count - 1; i++)
                {
                    value = value << 4;
                    value |= listValue[i]; }
                return value;
            }
        }
        public WinSetValue()
        {
            InitializeComponent();
            listValue = new List<int>();
        }
        public WinSetValue(Window owner):this()
        { this.Owner = owner; }

        private void AcceptParametres(object sender, ExecutedRoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
        private void CancelParametres(object sender, ExecutedRoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        void Update()
        {
            textBlockValue.Text = "0x" + Value.ToString("X");
        }

        private void Clear(object sender, ExecutedRoutedEventArgs e)
        {
            listValue.Clear();
            Update();
        }
        private void Backspace(object sender, ExecutedRoutedEventArgs e)
        {
            if (listValue.Count != 0)
                listValue.RemoveAt(listValue.Count - 1);
            Update();
        }

        private void Num_0(object sender, ExecutedRoutedEventArgs e)
        {
            if (listValue.Count == 8) return;
            listValue.Add(0);
            Update();
        }
        private void Num_1(object sender, ExecutedRoutedEventArgs e)
        {
            if (listValue.Count == 8) return;
            listValue.Add(1);
            Update();
        }

        private void Num_2(object sender, ExecutedRoutedEventArgs e)
        {
            if (listValue.Count == 8) return;
            listValue.Add(2);
            Update();
        }

        private void Num_3(object sender, ExecutedRoutedEventArgs e)
        {
            if (listValue.Count == 8) return;
            listValue.Add(3);
            Update();
        }

        private void Num_4(object sender, ExecutedRoutedEventArgs e)
        {
            if (listValue.Count == 8) return;
            listValue.Add(4);
            Update();
        }

        private void Num_5(object sender, ExecutedRoutedEventArgs e)
        {
            if (listValue.Count == 8) return;
            listValue.Add(5);
            Update();
        }

        private void Num_6(object sender, ExecutedRoutedEventArgs e)
        {
            if (listValue.Count == 8) return;
            listValue.Add(6);
            Update();
        }

        private void Num_7(object sender, ExecutedRoutedEventArgs e)
        {
            if (listValue.Count == 8) return;
            listValue.Add(7);
            Update();
        }

        private void Num_8(object sender, ExecutedRoutedEventArgs e)
        {
            if (listValue.Count == 8) return;
            listValue.Add(8);
            Update();
        }

        private void Num_9(object sender, ExecutedRoutedEventArgs e)
        {
            if (listValue.Count == 8) return;
            listValue.Add(9);
            Update();
        }

        private void Num_A(object sender, ExecutedRoutedEventArgs e)
        {
            if (listValue.Count == 8) return;
            listValue.Add(0xA);
            Update();
        }

        private void Num_B(object sender, ExecutedRoutedEventArgs e)
        {
            if (listValue.Count == 8) return;
            listValue.Add(0xB);
            Update();
        }

        private void Num_C(object sender, ExecutedRoutedEventArgs e)
        {
            if (listValue.Count == 8) return;
            listValue.Add(0xC);
            Update();
        }

        private void Num_D(object sender, ExecutedRoutedEventArgs e)
        {
            if (listValue.Count == 8) return;
            listValue.Add(0xD);
            Update();
        }

        private void Num_E(object sender, ExecutedRoutedEventArgs e)
        {
            if (listValue.Count == 8) return;
            listValue.Add(0xE);
            Update();
        }

        private void Num_F(object sender, ExecutedRoutedEventArgs e)
        {
            if (listValue.Count == 8) return;
            listValue.Add(0xF);
            Update();
        }
    }

    public class WinSetValueCommands
    {
        public static RoutedUICommand AcceptParametres = new RoutedUICommand("Выбрать", "Принять параметры выбора", typeof(WinSetValueCommands));
        public static RoutedUICommand CancelParametres = new RoutedUICommand("Закрыть", "Отменить параметры выбора", typeof(WinSetValueCommands));

        public static RoutedUICommand Clear = new RoutedUICommand("Сброс", "Сбросить значения", typeof(WinSetValueCommands));
        public static RoutedUICommand Backspace = new RoutedUICommand("Бэкспесйс", "Стереть один символ", typeof(WinSetValueCommands));

        public static RoutedUICommand Num_0 = new RoutedUICommand("0", "Ввод символа", typeof(WinSetValueCommands));
        public static RoutedUICommand Num_1 = new RoutedUICommand("1", "Ввод символа", typeof(WinSetValueCommands));
        public static RoutedUICommand Num_2 = new RoutedUICommand("2", "Ввод символа", typeof(WinSetValueCommands));
        public static RoutedUICommand Num_3 = new RoutedUICommand("3", "Ввод символа", typeof(WinSetValueCommands));
        public static RoutedUICommand Num_4 = new RoutedUICommand("4", "Ввод символа", typeof(WinSetValueCommands));
        public static RoutedUICommand Num_5 = new RoutedUICommand("5", "Ввод символа", typeof(WinSetValueCommands));
        public static RoutedUICommand Num_6 = new RoutedUICommand("6", "Ввод символа", typeof(WinSetValueCommands));
        public static RoutedUICommand Num_7 = new RoutedUICommand("7", "Ввод символа", typeof(WinSetValueCommands));
        public static RoutedUICommand Num_8 = new RoutedUICommand("8", "Ввод символа", typeof(WinSetValueCommands));
        public static RoutedUICommand Num_9 = new RoutedUICommand("9", "Ввод символа", typeof(WinSetValueCommands));
        public static RoutedUICommand Num_A = new RoutedUICommand("A", "Ввод символа", typeof(WinSetValueCommands));
        public static RoutedUICommand Num_B = new RoutedUICommand("B", "Ввод символа", typeof(WinSetValueCommands));
        public static RoutedUICommand Num_C = new RoutedUICommand("C", "Ввод символа", typeof(WinSetValueCommands));
        public static RoutedUICommand Num_D = new RoutedUICommand("D", "Ввод символа", typeof(WinSetValueCommands));
        public static RoutedUICommand Num_E = new RoutedUICommand("E", "Ввод символа", typeof(WinSetValueCommands));
        public static RoutedUICommand Num_F = new RoutedUICommand("F", "Ввод символа", typeof(WinSetValueCommands));
    }
}
