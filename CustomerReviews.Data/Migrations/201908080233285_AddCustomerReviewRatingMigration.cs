namespace CustomerReviews.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCustomerReviewRatingMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CustomerReview", "Rating", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CustomerReview", "Rating");
        }
    }
}
