﻿#pragma checksum "..\..\..\WinDpkMain.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "212F04E4EDD4B181EB1F8DC385D12306"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using DpkViewer;
using ListStringViewWPF;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace DpkViewer {
    
    
    /// <summary>
    /// WinDpkMain
    /// </summary>
    public partial class WinDpkMain : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 66 "..\..\..\WinDpkMain.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem menuItemFilterParametresSave;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\..\WinDpkMain.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem menuItemFilterParametresOpen;
        
        #line default
        #line hidden
        
        
        #line 118 "..\..\..\WinDpkMain.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem menuItemSaveProtocolAsTxt;
        
        #line default
        #line hidden
        
        
        #line 123 "..\..\..\WinDpkMain.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem menuItemSaveProtocolAsHtml;
        
        #line default
        #line hidden
        
        
        #line 130 "..\..\..\WinDpkMain.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem menuItemHelp;
        
        #line default
        #line hidden
        
        
        #line 135 "..\..\..\WinDpkMain.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem menuItemAboutApp;
        
        #line default
        #line hidden
        
        
        #line 179 "..\..\..\WinDpkMain.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock textBlock_CurrentNameFile;
        
        #line default
        #line hidden
        
        
        #line 181 "..\..\..\WinDpkMain.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ListStringViewWPF.ListStringView listStringViewDpkWords;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/DpkViewer;component/windpkmain.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\WinDpkMain.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 8 "..\..\..\WinDpkMain.xaml"
            ((DpkViewer.WinDpkMain)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 27 "..\..\..\WinDpkMain.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.OpenProtocolFile_Executed);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 28 "..\..\..\WinDpkMain.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.Exit_Executed);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 30 "..\..\..\WinDpkMain.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.FilterParametres_Executed);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 32 "..\..\..\WinDpkMain.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.Search_Executed);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 33 "..\..\..\WinDpkMain.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.SearchPreviousError);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 34 "..\..\..\WinDpkMain.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.SearchNextError);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 35 "..\..\..\WinDpkMain.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.SearchPreviousSynchroImpulse);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 36 "..\..\..\WinDpkMain.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.SearchNextSynchroImpulse);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 38 "..\..\..\WinDpkMain.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.GraphicalAnalisys);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 39 "..\..\..\WinDpkMain.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.FormatView);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 40 "..\..\..\WinDpkMain.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.SaveProtocolAsTxt);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 42 "..\..\..\WinDpkMain.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.AboutApp);
            
            #line default
            #line hidden
            return;
            case 14:
            this.menuItemFilterParametresSave = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 15:
            this.menuItemFilterParametresOpen = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 16:
            this.menuItemSaveProtocolAsTxt = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 17:
            this.menuItemSaveProtocolAsHtml = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 18:
            this.menuItemHelp = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 19:
            this.menuItemAboutApp = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 20:
            this.textBlock_CurrentNameFile = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 21:
            this.listStringViewDpkWords = ((ListStringViewWPF.ListStringView)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
