using FastLearner.Data;
using FastLearner.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FastLearner.ViewModels
{
    class SearchTicketPhotoViewModel
    {
        #region Properties
        public ICommand SearchCommand { get; set; }
        public ICommand DropTableCommand { get; set; }

        public ObservableCollection<TicketPhoto> Images { get; }
        public string Query { get; set; }
        public TicketPhoto SelectedItem { get; set; }
        #endregion

        private TicketPhotoDataBase db;

        public SearchTicketPhotoViewModel()
        {
            db = new TicketPhotoDataBase();

            Images = new ObservableCollection<TicketPhoto>();
            SearchCommand = new Command (() => { Search(); });
            DropTableCommand = new Command (() => { DropTable(); });

            Query = "sky";
        }

        private void Search()
        {
            Images.Clear();
            foreach (var item in db.GetItemsByDescription(Query))
            {
                Images.Add(item);
            }
        }

        private void DropTable()
        {
            var items = db.GetItems();
            foreach (var item in items)
            {
                db.DeleteItem(item.ID);
            }
        }
    }
}
