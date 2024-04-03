using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using vniu_api.Models.Responses;
using vniu_api.Repositories.Products;
using vniu_api.ViewModels.ProductsViewModels;

namespace vniu_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SizeController : ControllerBase
    {
        private readonly ISizeRepo _SizeRepo;

        public SizeController(ISizeRepo SizeRepo)
        {
            _SizeRepo = SizeRepo;
        }

        [HttpGet("get-all")]
        [Authorize]
        public async Task<IActionResult> GetSizes()
        {
            try
            {
                var result = await _SizeRepo.GetSizesAsync();

                return Ok(new SuccessResponse<ICollection<SizeVM>>()
                {
                    Message = "Get list Sizes successfully",
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

        [HttpGet("{SizeId}")]
        public async Task<IActionResult> GetSizeById(int SizeId)
        {
            try
            {
                var result = await _SizeRepo.GetSizeByIdAsync(SizeId);

                return Ok(new SuccessResponse<SizeVM>()
                {
                    Message = "Get Size successfully",
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
        public async Task<IActionResult> CreateSize(SizeVM SizeVM)
        {
            try
            {
                var newSize = await _SizeRepo.CreateSizeAsync(SizeVM);

                return Ok(new SuccessResponse<SizeVM>()
                {
                    Message = "Create Size successfully",
                    Data = newSize
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
