using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }

        public Genre()
        {
            Books = new List<Book>();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}