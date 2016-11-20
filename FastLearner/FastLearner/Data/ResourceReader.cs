using FastLearner.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FastLearner.Data
{
    class ResourceReader
    {
        string resFolder;
        IFileReader fileReader;

        public ResourceReader()
        {
            //resFolder = Device.OnPlatform<string>("", "", "C:\\Users\\Administrator\\Documents\\Labs\\RosettaStoneUtils\\!NewRecorded");
            fileReader = DependencyService.Get<IFileReader>();

            //resFolder = fileReader.GetResourceDirectory();
        }

        public async Task<string[]> getLessons()
        {
            if (string.IsNullOrEmpty(resFolder))
            {
                resFolder = await fileReader.GetResourceDirectory();
            }

            string[] folders = DependencyService.Get<IFileReader>().GetLessonDirectories(resFolder);
            return folders;
        }

        public void getResource(string lesson, ObservableCollection<Card> cards)
        {
            string imgPath = Path.Combine(lesson, "img");
            string[] images = DependencyService.Get<IFileReader>().GetLessonImageFiles(imgPath);

            foreach (var item in images)
            {
                cards.Add(new Card(lesson, Path.GetFileNameWithoutExtension(item)));
            }
        }
    }
}
