﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Templates.UI.Controls;
using Microsoft.Templates.UI.Services;
using Microsoft.Templates.UI.ViewModels.NewProject;
using Microsoft.Templates.UI.Views.Common;

namespace Microsoft.Templates.UI.Views.NewProject
{
    public partial class WizardShell : Window
    {
        private string _language;

        public static WizardShell Current { get; private set; }

        public UserSelection Result { get; set; }

        public MainViewModel ViewModel { get; }

        public WizardShell(string language, BaseStyleValuesProvider provider)
        {
            Current = this;
            _language = language;
            ViewModel = new MainViewModel(this, provider);
            DataContext = ViewModel;
            InitializeComponent();
            NavigationService.InitializeMainFrame(mainFrame, new MainPage());
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
                return;
            }

            if (e.Key == Key.Back
                && NavigationService.CanGoBackMainFrame
                && sender is WizardShell shell
                && shell.mainFrame.NavigationService.Content is TemplateInfoPage)
            {
                NavigationService.GoBackMainFrame();
            }
        }

        public async Task LoadAsync() => await MainViewModel.Instance.InitializeAsync(_language);

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OnMouseLeftButtonDown(e);
            DragMove();
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            await LoadAsync();
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            ViewModel.UnsuscribeEventHandlers();
            NotificationsControl.UnsuscribeEventHandlers();
        }
    }
}
