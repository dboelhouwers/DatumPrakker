﻿#pragma checksum "..\..\..\Pages\DPFillinPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "115C78ED47FE0D7A7F81F127870070B7FC0949CF6F7A767E3E9304330B203CE6"
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
using WPFClient.Pages;


namespace WPFClient.Pages {
    
    
    /// <summary>
    /// DPFillinPage
    /// </summary>
    public partial class DPFillinPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 27 "..\..\..\Pages\DPFillinPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label roomID;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\Pages\DPFillinPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label nameTXTBox;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\Pages\DPFillinPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox Date1;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\Pages\DPFillinPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox Date2;
        
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
            System.Uri resourceLocater = new System.Uri("/WPFClient;component/pages/dpfillinpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\DPFillinPage.xaml"
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
            this.roomID = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.nameTXTBox = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.Date1 = ((System.Windows.Controls.CheckBox)(target));
            
            #line 41 "..\..\..\Pages\DPFillinPage.xaml"
            this.Date1.Checked += new System.Windows.RoutedEventHandler(this.Date_Check_Handlr);
            
            #line default
            #line hidden
            
            #line 41 "..\..\..\Pages\DPFillinPage.xaml"
            this.Date1.Unchecked += new System.Windows.RoutedEventHandler(this.Date_Check_Handlr);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Date2 = ((System.Windows.Controls.CheckBox)(target));
            
            #line 42 "..\..\..\Pages\DPFillinPage.xaml"
            this.Date2.Checked += new System.Windows.RoutedEventHandler(this.Date_Check_Handlr);
            
            #line default
            #line hidden
            
            #line 42 "..\..\..\Pages\DPFillinPage.xaml"
            this.Date2.Unchecked += new System.Windows.RoutedEventHandler(this.Date_Check_Handlr);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 50 "..\..\..\Pages\DPFillinPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_Confirm);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

