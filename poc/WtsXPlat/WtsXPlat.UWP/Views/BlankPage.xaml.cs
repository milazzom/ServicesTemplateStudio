﻿using System;

using Windows.UI.Xaml.Controls;

using WtsXPlat.UWP.ViewModels;

namespace WtsXPlat.UWP.Views
{
    public sealed partial class BlankPage : Page
    {
        public BlankViewModel ViewModel { get; } = new BlankViewModel();

        public BlankPage()
        {
            InitializeComponent();
        }
    }
}
