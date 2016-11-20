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
using System.Windows.Input;
using Xamarin.Forms;

namespace FastLearner.ViewModels
{
    class BrowseViewModel : INotifyPropertyChanged
    {
        #region ICommands
        public ICommand RefreshCommand { get; set; }

        #endregion

        #region Properties
        public ObservableCollection<CardGroup> CardGroups { get; protected set; }
        public string Title { get; set; }
        private CardGroup _SelectedItem;

        public CardGroup SelectedItem
        {
            get { return _SelectedItem; }
            set
            {
                if (value != _SelectedItem)
                {
                    _SelectedItem = value;
                    OnRaiseLessonSelected(value.Path);
                }
            }
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        public event StringEventHandler LessonSeleted;

        #region Public methods

        public BrowseViewModel()
        {
            CardGroups = new ObservableCollection<CardGroup>();
            Title = "FastLearner";

            RefreshCommand = new Command(async () => { await GetLessons(); });
        }

        public async Task GetLessons()
        {
            ResourceReader rr = new ResourceReader();
            CardGroups.Clear();

            string[] lessons = await rr.getLessons();

            foreach (var item in lessons)
            {
                CardGroups.Add(new CardGroup(item));
            }

            OnPropertyChanged("CardGroups");

            Title = "Done";
            OnPropertyChanged("Title");
        }
        #endregion


        #region Private methods
        protected virtual void OnRaiseLessonSelected(string value)
        {
            var handler = LessonSeleted;
            if (handler != null)
            {
                var e = new StringEventArgs(value);
                handler(this, e);
            }
        }

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
