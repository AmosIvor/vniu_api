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
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryRepo _CategoryRepo;

        public ProductCategoryController(IProductCategoryRepo CategoryRepo)
        {
            _CategoryRepo = CategoryRepo;
        }

        [HttpGet("get-all")]
        [Authorize]
        public async Task<IActionResult> GetCategorys()
        {
            try
            {
                var result = await _CategoryRepo.GetCategorysAsync();
                Console.WriteLine(result);

                return Ok(new SuccessResponse<ICollection<ProductCategoryVM>>()
                {
                    Message = "Get list Categorys successfully",
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

        [HttpGet("{CategoryId}")]
        public async Task<IActionResult> GetCategoryById(int CategoryId)
        {
            try
            {
                var result = await _CategoryRepo.GetCategoryByIdAsync(CategoryId);

                return Ok(new SuccessResponse<ProductCategoryVM>()
                {
                    Message = "Get Category successfully",
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
        public async Task<IActionResult> CreateCategory(ProductCategoryVM CategoryVM)
        {
            try
            {
                var newCategory = await _CategoryRepo.CreateCategoryAsync(CategoryVM);

                return Ok(new SuccessResponse<ProductCategoryVM>()
                {
                    Message = "Create Category successfully",
                    Data = newCategory
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
