﻿using System.Windows.Input;
using WtsXPlat.Core.Helpers;
using WtsXPlat.Core.Models;
using WtsXPlat.Mobile.Services;
using Xamarin.Forms;

namespace WtsXPlat.Mobile.ViewModels
{
    public class ListViewItemViewModel : Observable
    {
        private SampleOrder _item;
        public ICommand _goBackCommand;

        public ListViewItemViewModel(SampleOrder item)
        {
            Item = item;
        }

        public SampleOrder Item
        {
            get => _item;
            set => Set(ref _item, value);
        }

        public ICommand GoBackCommand
        {
            get => _goBackCommand ?? (_goBackCommand = new Command(async () => await NavigationService.Instance.GoBack()));
        }
    }
}
