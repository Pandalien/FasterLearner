using FastLearner.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace FastLearner.Pages
{
    public partial class ListPage : ContentPage
    {
        private LessonViewModel vm;

        public ListPage(string value)
        {
            InitializeComponent();

            vm = new LessonViewModel(value);
            BindingContext = vm;
            btnGoBack.Clicked += async (s,e) => { await Navigation.PopModalAsync(); };
        }
    }
}
