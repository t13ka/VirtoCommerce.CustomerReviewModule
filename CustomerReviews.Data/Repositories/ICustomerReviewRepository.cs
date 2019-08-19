namespace CustomerReviews.Data.Repositories
{
    using System.Linq;

    using CustomerReviews.Data.Model;

    using VirtoCommerce.Platform.Core.Common;

    public interface ICustomerReviewRepository : IRepository
    {
        IQueryable<CustomerReviewEntity> CustomerReviews { get; }

        void DeleteCustomerReviews(string[] ids);

        CustomerReviewEntity[] GetByIds(string[] ids);
    }
}