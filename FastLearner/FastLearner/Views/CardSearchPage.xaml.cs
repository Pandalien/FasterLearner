using FastLearner.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace FastLearner.Pages
{
    public partial class CardSearchPage : ContentPage
    {
        private CardSearchViewModel vm;

        public CardSearchPage()
        {
            InitializeComponent();

            vm = new CardSearchViewModel();
            BindingContext = vm;
        }
    }
}
