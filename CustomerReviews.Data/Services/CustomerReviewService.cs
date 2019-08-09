namespace CustomerReviews.Data.Services
{
    using System;
    using System.Linq;

    using CustomerReviews.Core.Model;
    using CustomerReviews.Core.Services;
    using CustomerReviews.Data.Model;
    using CustomerReviews.Data.Repositories;

    using VirtoCommerce.Platform.Core.Common;
    using VirtoCommerce.Platform.Data.Infrastructure;

    public class CustomerReviewService : ServiceBase, ICustomerReviewService
    {
        private readonly Func<ICustomerReviewRepository> _repositoryFactory;

        public CustomerReviewService(Func<ICustomerReviewRepository> repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public void DeleteCustomerReviews(string[] ids)
        {
            using (var repository = _repositoryFactory())
            {
                repository.DeleteCustomerReviews(ids);
                CommitChanges(repository);
            }
        }

        public CustomerReview[] GetByIds(string[] ids)
        {
            using (var repository = _repositoryFactory())
            {
                var customerReviewEntities = repository.GetByIds(ids);
                var customerReviews = customerReviewEntities.Select(ConvertEntityToModel);
                var customerReviewsArray = customerReviews.ToArray();
                return customerReviewsArray;
            }
        }

        public void SaveCustomerReviews(CustomerReview[] newCustomerReviews)
        {
            if (newCustomerReviews == null)
            {
                throw new ArgumentNullException(nameof(newCustomerReviews));
            }

            using (var repository = _repositoryFactory())
            using (var changeTracker = GetChangeTracker(repository))
            {
                var keyResolvingMap = new PrimaryKeyResolvingMap();

                var nonTransientCustomerReviews = newCustomerReviews.Where(customerReview => !customerReview.IsTransient());

                var ids = nonTransientCustomerReviews.Select(x => x.Id).ToArray();

                var alreadyExistEntities = repository.GetByIds(ids);
                
                foreach (var newCustomerReview in newCustomerReviews)
                {
                    var sourceEntity = CreateEntity(newCustomerReview, keyResolvingMap);
                    var targetEntity = alreadyExistEntities.FirstOrDefault(x => x.Id == sourceEntity.Id);

                    if (targetEntity != null)
                    {
                        changeTracker.Attach(targetEntity);
                        sourceEntity.Patch(targetEntity);
                    }
                    else
                    {
                        repository.Add(sourceEntity);
                    }
                }

                CommitChanges(repository);
                
                keyResolvingMap.ResolvePrimaryKeys();
            }
        }

        public float GetProductRating(string productId)
        {
            float productAverageRating;
            using (var repository = _repositoryFactory())
            {
                var productCustomerReviews = repository.CustomerReviews.Where(customerReview => customerReview.ProductId == productId);
                productAverageRating = productCustomerReviews.Average(customerReview => customerReview.Rating);
            }

            return productAverageRating;
        }

        private static CustomerReview ConvertEntityToModel(CustomerReviewEntity entity)
        {
            return entity.ToModel(AbstractTypeFactory<CustomerReview>.TryCreateInstance());
        }

        private static CustomerReviewEntity CreateEntity(CustomerReview source, PrimaryKeyResolvingMap keyResolvingMap)
        {
            return AbstractTypeFactory<CustomerReviewEntity>.TryCreateInstance().FromModel(source, keyResolvingMap);
        }
    }
}