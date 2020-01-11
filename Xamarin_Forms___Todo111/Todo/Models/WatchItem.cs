using System;
using System.Collections.Generic;
using System.Text;
using SQLite;


namespace Todo.Models
{
    public class WatchItem
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public bool Done { get; set; }
    }
}
