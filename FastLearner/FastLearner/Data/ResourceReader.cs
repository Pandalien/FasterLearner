using FastLearner.Models;
using PCLStorage;
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

        //public async Task<string[]> getLessons()
        //{
        //    if (string.IsNullOrEmpty(resFolder))
        //    {
        //        resFolder = await fileReader.GetResourceDirectory();
        //    }

        //    string[] folders = fileReader.GetLessonDirectories(resFolder);
        //    return folders;
        //}

        public async Task<List<string>> getLessons()
        {
            if (string.IsNullOrEmpty(resFolder))
            {
                resFolder = await fileReader.GetResourceDirectory();
            }

            var folder = await FileSystem.Current.GetFolderFromPathAsync(resFolder);
            if (folder != null)
            {
                var list = await folder.GetFoldersAsync();
                return list.Select(s => s.Path).ToList();

            }
            //string[] folders = fileReader.GetLessonDirectories(resFolder);
            return null;
        }

        public async Task PCLStorageSample()
        {
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            IFolder folder = await rootFolder.CreateFolderAsync("MySubFolder",
                CreationCollisionOption.OpenIfExists);
            IFile file = await folder.CreateFileAsync("answer.txt",
                CreationCollisionOption.ReplaceExisting);
            await file.WriteAllTextAsync("42");
        }

        public void getResource(string lesson, ObservableCollection<Card> cards)
        {
            //Task task = new Task(() => {
            //    string imgPath = Path.Combine(lesson, "img");
            //    string[] images = fileReader.GetLessonImageFiles(imgPath);

            //    foreach (var item in images)
            //    {
            //        Card card = new Card(lesson, Path.GetFileNameWithoutExtension(item));
            //        cards.Add(card);
            //    }
            //});

            //await task.ConfigureAwait(true);

            string imgPath = Path.Combine(lesson, "img");
            string[] images = fileReader.GetLessonImageFiles(imgPath);

            foreach (var item in images)
            {
                Card card = new Card(lesson, Path.GetFileNameWithoutExtension(item));
                cards.Add(card);
            }
        }
    }
}
