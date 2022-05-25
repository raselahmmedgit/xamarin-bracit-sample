using lab.XAPP1.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace lab.XAPP1.Views
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