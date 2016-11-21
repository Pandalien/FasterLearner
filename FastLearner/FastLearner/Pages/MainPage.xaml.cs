using FastLearner.Data;
using FastLearner.Pages;
using FastLearner.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FastLearner
{
    public partial class MainPage : ContentPage
    {
        CardDataBase db;
        BrowseViewModel vm;

        public MainPage()
        {
            InitializeComponent();

            db = new CardDataBase();
            //btnTest.Clicked += BtnTest_Clicked;
            vm = new BrowseViewModel();
            this.BindingContext = vm;
            vm.LessonSeleted += Vm_LessonSeleted;
            imgTest.Source = ImageSource.FromFile("C:\\Users\\Andy\\Documents\\Labs\\RosettaStone\\1_1_1\\img\\1_1.jpg");
        }

        private async void Vm_LessonSeleted(object sender, Models.StringEventArgs e)
        {
            await Navigation.PushModalAsync(new ListPage(e.Value));
        }

        private void BtnTest_Clicked(object sender, EventArgs e)
        {
            //db.SaveItem(new Card() { Level = 1, ReviewTime = DateTime.Now });

            //var items = db.GetItems().ToList();
            //if (items.Count > 0)
            //{
            //    btnTest.Text = items.Count + items[0].ReviewTime.ToString();
            //}

            
        }
    }
}
