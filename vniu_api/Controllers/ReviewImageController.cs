using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using vniu_api.Models.Responses;
using vniu_api.Repositories.Reviews;
using vniu_api.ViewModels.ReviewsViewModels;
using vniu_api.ViewModels.ShippingViewModels;

namespace vniu_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewImageController : ControllerBase
    {
        private readonly IReviewImageRepo _reviewImageRepo;

        public ReviewImageController(IReviewImageRepo reviewImageRepo)
        {
            _reviewImageRepo = reviewImageRepo;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetReviewImages()
        {
            try
            {
                var result = await _reviewImageRepo.GetReviewImagesAsync();

                return Ok(new SuccessResponse<ICollection<ReviewImageVM>>()
                {
                    Message = "Get list review image successfully",
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

        [HttpGet("{reviewImageId}")]
        public async Task<IActionResult> GetReviewImageById(int reviewImageId)
        {
            try
            {
                var result = await _reviewImageRepo.GetReviewImageByIdAsync(reviewImageId);

                return Ok(new SuccessResponse<ReviewImageVM>()
                {
                    Message = "Get review image successfully",
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

        [HttpGet("{reviewId}/all")]
        public async Task<IActionResult> GetReviewImageByReviewId(int reviewId)
        {
            try
            {
                var result = await _reviewImageRepo.GetReviewImagesByReviewIdAsync(reviewId);

                return Ok(new SuccessResponse<ICollection<ReviewImageVM>>()
                {
                    Message = "Get review image by review id successfully",
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
        public async Task<IActionResult> CreateReviewImage(ReviewImageVM reviewImageVM)
        {
            try
            {
                var newReviewImage = await _reviewImageRepo.CreateReviewImageAsync(reviewImageVM);

                return Ok(new SuccessResponse<ReviewImageVM>()
                {
                    Message = "Create review image successfully",
                    Data = newReviewImage
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

        [HttpPut("{reviewImageId}")]
        public async Task<IActionResult> UpdateReviewImage(int reviewImageId, ReviewImageVM reviewImageVM)
        {
            try
            {
                var reviewImageUpdate = await _reviewImageRepo.UpdateReviewImageAsync(reviewImageId, reviewImageVM);

                return Ok(new SuccessResponse<ReviewImageVM>()
                {
                    Message = "Update review image successfully",
                    Data = reviewImageUpdate
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

        [HttpDelete("{reviewImageId}")]
        public async Task<IActionResult> DeleteReviewImageById(int reviewImageId)
        {
            try
            {
                var reviewImageDelete = await _reviewImageRepo.DeleteReviewImageByIdAsync(reviewImageId);

                return Ok(new SuccessResponse<ReviewImageVM>()
                {
                    Message = "Delete review image successfully",
                    Data = reviewImageDelete
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

        [HttpDelete("{reviewId}/all")]
        public async Task<IActionResult> DeleteReviewImageByReviewId(int reviewId)
        {
            try
            {
                var reviewImagesDelete = await _reviewImageRepo.DeleteReviewImageByReviewIdAsync(reviewId);

                return Ok(new SuccessResponse<ICollection<ReviewImageVM>>()
                {
                    Message = "Delete review image successfully",
                    Data = reviewImagesDelete
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
