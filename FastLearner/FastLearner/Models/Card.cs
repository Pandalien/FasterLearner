using FastLearner.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FastLearner.Models
{
    class Card
    {
        public DBData ID { get; set; }
        public string Image { get; set; }
        public string Caption { get; set; }
        public string Text { get; set; }
        public string Audio { get; set; }
        public ImageSource ImageSrc { get; set; }

        public Card(string path, string nameBase)
        {
            Image = Path.Combine(path, "img", nameBase+".jpg");
            Caption = Path.Combine(path, "caption", nameBase+".jpg");
            Text = Path.Combine(path, "txt", nameBase+".txt");
            Audio = Path.Combine(path, "audio", nameBase+".mp3");

            ImageSrc = ImageSource.FromFile(Image);
        }
    }
}
