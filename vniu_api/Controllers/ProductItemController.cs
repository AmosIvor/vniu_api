using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using vniu_api.Models.EF.Products;
using vniu_api.Models.Responses;
using vniu_api.Repositories.Products;
using vniu_api.ViewModels.ProductsViewModels;

namespace vniu_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductItemController : ControllerBase
    {
        private readonly IProductItemRepo _ProductItemRepo;

        public ProductItemController(IProductItemRepo ProductItemRepo)
        {
            _ProductItemRepo = ProductItemRepo;
        }

        [HttpGet("get-all"+ "{ProductId}")]
        [Authorize]
        public async Task<IActionResult> GetProductItems(int ProductId)
        {
            try
            {
                var result = await _ProductItemRepo.GetProductItemAsync(ProductId);

                return Ok(new SuccessResponse<ICollection<ProductItemVM>>()
                {
                    Message = "Get list ProductItems successfully",
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

        [HttpGet("{ProductItemId}")]
        public async Task<IActionResult> GetProductItemById(int ProductItemId)
        {
            try
            {
                var result = await _ProductItemRepo.GetProductItemByIdAsync(ProductItemId);

                return Ok(new SuccessResponse<ProductItemVM>()
                {
                    Message = "Get ProductItem successfully",
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
        public async Task<IActionResult> CreateProductItem(ProductItemVM ProductItemVM)
        {
            try
            {
                var newProductItem = await _ProductItemRepo.CreateProductItemAsync(ProductItemVM);

                return Ok(new SuccessResponse<ProductItemVM>()
                {
                    Message = "Create ProductItem successfully",
                    Data = newProductItem
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
