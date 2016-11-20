using FastLearner.Models;
using SQLite;
using System;

namespace FastLearner.Data
{
    internal class DBData
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; internal set; }
        public int Level { get; set; }
        public int Unit { get; set; }
        public int Lesson { get; set; }
        public int Activity { get; set; }
        public int Index { get; set; }
        public DateTime ReviewTime { get; set; }
        public int Rate { get; set; }
    }
}