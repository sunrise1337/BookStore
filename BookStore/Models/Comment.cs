using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Time { get; set; }

        public int? UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public int? BookId { get; set; }
        public virtual Book Book { get; set; }
    }
}