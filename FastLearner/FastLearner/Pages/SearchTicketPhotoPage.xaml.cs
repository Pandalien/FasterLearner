using FastLearner.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace FastLearner.Pages
{
    public partial class SearchTicketPhotoPage : ContentPage
    {
        SearchTicketPhotoViewModel vm;

        public SearchTicketPhotoPage()
        {
            InitializeComponent();
            vm = new SearchTicketPhotoViewModel();
            BindingContext = vm;

            btnGoBack.Clicked += async (s, e) => { await Navigation.PopModalAsync(); };
        }
    }
}
