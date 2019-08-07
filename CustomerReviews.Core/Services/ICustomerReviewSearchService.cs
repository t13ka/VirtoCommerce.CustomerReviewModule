namespace CustomerReviews.Core.Services
{
    using CustomerReviews.Core.Model;

    using VirtoCommerce.Domain.Commerce.Model.Search;

    public interface ICustomerReviewSearchService
    {
        GenericSearchResult<CustomerReview> SearchCustomerReviews(CustomerReviewSearchCriteria criteria);
    }
}