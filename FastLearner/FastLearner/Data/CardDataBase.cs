using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FastLearner.Data
{
    class CardDataBase
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
        public CardDataBase()
        {
            //obtain an implementation and get a valid SQLite database connection for iOS, Android, & Windows 
            database = DependencyService.Get<ISQLite>().GetConnection();
            // create the tables
            database.CreateTable<DBData>();
        }

        public IEnumerable<DBData> GetItems()
        {
            lock (locker)
            {
                return (from i in database.Table<DBData>() select i).ToList();
            }
        }

        public IEnumerable<DBData> GetItemsNotDone()
        {
            lock (locker)
            {
                return database.Query<DBData>("SELECT * FROM [Card] WHERE [Done] = 0");
            }
        }

        public DBData GetItem(int id)
        {
            lock (locker)
            {
                return database.Table<DBData>().FirstOrDefault(x => x.ID == id);
            }
        }

        public int SaveItem(DBData item)
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
                return database.Delete<DBData>(id);
            }
        }
    }
}
