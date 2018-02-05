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
using DPK;
using DpkViewer.Tools;

namespace ControlsLibrary
{
    /// <summary>
    /// Логика взаимодействия для DpkView.xaml
    /// </summary>
    public partial class DpkView : UserControl
    {
        public DpkView()
        {
            InitializeComponent();
            this.binView_Address.SetFirstNumber(1);
            this.binView_Data.SetFirstNumber(9);
        }
        public void SetDpkWord(DpkWordItem word)
        {
            binView_Address.SetValues(Service.ConvertFromInt(word.ADR, 8));
            binView_Data.SetValues(Service.ConvertFromInt(word.DATA, 24));
        }
    }
}
