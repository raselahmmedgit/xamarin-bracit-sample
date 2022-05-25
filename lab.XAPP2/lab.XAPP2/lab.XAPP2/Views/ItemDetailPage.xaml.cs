using lab.XAPP2.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace lab.XAPP2.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}