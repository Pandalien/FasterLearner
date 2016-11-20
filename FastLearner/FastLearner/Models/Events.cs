using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastLearner.Models
{
    public class StringEventArgs : EventArgs
    {
        public StringEventArgs(string str)
        {
            _value = str;
        }

        private string _value;

        public string Value
        {
            get { return _value; }
        }

    }

    public delegate void StringEventHandler(object sender, StringEventArgs e);
}
