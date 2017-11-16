﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using WtsXamarin.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WtsXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListViewListPage : ContentPage
    {
        public ObservableCollection<string> Items { get; set; }

        public ListViewListPage()
        {
            InitializeComponent();
            BindingContext = new ListViewListViewModel();
        }
    }
}
