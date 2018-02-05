﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using Microsoft.Templates.UI.V2Resources;

namespace Microsoft.Templates.UI.V2ViewModels.Common
{
    public class ErrorDialogViewModel : BaseDialogViewModel
    {
        private string _showDetails;

        public ErrorDialogViewModel(Exception ex)
        {
            Title = StringRes.ErrorDialogTitle;
            Description = ex.Message;
            ErrorStackTrace = ex.ToString();
        }

        public string ErrorStackTrace { get; set; }

        public string ShowDetails
        {
            get => _showDetails;
            set => SetProperty(ref _showDetails, value);
        }
    }
}
