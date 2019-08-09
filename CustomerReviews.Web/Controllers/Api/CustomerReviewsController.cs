namespace CustomerReviews.Web.Controllers.Api
{
    using System.Net;
    using System.Web.Http;
    using System.Web.Http.Description;

    using CustomerReviews.Core.Model;
    using CustomerReviews.Core.Services;
    using CustomerReviews.Web.Security;

    using VirtoCommerce.Domain.Commerce.Model.Search;
    using VirtoCommerce.Platform.Core.Web.Security;

    [RoutePrefix("api/customerReviews")]
    public class CustomerReviewsController : ApiController
    {
        private readonly ICustomerReviewSearchService _customerReviewSearchService;

        private readonly ICustomerReviewService _customerReviewService;

        public CustomerReviewsController()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerReviewsController"/> class.
        /// </summary>
        /// <param name="customerReviewSearchService">
        /// The customer review search service.
        /// </param>
        /// <param name="customerReviewService">
        /// The customer review service.
        /// </param>
        public CustomerReviewsController(
            ICustomerReviewSearchService customerReviewSearchService,
            ICustomerReviewService customerReviewService)
        {
            _customerReviewSearchService = customerReviewSearchService;
            _customerReviewService = customerReviewService;
        }

        /// <summary>
        /// Delete customer reviews by IDs
        /// </summary>
        /// <param name="ids">
        /// The ids.
        /// </param>
        /// <returns>
        /// The <see cref="IHttpActionResult"/>.
        /// </returns>
        [HttpDelete]
        [Route("")]
        [ResponseType(typeof(void))]
        [CheckPermission(Permission = PredefinedPermissions.CustomerReviewDelete)]
        public IHttpActionResult Delete([FromUri] string[] ids)
        {
            _customerReviewService.DeleteCustomerReviews(ids);
            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Get product rating.
        /// </summary>
        /// <param name="productId">
        /// The product id.
        /// </param>
        /// <returns>
        /// The <see cref="IHttpActionResult"/>.
        /// </returns>
        [HttpGet]
        [Route("rating")]
        [ResponseType(typeof(string))]
        [CheckPermission(Permission = PredefinedPermissions.CustomerReviewRead)]
        public IHttpActionResult GetProductRating(string productId)
        {
            var result = _customerReviewService.GetProductRating(productId);
            return Ok(result);
        }

        /// <summary>
        /// Search customer reviews.
        /// </summary>
        /// <param name="criteria">
        /// The criteria.
        /// </param>
        /// <returns>
        /// The <see cref="IHttpActionResult"/>.
        /// </returns>
        [HttpPost]
        [Route("search")]
        [ResponseType(typeof(GenericSearchResult<CustomerReview>))]
        [CheckPermission(Permission = PredefinedPermissions.CustomerReviewRead)]
        public IHttpActionResult SearchCustomerReviews(CustomerReviewSearchCriteria criteria)
        {
            var result = _customerReviewSearchService.SearchCustomerReviews(criteria);
            return Ok(result);
        }

        /// <summary>
        /// Create new or update existing customer review.
        /// </summary>
        /// <param name="customerReviews">
        /// The customer reviews.
        /// </param>
        /// <returns>
        /// The <see cref="IHttpActionResult"/>.
        /// </returns>
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(void))]
        [CheckPermission(Permission = PredefinedPermissions.CustomerReviewUpdate)]
        public IHttpActionResult Update(CustomerReview[] customerReviews)
        {
            _customerReviewService.SaveCustomerReviews(customerReviews);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}