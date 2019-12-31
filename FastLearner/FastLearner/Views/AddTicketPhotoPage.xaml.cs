using FastLearner.Data;
using Microsoft.ProjectOxford.Vision;
using Microsoft.ProjectOxford.Vision.Contract;
using Plugin.Connectivity;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FastLearner.Pages
{
    public partial class AddTicketPhotoPage : ContentPage
    {
        private readonly VisionServiceClient visionClient;
        private TicketPhotoDataBase db;

        public AddTicketPhotoPage()
        {
            InitializeComponent();
            this.visionClient =
                new VisionServiceClient(ImageSearch.Services.CognitiveServicesKeys.ComputerVision);

            db = new TicketPhotoDataBase();

            btnGoBack.Clicked += async (s, e) => { await Navigation.PopModalAsync(); };
        }

        private async Task<AnalysisResult> AnalyzePictureAsync(Stream inputFile)
        {
            // Use the connectivity plugin to detect
            // if a network connection is available
            // Remember a using Plugin.Connectivit; directive
            if (!CrossConnectivity.Current.IsConnected)
            {
                await DisplayAlert("Network error",
                    "Please check your network connection and retry.", "OK");
                return null;
            }

            VisualFeature[] visualFeatures = new VisualFeature[] { VisualFeature.Adult,
                VisualFeature.Categories, VisualFeature.Color, VisualFeature.Description,
                VisualFeature.Faces, VisualFeature.ImageType, VisualFeature.Tags };

            AnalysisResult analysisResult =
                await visionClient.AnalyzeImageAsync(inputFile,
                visualFeatures);

            return analysisResult;
        }

        private async void TakePictureButton_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", "No camera available.", "OK");
                return;
            }

            string fileName = System.IO.Path.GetRandomFileName() + ".jpg";
            var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                SaveToAlbum = true,
                Name = fileName,
                PhotoSize = PhotoSize.Small
            });

            if (file == null)
                return;

            this.Indicator1.IsVisible = true;
            this.Indicator1.IsRunning = true;

            Image1.Source = ImageSource.FromStream(() => file.GetStream());
            var results = await AnalyzePictureAsync(file.GetStream());
            this.BindingContext = results;

            this.Indicator1.IsRunning = false;
            this.Indicator1.IsVisible = false;

            //Save picture to app folder
            
            db.SaveItem(new Models.TicketPhoto() { Description = results.Description.Captions[0].Text, Image = file.Path});
        }

        private async void UploadPictureButton_Clicked(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("No upload", "Picking a photo is not supported.", "OK");
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync();
            if (file == null)
                return;

            this.Indicator1.IsVisible = true;
            this.Indicator1.IsRunning = true;
            Image1.Source = ImageSource.FromStream(() => file.GetStream());

            try
            {
                this.BindingContext = await AnalyzePictureAsync(file.GetStream());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
                return;
            }
            finally
            {
                this.Indicator1.IsRunning = false;
                this.Indicator1.IsVisible = false;
            }
        }
    }
}
