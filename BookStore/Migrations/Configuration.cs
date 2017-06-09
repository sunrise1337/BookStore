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

            //Author author1 = new Author { FirstName = "Александр", LastName = "Пушкин", Rate = 0 };
            //Author author2 = new Author { FirstName = "Николай", LastName = "Гоголь", Rate = 0 };
            //Author author3 = new Author { FirstName = "Иван", LastName = "Тургенев", Rate = 0 };
            //Author author4 = new Author { FirstName = "Антон", LastName = "Чехов", Rate = 0 };
            //context.Authors.Add(author1);
            //context.Authors.Add(author2);
            //context.Authors.Add(author3);
            //context.Authors.Add(author4);

            //Genre genre1 = new Genre { Name = "Поэма" };
            //Genre genre2 = new Genre { Name = "Роман" };
            //Genre genre3 = new Genre { Name = "Комедия" };
            //Genre genre4 = new Genre { Name = "Пьеса" };
            //Genre genre5 = new Genre { Name = "Рассказ" };
            //context.Genres.Add(genre1);
            //context.Genres.Add(genre2);
            //context.Genres.Add(genre3);
            //context.Genres.Add(genre4);
            //context.Genres.Add(genre5);

            //context.Books.Add(new Book { Name = "Евгений Онегин", ImagePath = "~/BookImages/DefaultImage.png", Genre = genre2, Author = author1, Rate = 100, Amount = 100, Price = 250 });
            //context.Books.Add(new Book { Name = "Ревизор", ImagePath = "~/BookImages/DefaultImage.png", Genre = genre3, Author = author2, Rate = 100, Amount = 100, Price = 250 });
            //context.Books.Add(new Book { Name = "Горе от ума", ImagePath = "~/BookImages/DefaultImage.png", Genre = genre3, Author = author2, Rate = 100, Amount = 100, Price = 250 });
            //context.Books.Add(new Book { Name = "Вишнёвый сад", ImagePath = "~/BookImages/DefaultImage.png", Genre = genre4, Author = author4, Rate = 100, Amount = 100, Price = 250 });
            //context.Books.Add(new Book { Name = "Мёртвые души", ImagePath = "~/BookImages/DefaultImage.png", Genre = genre1, Author = author2, Rate = 100, Amount = 100, Price = 250 });
            //context.Books.Add(new Book { Name = "Отцы и дети", ImagePath = "~/BookImages/DefaultImage.png", Genre = genre2, Author = author3, Rate = 100, Amount = 100, Price = 250 });
            //context.Books.Add(new Book { Name = "Муму", ImagePath = "~/BookImages/DefaultImage.png", Genre = genre5, Author = author3, Rate = 100, Amount = 100, Price = 250 });
        }
    }
}
