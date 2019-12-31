using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FastLearner
{
    public interface IFileReader
    {
        //get the path for all lessons
        Task<string> GetResourceDirectory();

        Task<string> PickFolder();
        Task<string> PickFile();

        //get a list of lessons
        string[] GetLessonDirectories(string path);

        //get a list of files in specified path
        string[] GetLessonImageFiles(string path);

        ImageSource GetImageSource(string path);
    }
}
