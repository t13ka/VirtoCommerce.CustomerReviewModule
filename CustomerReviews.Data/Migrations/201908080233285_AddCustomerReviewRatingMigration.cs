namespace CustomerReviews.Data.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddCustomerReviewRatingMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CustomerReview", "Rating", c => c.Single(nullable: false));
            Sql("UPDATE dbo.CustomerReview SET Rating = '0'");
        }
        
        public override void Down()
        {
            DropColumn("dbo.CustomerReview", "Rating");
        }
    }
}
