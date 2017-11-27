﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Windows.Controls;
using Microsoft.Templates.UI.V2ViewModels.Common;

namespace Microsoft.Templates.UI.V2Views.NewProject
{
    /// <summary>
    /// Interaction logic for TemplateInfoPage.xaml
    /// </summary>
    public partial class TemplateInfoPage : Page
    {
        public BasicInfoViewModel ViewModel { get; }

        public TemplateInfoPage(BasicInfoViewModel basicInfoViewModel)
        {
            ViewModel = basicInfoViewModel;
            DataContext = ViewModel;
            InitializeComponent();
        }
    }
}
