namespace CustomerReviews.Data.Repositories
{
    using System.Data.Entity;
    using System.Linq;

    using CustomerReviews.Data.Model;

    using VirtoCommerce.Platform.Data.Infrastructure;
    using VirtoCommerce.Platform.Data.Infrastructure.Interceptors;

    public class CustomerReviewRepository : EFRepositoryBase, ICustomerReviewRepository
    {
        public CustomerReviewRepository()
        {
        }

        public CustomerReviewRepository(string nameOrConnectionString, params IInterceptor[] interceptors)
            : base(nameOrConnectionString, null, interceptors)
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public IQueryable<CustomerReviewEntity> CustomerReviews => GetAsQueryable<CustomerReviewEntity>();

        public void DeleteCustomerReviews(string[] ids)
        {
            var items = GetByIds(ids);
            foreach (var item in items)
            {
                Remove(item);
            }
        }

        public CustomerReviewEntity[] GetByIds(string[] ids)
        {
            return CustomerReviews.Where(x => ids.Contains(x.Id)).ToArray();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerReviewEntity>().ToTable("CustomerReview").HasKey(x => x.Id).Property(x => x.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}