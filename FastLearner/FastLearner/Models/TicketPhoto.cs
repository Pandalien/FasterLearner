using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastLearner.Models
{
    class TicketPhoto
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; internal set; }
        public string Image { get; set; }
        public string Description { get; set; }
    }
}
