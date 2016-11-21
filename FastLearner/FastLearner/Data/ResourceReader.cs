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

            string[] folders = fileReader.GetLessonDirectories(resFolder);
            return folders;
        }

        public void getResource(string lesson, ObservableCollection<Card> cards)
        {
            Task task = new Task(() => {
                string imgPath = Path.Combine(lesson, "img");
                string[] images = fileReader.GetLessonImageFiles(imgPath);

                foreach (var item in images)
                {
                    Card card = new Card(lesson, Path.GetFileNameWithoutExtension(item));
                    cards.Add(card);
                }
            });
            task.Start();
        }
    }
}
