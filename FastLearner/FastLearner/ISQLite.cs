using SQLite;
using System;

/*
https://developer.xamarin.com/guides/xamarin-forms/working-with/databases/

*/
namespace FastLearner
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
