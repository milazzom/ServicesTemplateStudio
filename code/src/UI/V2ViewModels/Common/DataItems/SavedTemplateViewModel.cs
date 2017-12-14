﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.Templates.Core;
using Microsoft.Templates.Core.Mvvm;
using Microsoft.Templates.UI.Services;

namespace Microsoft.Templates.UI.V2ViewModels.Common
{
    public class SavedTemplateViewModel : Observable
    {
        private string _name;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public SavedTemplateViewModel()
        {
        }
    }
}
