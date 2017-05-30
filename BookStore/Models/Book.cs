using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? GenreId{ get; set; }
        public virtual Genre Genre { get; set; }

        public int? AuthorId { get; set; }
        public virtual Author Author { get; set; }

        public int Rate { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
        public bool isBooked { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public Book()
        {
            Comments = new List<Comment>();
        }
    }
}