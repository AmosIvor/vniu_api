using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using vniu_api.Constants;
using vniu_api.Models.Responses;
using vniu_api.Repositories.Promotions;
using vniu_api.ViewModels.PromotionsViewModels;

namespace vniu_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private readonly IPromotionRepo _promotionRepo;

        public PromotionController(IPromotionRepo promotionRepo)
        {
            _promotionRepo = promotionRepo;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetPromotions()
        {
            try
            {
                var result = await _promotionRepo.GetPromotionsAsync();

                return Ok(new SuccessResponse<ICollection<PromotionVM>>()
                {
                    Message = "Get list promotions successfully",
                    Data = result
                });
            }
            catch (Exception e)
            {

                return BadRequest(new ErrorResponse()
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Title = e.Message
                }) ;
            }
        }

        [HttpGet("{promotionId}")]
        [Authorize]
        public async Task<IActionResult> GetPromotionById(int promotionId)
        {
            try
            {
                var result = await _promotionRepo.GetPromotionByIdAsync(promotionId);

                return Ok(new SuccessResponse<PromotionVM>()
                {
                    Message = "Get promotion successfully",
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
        public async Task<IActionResult> CreatePromotion(PromotionVM promotionVM)
        {
            try
            {
                var newPromotion = await _promotionRepo.CreatePromotionAsync(promotionVM);

                return Ok(new SuccessResponse<PromotionVM>()
                {
                    Message = "Create promotion successfully",
                    Data = newPromotion
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

        [HttpPut("{promotionId}")]
        public async Task<IActionResult> UpdatePromotion(int promotionId, PromotionVM promotionVM)
        {
            try
            {
                var promotionUpdate = await _promotionRepo.UpdatePromotionAsync(promotionId, promotionVM);

                return Ok(new SuccessResponse<PromotionVM>()
                {
                    Message = "Update promotion successfully",
                    Data = promotionUpdate
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

        [HttpDelete("{promotionId}")]
        public async Task<IActionResult> DeletePromotion(int promotionId)
        {
            try
            {
                var promotionDelete = await _promotionRepo.DeletePromotionAsync(promotionId);

                return Ok(new SuccessResponse<PromotionVM>()
                {
                    Message = "Delete promotion successfully",
                    Data = promotionDelete
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
