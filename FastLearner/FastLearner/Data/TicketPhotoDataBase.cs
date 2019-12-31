using FastLearner.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FastLearner.Data
{
    class TicketPhotoDataBase
    {
        static object locker = new object();

        SQLiteConnection database;

        /// <summary>
        /// Initializes a new instance of the <see cref="Tasky.DL.TaskDatabase"/> TaskDatabase. 
        /// if the database doesn't exist, it will create the database and all the tables.
        /// </summary>
        /// <param name='path'>
        /// Path.
        /// </param>
        public TicketPhotoDataBase()
        {
            //obtain an implementation and get a valid SQLite database connection for iOS, Android, & Windows 
            database = DependencyService.Get<ISQLite>().GetConnection();
            // create the tables
            database.CreateTable<TicketPhoto>();
        }

        public IEnumerable<TicketPhoto> GetItems()
        {
            lock (locker)
            {
                return (from i in database.Table<TicketPhoto>() select i).ToList();
            }
        }

        public IEnumerable<TicketPhoto> GetItemsByDescription(string desc)
        {
            lock (locker)
            {
                return database.Query<TicketPhoto>("SELECT * FROM [TicketPhoto] WHERE [Description] LIKE '%" + desc + "%'");
            }
        }

        public TicketPhoto GetItem(int id)
        {
            lock (locker)
            {
                return database.Table<TicketPhoto>().FirstOrDefault(x => x.ID == id);
            }
        }

        public int SaveItem(TicketPhoto item)
        {
            lock (locker)
            {
                if (item.ID != 0)
                {
                    database.Update(item);
                    return item.ID;
                }
                else {
                    return database.Insert(item);
                }
            }
        }

        public int DeleteItem(int id)
        {
            lock (locker)
            {
                return database.Delete<TicketPhoto>(id);
            }
        }
    }
}
