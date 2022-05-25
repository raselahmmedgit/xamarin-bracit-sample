using lab.XAPP2.ViewModels;
using lab.XAPP2.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace lab.XAPP2
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}
