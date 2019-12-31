using FastLearner.UWP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileReader_UWP))]
namespace FastLearner.UWP
{
    class FileReader_UWP : IFileReader
    {
        private const string dirToken = "PickedFolderToken";

        public async Task<string> PickFolder()
        {
            //foler picker
            var folderPicker = new Windows.Storage.Pickers.FolderPicker();
            folderPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.Desktop;
            folderPicker.FileTypeFilter.Add("*");

            Windows.Storage.StorageFolder folder = await folderPicker.PickSingleFolderAsync();
            if (folder != null)
            {
                // Application now has read/write access to all contents in the picked folder
                // (including other sub-folder contents)
                Windows.Storage.AccessCache.StorageApplicationPermissions.
                FutureAccessList.AddOrReplace(dirToken, folder);
                return folder.Path;
            }
            else
            {
                return null;
            }
        }
        public async Task<string> PickFile()
        {
            var filePick = new Windows.Storage.Pickers.FileOpenPicker();
            filePick.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            filePick.FileTypeFilter.Add("*");

            var file = await filePick.PickSingleFileAsync();
            if (file != null)
            {
                return file.Path;
            }

            return null;
        }

        public async Task<string> GetResourceDirectory()
        {
            //use last folder is exist
            //Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.
            Windows.Storage.StorageFolder lastFolder = null;
            try
            {
                lastFolder = await Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.GetFolderAsync(dirToken);
            }
            catch (Exception)
            {
            }

            if (lastFolder != null)
            {
                return lastFolder.Path;
            }

            //foler picker
            return await PickFolder();
        }

        public string[] GetLessonDirectories(string path)
        {
            return Directory.GetDirectories(path);
        }

        public string[] GetLessonImageFiles(string path)
        {
            return Directory.GetFiles(path, "*.jpg");
        }

        public ImageSource GetImageSource(string path)
        {
            if (File.Exists(path))
            {
                return ImageSource.FromFile(path);
            }

            return null;
        }
    }
}
