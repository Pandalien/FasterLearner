using FastLearner.Data;
using FastLearner.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FastLearner.ViewModels
{
    class LessonViewModel : INotifyPropertyChanged
    {
        #region Properties
        public ObservableCollection<Card> Cards { get; set; }
        public string selectedPath { get; set; }

        #endregion
        public event PropertyChangedEventHandler PropertyChanged;

        #region Public methods
        public LessonViewModel(string resourceFolder)
        {
            this.selectedPath = resourceFolder;
            Cards = new ObservableCollection<Card>();
            RefreshCommand();
        }

        public void RefreshCommand()
        {
            ResourceReader rr = new ResourceReader();
            Cards.Clear();
            rr.getResource(selectedPath, Cards);
        }

        public void Play()
        {

        }
        #endregion

        #region Private methods
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }

        //call this in getter and setter
        private void SetProperty<T>(ref T field, T value, [CallerMemberName] string name = "")
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                var handler = PropertyChanged;
                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(name));
                }
            }
        }
        #endregion
    }
}
