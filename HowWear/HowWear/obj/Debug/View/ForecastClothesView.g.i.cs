﻿#pragma checksum "..\..\..\View\ForecastClothesView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "2C7E8AED0AC079F5D2EF37FBC1DA48C6CF49A535015F291901F95BD2E8FA6A95"
//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.42000
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
// </auto-generated>
//------------------------------------------------------------------------------

using HowWear.View;
using HowWear.ViewModel;
using HowWear.ViewModel.Converter;
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


namespace HowWear.View {
    
    
    /// <summary>
    /// ForecastClothesView
    /// </summary>
    public partial class ForecastClothesView : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 69 "..\..\..\View\ForecastClothesView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tempTextBlock;
        
        #line default
        #line hidden
        
        
        #line 88 "..\..\..\View\ForecastClothesView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock locationTextBlock;
        
        #line default
        #line hidden
        
        
        #line 100 "..\..\..\View\ForecastClothesView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock descriptionTextBlock;
        
        #line default
        #line hidden
        
        
        #line 146 "..\..\..\View\ForecastClothesView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid Clothes;
        
        #line default
        #line hidden
        
        
        #line 156 "..\..\..\View\ForecastClothesView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button clothesPrevBtn;
        
        #line default
        #line hidden
        
        
        #line 189 "..\..\..\View\ForecastClothesView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button clothesNextBtn;
        
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
            System.Uri resourceLocater = new System.Uri("/HowWear;component/view/forecastclothesview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\ForecastClothesView.xaml"
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
            this.tempTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.locationTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.descriptionTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.Clothes = ((System.Windows.Controls.Grid)(target));
            return;
            case 5:
            this.clothesPrevBtn = ((System.Windows.Controls.Button)(target));
            return;
            case 6:
            this.clothesNextBtn = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

