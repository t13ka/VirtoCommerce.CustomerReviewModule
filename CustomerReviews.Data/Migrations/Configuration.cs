namespace CustomerReviews.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using CustomerReviews.Data.Model;

    public sealed class Configuration : DbMigrationsConfiguration<CustomerReviews.Data.Repositories.CustomerReviewRepository>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations";
        }

        protected override void Seed(CustomerReviews.Data.Repositories.CustomerReviewRepository context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            var now = DateTime.UtcNow;
            context.AddOrUpdate(new CustomerReviewEntity { Id = "1", ProductId = "baa4931161214690ad51c50787b1ed94", CreatedDate = now, CreatedBy = "initial data seed", AuthorNickname = "Homer Simpson", Content = "Great!" });
            context.AddOrUpdate(new CustomerReviewEntity { Id = "2", ProductId = "e9de38b73c424db19f319c9538184d03", CreatedDate = now, CreatedBy = "initial data seed", AuthorNickname = "Jemmi Lanister", Content = "Nice" });
            context.AddOrUpdate(new CustomerReviewEntity { Id = "3", ProductId = "ec235043d51848249e90ef170c371a1c", CreatedDate = now, CreatedBy = "initial data seed", AuthorNickname = "Bill Gates", Content = "Do you like this" });
        }
    }
}
