namespace CustomerReviews.Data.Services
{
    using System;
    using System.Linq;

    using CustomerReviews.Core.Model;
    using CustomerReviews.Core.Services;
    using CustomerReviews.Data.Model;
    using CustomerReviews.Data.Repositories;

    using VirtoCommerce.Domain.Commerce.Model.Search;
    using VirtoCommerce.Platform.Core.Common;
    using VirtoCommerce.Platform.Data.Infrastructure;

    public class CustomerReviewSearchService : ServiceBase, ICustomerReviewSearchService
    {
        private readonly ICustomerReviewService _customerReviewService;

        private readonly Func<ICustomerReviewRepository> _repositoryFactory;

        public CustomerReviewSearchService(
            Func<ICustomerReviewRepository> repositoryFactory,
            ICustomerReviewService customerReviewService)
        {
            _repositoryFactory = repositoryFactory;
            _customerReviewService = customerReviewService;
        }

        public GenericSearchResult<CustomerReview> Search(CustomerReviewSearchCriteria criteria)
        {
            ValidateCriteria(criteria);

            using (var repository = _repositoryFactory())
            {
                var reviewsQuery = BuildCustomerReviewsQuery(repository, criteria);
                var reviewsTotalCount = reviewsQuery.Count();
                var reviewsIdsQuery = reviewsQuery
                    .Skip(criteria.Skip)
                    .Take(criteria.Take)
                    .Select(entity => entity.Id);

                var idsList = reviewsIdsQuery.ToList();
                var idsArray = idsList.ToArray();

                var reviews = _customerReviewService.GetByIds(idsArray);
                var sortedReviews = reviews.OrderBy(review => idsList.IndexOf(review.Id));
                var resultedReviews = sortedReviews.ToList();

                return new GenericSearchResult<CustomerReview>
                    {
                        TotalCount = reviewsTotalCount, 
                        Results = resultedReviews
                    };
            }
        }

        private static void ValidateCriteria(CustomerReviewSearchCriteria criteria)
        {
            if (criteria == null)
            {
                throw new ArgumentNullException($"{nameof(criteria)} must be set");
            }
        }

        private static IQueryable<CustomerReviewEntity> BuildCustomerReviewsQuery(
            ICustomerReviewRepository repository,
            CustomerReviewSearchCriteria criteria)
        {
            var query = repository.CustomerReviews;

            if (!criteria.ProductIds.IsNullOrEmpty())
            {
                query = query.Where(entity => criteria.ProductIds.Contains(entity.ProductId));
            }

            if (criteria.IsActive.HasValue)
            {
                query = query.Where(entity => entity.IsActive == criteria.IsActive);
            }

            if (!criteria.SearchPhrase.IsNullOrEmpty())
            {
                query = query.Where(entity => entity.Content.Contains(criteria.SearchPhrase));
            }

            return query.OrderBySortInfos(CreateSortInfo(criteria));
        }

        private static SortInfo[] CreateSortInfo(SearchCriteriaBase criteria)
        {
            var sortInfos = criteria.SortInfos;
            if (sortInfos.IsNullOrEmpty())
            {
                sortInfos = new[]
                    {
                        new SortInfo
                            {
                                SortColumn = "CreatedDate", 
                                SortDirection = SortDirection.Descending
                            }
                    };
            }

            return sortInfos;
        }
    }
}