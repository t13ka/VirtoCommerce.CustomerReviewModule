namespace CustomerReviews.Test
{
    using System;

    using CustomerReviews.Core.Services;
    using CustomerReviews.Data.Repositories;
    using CustomerReviews.Data.Services;

    using Moq;

    using VirtoCommerce.Platform.Testing.Bases;

    using Xunit;

    public class CustomerReviewsSearchServiceTests : FunctionalTestBase
    {
        private readonly ICustomerReviewSearchService _customerReviewSearchService;

        private readonly Mock<ICustomerReviewService> _customerReviewService;

        private readonly Mock<ICustomerReviewRepository> _repositoryFactory;

        public CustomerReviewsSearchServiceTests()
        {
            _repositoryFactory = new Mock<ICustomerReviewRepository>();
            _customerReviewService = new Mock<ICustomerReviewService>();
            _customerReviewSearchService = new CustomerReviewSearchService(
                () => _repositoryFactory.Object,
                _customerReviewService.Object);
        }

        [Fact]
        public void Search_ShouldThrow_ArgumentNullException()
        {
            // arrange

            // act
            var action = new Action(() => _customerReviewSearchService.Search(null));

            // assert
            Assert.Throws<ArgumentNullException>(action);
        }
    }
}