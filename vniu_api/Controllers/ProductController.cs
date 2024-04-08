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
    public class ProductController :ControllerBase
    {
        private readonly IProductRepo _ProductRepo;

        public ProductController(IProductRepo ProductRepo)
        {
            _ProductRepo = ProductRepo;
        }

        [HttpGet("get-all")]
        [Authorize]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var result = await _ProductRepo.GetProductsAsync();

                return Ok(new SuccessResponse<ICollection<ProductVM>>()
                {
                    Message = "Get list Products successfully",
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

        [HttpGet("{ProductId}")]
        public async Task<IActionResult> GetProductById(int ProductId)
        {
            try
            {
                var result = await _ProductRepo.GetProductByIdAsync(ProductId);

                return Ok(new SuccessResponse<ProductVM>()
                {
                    Message = "Get Product successfully",
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
        public async Task<IActionResult> CreateProduct(ProductVM ProductVM)
        {
            try
            {
                var newProduct = await _ProductRepo.CreateProductAsync(ProductVM);

                return Ok(new SuccessResponse<ProductVM>()
                {
                    Message = "Create Product successfully",
                    Data = newProduct
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
