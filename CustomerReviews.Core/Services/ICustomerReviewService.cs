namespace CustomerReviews.Core.Services
{
    using CustomerReviews.Core.Model;

    public interface ICustomerReviewService
    {
        void DeleteCustomerReviews(string[] ids);

        CustomerReview[] GetByIds(string[] ids);

        void SaveCustomerReviews(CustomerReview[] newCustomerReviews);
    }
}