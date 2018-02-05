using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using DPK;

namespace DpkViewer
{
    public partial class DpkViewerApp : Application
    {
        WinDpkMain winDpkMain { get; set; }
        const string MainWindowTitle = "Просмотр протокола ДПК";

        public DpkDataBuf DpkLogFile { get; protected set; }
        public string CurrentFileName { get; protected set; }
        public List<int> ListAddressesInDpkLogFile { get; protected set; }
        public List<int> ListFilteredAddresses { get; set; }

        public DpkViewerApp()
        {
            DpkLogFile = new DpkDataBuf();
            ListAddressesInDpkLogFile = new List<int>();
            ListFilteredAddresses = new List<int>();
        }

        public void LoadDpkLogFile(string fullNameFile)
        {
            CurrentFileName = fullNameFile;
            WinSplash splash = new WinSplash();
            splash.Owner = winDpkMain;
            splash.Show();
            winDpkMain.textBlock_CurrentNameFile.Text = fullNameFile;
            string[] nameFile = fullNameFile.Split('\\');
            winDpkMain.Title = MainWindowTitle + " - " + nameFile[nameFile.Length - 1];
            DpkLogFile.Clear();
            DpkLogFile.LoadFromFile(fullNameFile);
            for(int i = DpkLogFile.Count - 1; i >= 0; i--)
            {
                int currentAddress = DpkLogFile[i].ADR;
                if (!ListAddressesInDpkLogFile.Contains(currentAddress))
                    ListAddressesInDpkLogFile.Add(currentAddress);                
            }
            ListFilteredAddresses.Clear();
            ListFilteredAddresses.AddRange(ListAddressesInDpkLogFile);
            splash.Close();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            winDpkMain = new WinDpkMain();
            this.MainWindow = winDpkMain;
            this.MainWindow.Show();
            if ((e.Args != null) && (e.Args.Length == 1))
            {
                LoadDpkLogFile(e.Args[0]);
                winDpkMain.FillListDpkWord();
            }
            else
                CurrentFileName = null;
        }
    }
}
