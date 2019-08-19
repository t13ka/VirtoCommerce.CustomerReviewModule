namespace CustomerReviews.Core.Model
{
    using VirtoCommerce.Domain.Commerce.Model.Search;

    public class CustomerReviewSearchCriteria : SearchCriteriaBase
    {
        public bool? IsActive { get; set; }

        public string[] ProductIds { get; set; }
    }
}