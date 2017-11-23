﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Windows;
using System.Windows.Input;
using Microsoft.Templates.UI.V2Services;
using Microsoft.Templates.UI.V2ViewModels.NewProject;

namespace Microsoft.Templates.UI.V2Views.NewProject
{
    public partial class WizardShell : Window
    {
        public UserSelection Result { get; set; }

        public WizardShell(string language)
        {
            DataContext = MainViewModel.Instance;
            InitializeComponent();
            NavigationService.Initialize(mainFrame, new MainPage());
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }
}
