using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastLearner.Models
{
    //Core, Reading, etc.
    class CardGroup
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int Unit { get; set; }
        public int Lesson { get; set; }
        public int Activity { get; set; }
        public string Path { get; set; }

        public CardGroup(string path)
        {
            this.Path = path;
            Name = System.IO.Path.GetFileName(path);

            string[] values = Name.Split('_');
            if (values.Count() == 3)
            {
                try
                {
                    Level = int.Parse(values[0]);
                    Unit = int.Parse(values[1]);
                    Lesson = int.Parse(values[2]);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
