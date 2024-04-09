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
    public class ProductImageController : ControllerBase
    {
        private readonly IProductImageRepo _ProductImageRepo;

        public ProductImageController(IProductImageRepo ProductImageRepo)
        {
            _ProductImageRepo = ProductImageRepo;
        }

        [HttpGet("get-all")]
        [Authorize]
        public async Task<IActionResult> GetProductImages()
        {
            try
            {
                var result = await _ProductImageRepo.GetProductImagesAsync();

                return Ok(new SuccessResponse<ICollection<ProductImageVM>>()
                {
                    Message = "Get list ProductImages successfully",
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

        [HttpGet("{ProductImageId}")]
        public async Task<IActionResult> GetProductImageById(int ProductImageId)
        {
            try
            {
                var result = await _ProductImageRepo.GetProductImageByIdAsync(ProductImageId);

                return Ok(new SuccessResponse<ProductImageVM>()
                {
                    Message = "Get ProductImage successfully",
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
        public async Task<IActionResult> CreateProductImage(ProductImageVM ProductImageVM)
        {
            try
            {
                var newProductImage = await _ProductImageRepo.CreateProductImageAsync(ProductImageVM);

                return Ok(new SuccessResponse<ProductImageVM>()
                {
                    Message = "Create ProductImage successfully",
                    Data = newProductImage
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
