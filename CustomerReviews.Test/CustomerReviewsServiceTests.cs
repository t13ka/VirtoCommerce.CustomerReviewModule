namespace CustomerReviews.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using CustomerReviews.Core.Model;
    using CustomerReviews.Core.Services;
    using CustomerReviews.Data.Model;
    using CustomerReviews.Data.Repositories;
    using CustomerReviews.Data.Services;

    using Moq;

    using VirtoCommerce.Platform.Core.Common;
    using VirtoCommerce.Platform.Testing.Bases;

    using Xunit;

    public class CustomerReviewsServiceTests : FunctionalTestBase
    {
        private readonly ICustomerReviewService _customerReviewService;

        private readonly Mock<ICustomerReviewRepository> _repositoryFactory;

        public CustomerReviewsServiceTests()
        {
            _repositoryFactory = new Mock<ICustomerReviewRepository>(MockBehavior.Loose);
            _customerReviewService = new CustomerReviewService(() => _repositoryFactory.Object);
        }

        [Fact]
        public void GetByIds_MockMethodShouldBeInvoked()
        {
            // arrange
            var ids = new[] { Guid.NewGuid().ToString() };
            var repositoryResult = new List<CustomerReviewEntity>().ToArray();

            _repositoryFactory.Setup(repository => repository.GetByIds(ids)).Returns(repositoryResult);

            // act
            _customerReviewService.GetByIds(ids);

            // assert
            _repositoryFactory.VerifyAll();
        }

        [Fact]
        public void GetByIds_ShouldBeSpecificType()
        {
            // arrange
            var ids = new[] { Guid.NewGuid().ToString() };
            var repositoryResult = new List<CustomerReviewEntity>().ToArray();

            _repositoryFactory.Setup(repository => repository.GetByIds(ids)).Returns(repositoryResult);

            // act
            var result = _customerReviewService.GetByIds(ids);

            // assert
            Assert.IsType<CustomerReview[]>(result);
        }

        [Fact]
        public void GetByIds_ShouldReturnEmpty()
        {
            // arrange
            var ids = new[] { Guid.NewGuid().ToString() };
            var repositoryResult = new List<CustomerReviewEntity>().ToArray();

            _repositoryFactory.Setup(repository => repository.GetByIds(ids)).Returns(repositoryResult);

            // act
            var result = _customerReviewService.GetByIds(ids);

            // assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetByIds_ShouldReturnSingleElement()
        {
            // arrange
            var ids = new[] { Guid.NewGuid().ToString() };
            var repositoryResult = new List<CustomerReviewEntity> { new CustomerReviewEntity() }.ToArray();

            _repositoryFactory.Setup(repository => repository.GetByIds(ids)).Returns(repositoryResult);

            // act
            var result = _customerReviewService.GetByIds(ids);

            // assert
            Assert.Single(result);
        }

        [Theory]
        [InlineData(2.5f, 1f, 4f)]
        [InlineData(3f, 1f, 5f)]
        [InlineData(3.495f, 1.55f, 5.44f)]
        public void GetProductRating_ShouldBeEquals(float expected, float review1Rating, float review2Rating)
        {
            // arrange
            var fakeProductId = "fake_product_id";
            var review1 = new CustomerReviewEntity { ProductId = fakeProductId, Rating = review1Rating };
            var review2 = new CustomerReviewEntity { ProductId = fakeProductId, Rating = review2Rating };
            var repositoryResult = new List<CustomerReviewEntity>
                                       {
                                           review1,
                                           review2
                                       };

            _repositoryFactory.Setup(repository => repository.CustomerReviews).Returns(repositoryResult.AsQueryable());

            // act
            var result = _customerReviewService.GetProductRating("fake_product_id");

            // assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void SaveCustomerReviews_ShouldThrow_ArgumentNullException()
        {
            // arrange

            // act
            var action = new Action(() => _customerReviewService.SaveCustomerReviews(null));

            // assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void SaveCustomerReviews_MockMethodsShouldBeInvoked()
        {
            // arrange
            var newCustomerReviews = new List<CustomerReview>
                                         {
                                             new CustomerReview
                                                 {
                                                     Id = "fake id",
                                                     Rating = 4,
                                                     ProductId = "fake_product_id",
                                                     AuthorNickname = "tester",
                                                     Content = "super",
                                                     ModifiedDate = DateTime.UtcNow,
                                                     IsActive = true,
                                                     CreatedBy = "test",
                                                     CreatedDate = DateTime.UtcNow
                                                 }
                                         }.ToArray();
            _repositoryFactory.Setup(repository => repository.Add(It.IsAny<CustomerReviewEntity>()));
            _repositoryFactory.Setup(repository => repository.UnitOfWork).Returns(Mock.Of<IUnitOfWork>());

            // act
            _customerReviewService.SaveCustomerReviews(newCustomerReviews);

            // assert
            _repositoryFactory.VerifyAll();
        }
    }
}