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

            //Author author1 = new Author { FirstName = "���������", LastName = "������", Rate = 0 };
            //Author author2 = new Author { FirstName = "�������", LastName = "������", Rate = 0 };
            //Author author3 = new Author { FirstName = "����", LastName = "��������", Rate = 0 };
            //Author author4 = new Author { FirstName = "�����", LastName = "�����", Rate = 0 };
            //context.Authors.Add(author1);
            //context.Authors.Add(author2);
            //context.Authors.Add(author3);
            //context.Authors.Add(author4);

            //Genre genre1 = new Genre { Name = "�����" };
            //Genre genre2 = new Genre { Name = "�����" };
            //Genre genre3 = new Genre { Name = "�������" };
            //Genre genre4 = new Genre { Name = "�����" };
            //Genre genre5 = new Genre { Name = "�������" };
            //context.Genres.Add(genre1);
            //context.Genres.Add(genre2);
            //context.Genres.Add(genre3);
            //context.Genres.Add(genre4);
            //context.Genres.Add(genre5);

            //context.Books.Add(new Book { Name = "������� ������", ImagePath = "~/BookImages/DefaultImage.png", Genre = genre2, Author = author1, Rate = 100, Amount = 100, Price = 250 });
            //context.Books.Add(new Book { Name = "�������", ImagePath = "~/BookImages/DefaultImage.png", Genre = genre3, Author = author2, Rate = 100, Amount = 100, Price = 250 });
            //context.Books.Add(new Book { Name = "���� �� ���", ImagePath = "~/BookImages/DefaultImage.png", Genre = genre3, Author = author2, Rate = 100, Amount = 100, Price = 250 });
            //context.Books.Add(new Book { Name = "������� ���", ImagePath = "~/BookImages/DefaultImage.png", Genre = genre4, Author = author4, Rate = 100, Amount = 100, Price = 250 });
            //context.Books.Add(new Book { Name = "̸����� ����", ImagePath = "~/BookImages/DefaultImage.png", Genre = genre1, Author = author2, Rate = 100, Amount = 100, Price = 250 });
            //context.Books.Add(new Book { Name = "���� � ����", ImagePath = "~/BookImages/DefaultImage.png", Genre = genre2, Author = author3, Rate = 100, Amount = 100, Price = 250 });
            //context.Books.Add(new Book { Name = "����", ImagePath = "~/BookImages/DefaultImage.png", Genre = genre5, Author = author3, Rate = 100, Amount = 100, Price = 250 });
        }
    }
}
