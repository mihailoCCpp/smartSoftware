﻿#pragma checksum "..\..\PodaciOKupcuProzor.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "DE1A1872AE1193D2173DBA25A62A70848247670BBC863274EFC549379A04FA6B"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
using Xceed.Wpf.AvalonDock;
using Xceed.Wpf.AvalonDock.Controls;
using Xceed.Wpf.AvalonDock.Converters;
using Xceed.Wpf.AvalonDock.Layout;
using Xceed.Wpf.AvalonDock.Themes;


namespace SmartSoftware {
    
    
    /// <summary>
    /// PodaciOKupcuProzor
    /// </summary>
    public partial class PodaciOKupcuProzor : System.Windows.Window, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 36 "..\..\PodaciOKupcuProzor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid Pretraga;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\PodaciOKupcuProzor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid PretragaInfo;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\PodaciOKupcuProzor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox PrikazDetaljaKorisnika;
        
        #line default
        #line hidden
        
        
        #line 94 "..\..\PodaciOKupcuProzor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox IzmenaDetaljaKorisnika;
        
        #line default
        #line hidden
        
        
        #line 128 "..\..\PodaciOKupcuProzor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox PrikazRezultataKorisnika;
        
        #line default
        #line hidden
        
        
        #line 184 "..\..\PodaciOKupcuProzor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnRegistracijaKorisnika;
        
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
            System.Uri resourceLocater = new System.Uri("/SmartSoftware;component/podaciokupcuprozor.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\PodaciOKupcuProzor.xaml"
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
            this.Pretraga = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            
            #line 44 "..\..\PodaciOKupcuProzor.xaml"
            ((System.Windows.Controls.TextBox)(target)).KeyUp += new System.Windows.Input.KeyEventHandler(this.TextBox_KeyUp);
            
            #line default
            #line hidden
            return;
            case 3:
            this.PretragaInfo = ((System.Windows.Controls.Grid)(target));
            return;
            case 4:
            this.PrikazDetaljaKorisnika = ((System.Windows.Controls.ListBox)(target));
            return;
            case 6:
            this.IzmenaDetaljaKorisnika = ((System.Windows.Controls.ListBox)(target));
            return;
            case 7:
            this.PrikazRezultataKorisnika = ((System.Windows.Controls.ListBox)(target));
            return;
            case 9:
            this.btnRegistracijaKorisnika = ((System.Windows.Controls.Button)(target));
            
            #line 184 "..\..\PodaciOKupcuProzor.xaml"
            this.btnRegistracijaKorisnika.Click += new System.Windows.RoutedEventHandler(this.btnRegistracijaKorisnika_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 5:
            
            #line 80 "..\..\PodaciOKupcuProzor.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnIzaberiKorisnika_Click);
            
            #line default
            #line hidden
            break;
            case 8:
            
            #line 133 "..\..\PodaciOKupcuProzor.xaml"
            ((System.Windows.Controls.Grid)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.GridPrikazRezultataKorisnika_MouseDown);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

