using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using vniu_api.Models.Responses;
using vniu_api.Repositories.Reviews;
using vniu_api.ViewModels.ReviewsViewModels;

namespace vniu_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepo _reviewRepo;

        public ReviewController(IReviewRepo reviewRepo)
        {
            _reviewRepo = reviewRepo;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetReviews()
        {
            try
            {
                var result = await _reviewRepo.GetReviewsAsync();

                return Ok(new SuccessResponse<ICollection<ReviewVM>>()
                {
                    Message = "Get list review successfully",
                    Data = result
                });
            }
            catch (Exception e)
            {

                return BadRequest(new ErrorResponse()
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Title = e.Message
                });
            }
        }

        [HttpGet("{reviewId}")]
        public async Task<IActionResult> GetReviewById(int reviewId)
        {
            try
            {
                var result = await _reviewRepo.GetReviewByIdAsync(reviewId);

                return Ok(new SuccessResponse<ReviewVM>()
                {
                    Message = "Get review successfully",
                    Data = result
                });
            }
            catch (Exception e)
            {

                return BadRequest(new ErrorResponse()
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Title = e.Message
                });
            }
        }

        [HttpGet("{userId}/reviews")]
        public async Task<IActionResult> GetReviewsByUserId(string userId)
        {
            try
            {
                var result = await _reviewRepo.GetReviewByUserIdAsync(userId);

                return Ok(new SuccessResponse<ICollection<ReviewVM>>()
                {
                    Message = "Get list review of user successfully",
                    Data = result
                });
            }
            catch (Exception e)
            {

                return BadRequest(new ErrorResponse()
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Title = e.Message
                });
            }
        }

        [HttpGet("{productId}/reviews")]
        public async Task<IActionResult> GetReviewsByUserId(int productId)
        {
            try
            {
                var result = await _reviewRepo.GetReviewByProductIdAsync(productId);

                return Ok(new SuccessResponse<ICollection<ReviewVM>>()
                {
                    Message = "Get list review of product successfully",
                    Data = result
                });
            }
            catch (Exception e)
            {

                return BadRequest(new ErrorResponse()
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Title = e.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview(ReviewVM reviewVM)
        {
            try
            {
                var newReview = await _reviewRepo.CreateReviewAsync(reviewVM);

                return Ok(new SuccessResponse<ReviewVM>()
                {
                    Message = "Create review successfully",
                    Data = newReview
                });
            }
            catch (Exception e)
            {

                return BadRequest(new ErrorResponse()
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Title = e.Message
                });
            }
        }

        [HttpPut("{reviewId}")]
        public async Task<IActionResult> UpdateReview(int reviewId, ReviewVM reviewVM)
        {
            try
            {
                var reviewUpdate = await _reviewRepo.UpdateReviewAsync(reviewId, reviewVM);

                return Ok(new SuccessResponse<ReviewVM>()
                {
                    Message = "Update review successfully",
                    Data = reviewUpdate
                });
            }
            catch (Exception e)
            {

                return BadRequest(new ErrorResponse()
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Title = e.Message
                });
            }
        }

        [HttpDelete("{reviewId}")]
        public async Task<IActionResult> DeleteReview(int reviewId)
        {
            try
            {
                var reviewDelete = await _reviewRepo.DeleteReviewAsync(reviewId);

                return Ok(new SuccessResponse<ReviewVM>()
                {
                    Message = "Delete review successfully",
                    Data = reviewDelete
                });
            }
            catch (Exception e)
            {

                return BadRequest(new ErrorResponse()
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Title = e.Message
                });
            }
        }
    }
}
