using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace FastLearner.Pages
{
    public partial class MainTicketPage : ContentPage
    {
        public MainTicketPage()
        {
            InitializeComponent();
            btnCreate.Clicked += BtnCreate_Clicked;
            btnSearch.Clicked += BtnSearch_Clicked;
        }

        private async void BtnSearch_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new SearchTicketPhotoPage());
        }

        private  async void BtnCreate_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AddTicketPhotoPage());
        }
    }
}
