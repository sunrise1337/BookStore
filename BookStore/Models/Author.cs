using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Rate { get; set; }

        public virtual ICollection<Book> Books { get; set; }

        public Author()
        {
            Books = new List<Book>();
        }

        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
    }
}