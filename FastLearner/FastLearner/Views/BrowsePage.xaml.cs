using FastLearner.Data;
using FastLearner.Pages;
using FastLearner.ViewModels;
using Splat;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FastLearner
{
    public partial class BrowsePage : ContentPage
    {
        CardDataBase db;
        BrowseViewModel vm;

        public BrowsePage()
        {
            InitializeComponent();

            db = new CardDataBase();
            //btnTest.Clicked += BtnTest_Clicked;
            vm = new BrowseViewModel();
            this.BindingContext = vm;
            vm.LessonSeleted += Vm_LessonSeleted;
            //var imgSrc1 = ImageSource.FromFile("C:\\Users\\Administrator\\Documents\\Labs\\RosettaStoneUtils\\!NewRecorded\\1_1_1\\img\\1_1.jpg");
            var imgSrc1 = ImageSource.FromFile(@"C:\Users\Public\Pictures\3_2.jpg");
            ImgTest1.Source = imgSrc1;

            var imgSrc2 = ImageSource.FromFile("C:\\Users\\Administrator\\AppData\\Local\\Packages\\d9713359-84b2-41dc-a3a7-15b37902ab8f_75cr2b68sm664\\LocalState\\1_1.jpg");
            ImgTest2.Source = imgSrc2;

            btnTest.Clicked += BtnTest_Clicked;
        }

        private async void Vm_LessonSeleted(object sender, string e)
        {
            //var imgSrc = ImageSource.FromFile("C:\\Users\\Administrator\\AppData\\Local\\Packages\\d9713359-84b2-41dc-a3a7-15b37902ab8f_75cr2b68sm664\\LocalState\\1_1.jpg");
            //ImgTest.Source = imgSrc;
            await Navigation.PushModalAsync(new LessonPage(e));
        }

        private async void BtnTest_Clicked(object sender, EventArgs e)
        {
            //db.SaveItem(new Card() { Level = 1, ReviewTime = DateTime.Now });

            //var items = db.GetItems().ToList();
            //if (items.Count > 0)
            //{
            //    btnTest.Text = items.Count + items[0].ReviewTime.ToString();
            //}

            string path = await DependencyService.Get<IFileReader>().PickFile();
            await DisplayAlert("Info", path, "Ok");

            var file = await PCLStorage.FileSystem.Current.GetFileFromPathAsync(path);
            using (var opened = await file.OpenAsync(PCLStorage.FileAccess.Read))
            {
                using (var sr = new StreamReader(opened))
                {
                    string text = await sr.ReadToEndAsync();
                    await DisplayAlert("Info", text, "Ok");
                }
            }
            //var opened = await file.OpenAsync(PCLStorage.FileAccess.Read);
            //BitmapLoader.Current.
            //var imgSrc1 = ImageSource.FromFile(path);
            //ImageSource.F
            //ImgTest1.Source = imgSrc1;
        }
    }
}
