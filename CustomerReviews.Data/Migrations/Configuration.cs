namespace CustomerReviews.Data.Migrations
{
    using System.Data.Entity.Migrations;

    using CustomerReviews.Data.Repositories;

    public sealed class Configuration : DbMigrationsConfiguration<CustomerReviewRepository>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations";
        }

        protected override void Seed(CustomerReviewRepository context)
        {
            // This method will be called after migrating to the latest version.

            // You can use the DbSet<T>.AddOrUpdate() helper extension method 
            // to avoid creating duplicate seed data.
        }
    }
}