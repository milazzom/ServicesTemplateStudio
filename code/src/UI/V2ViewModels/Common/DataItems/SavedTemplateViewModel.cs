﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.TemplateEngine.Abstractions;
using Microsoft.Templates.Core;
using Microsoft.Templates.Core.Mvvm;
using Microsoft.Templates.UI.V2Controls;
using Microsoft.Templates.UI.V2Extensions;
using Microsoft.Templates.UI.V2Services;
using Microsoft.Templates.UI.V2ViewModels.NewProject;

namespace Microsoft.Templates.UI.V2ViewModels.Common
{
    public class SavedTemplateViewModel : Observable
    {
        private string _name;
        private string _icon;
        private bool _itemNameEditable;
        private bool _isFocused;
        private bool _hasErrors;
        private ICommand _textChangedCommand;
        private ICommand _lostKeyboardFocusCommand;

        public ITemplateInfo Template { get; }

        public string Identity { get; }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string Icon
        {
            get => _icon;
            set => SetProperty(ref _icon, value);
        }

        public bool ItemNameEditable
        {
            get => _itemNameEditable;
            set => SetProperty(ref _itemNameEditable, value);
        }

        public bool IsFocused
        {
            get => _isFocused;
            set => SetProperty(ref _isFocused, value);
        }

        public bool HasErrors
        {
            get => _hasErrors;
            set => SetProperty(ref _hasErrors, value);
        }

        public ICommand TextChangedCommand => _textChangedCommand ?? (_textChangedCommand = new RelayCommand<TextChangedEventArgs>(OnTextChanged));

        public ICommand LostKeyboardFocusCommand => _lostKeyboardFocusCommand ?? (_lostKeyboardFocusCommand = new RelayCommand<KeyboardFocusChangedEventArgs>(OnLostKeyboardFocus));

        public SavedTemplateViewModel(TemplateInfoViewModel template)
        {
            Template = template.Template;
            Identity = template.Identity;
            Icon = template.Icon;
            ItemNameEditable = template.ItemNameEditable;
        }

        public void Focus()
        {
            IsFocused = true;
        }

        private void OnTextChanged(TextChangedEventArgs args)
        {
            var textBox = args.Source as TextBox;
            if (textBox != null)
            {
                if (ItemNameEditable)
                {
                    var validationResult = ValidationService.ValidateTemplateName(textBox.Text, ItemNameEditable, true);
                    HasErrors = !validationResult.IsValid;
                    MainViewModel.Instance.WizardStatus.HasValidationErrors = !validationResult.IsValid;
                    if (validationResult.IsValid)
                    {
                        NotificationsControl.Instance.CleanNotificationsAsync(Category.NamingValidation).FireAndForget();
                    }
                    else
                    {
                        NotificationsControl.Instance.AddNotificationAsync(validationResult.GetNotification()).FireAndForget();
                    }

                    Name = textBox.Text;
                }
            }
        }

        private void OnLostKeyboardFocus(KeyboardFocusChangedEventArgs args)
        {
            if (HasErrors)
            {
                var textBox = args.Source as TextBox;
                textBox.Focus();
            }
        }

        public override bool Equals(object obj)
        {
            var result = false;
            if (obj is SavedTemplateViewModel savedTemplate)
            {
                result = Identity.Equals(savedTemplate.Identity);
            }
            else if (obj is TemplateInfoViewModel templateInfo)
            {
                result = Identity.Equals(templateInfo.Identity);
            }

            return result;
        }

        public override int GetHashCode() => base.GetHashCode();

#pragma warning disable SA1008 // Opening parenthesis must be spaced correctly - StyleCop can't handle Tuples
        public (string name, ITemplateInfo template) GetUserSelection() => (Name, Template);
#pragma warning restore SA1008 // Opening parenthesis must be spaced correctly
    }
}
