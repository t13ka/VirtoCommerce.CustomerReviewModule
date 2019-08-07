namespace CustomerReviews.Core.Model
{
    using VirtoCommerce.Platform.Core.Common;

    public class CustomerReview : AuditableEntity
    {
        public string AuthorNickname { get; set; }

        public string Content { get; set; }

        public bool IsActive { get; set; }

        public string ProductId { get; set; }
    }
}