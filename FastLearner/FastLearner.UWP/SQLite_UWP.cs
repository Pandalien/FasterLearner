using FastLearner.UWP;
using System;
using System.IO;
using Windows.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_UWP))]

namespace FastLearner.UWP
{
    public class SQLite_UWP : ISQLite
    {
        public SQLite_UWP()
        {
        }
        #region ISQLite implementation
        public SQLite.SQLiteConnection GetConnection()
        {
            var sqliteFilename = "TodoSQLite.db3";
            string path = Path.Combine(ApplicationData.Current.LocalFolder.Path, sqliteFilename);

            var conn = new SQLite.SQLiteConnection(path);

            // Return the database connection 
            return conn;
        }
        #endregion
    }
}
