using FastLearner.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace FastLearner.Pages
{
    public partial class LessonPage : ContentPage
    {
        private LessonViewModel vm;

        public LessonPage(string value)
        {
            InitializeComponent();

            vm = new LessonViewModel(value);
            BindingContext = vm;
            btnGoBack.Clicked += async (s,e) => { await Navigation.PopModalAsync(); };
        }
    }
}
