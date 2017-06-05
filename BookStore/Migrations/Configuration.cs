using BookStore.Models;

namespace BookStore.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BookStore.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "BookStore.Models.ApplicationDbContext";
        }

        protected override void Seed(BookStore.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //

            //Author author1 = new Author { FirstName = "Alexander", LastName = "Pushkin", Rate = 0 };
            //Author author2 = new Author { FirstName = "Name", LastName = "LastName", Rate = 0 };
            //Author author3 = new Author { FirstName = "Asd", LastName = "Qwe", Rate = 0 };
            //context.Authors.Add(author1);
            //context.Authors.Add(author2);
            //context.Authors.Add(author3);

            //Genre genre1 = new Genre { Name = "Genre 1" };
            //context.Genres.Add(genre1);

            //context.Books.Add(new Book { Name = "Book 1",ImagePath = "~/BookImages/DefaultImage.png", Genre = genre1, Author = author1, Rate = 0, Amount = 100, Price = 250 });

        }
    }
}
