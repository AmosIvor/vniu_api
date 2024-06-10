using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nest;
using System.Net;
using vniu_api.Models.Responses;
using vniu_api.Repositories;
using vniu_api.Repositories.Products;
using vniu_api.Repositories.Profiles;
using vniu_api.ViewModels.ProductsViewModels;

namespace vniu_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController :ControllerBase
    {
        private readonly IProductRepo _ProductRepo;
        private readonly IElasticClient _elasticClient;
        private readonly DataContext _context;

        public ProductController(IProductRepo ProductRepo, DataContext context, IElasticClient elasticClient)
        {
            _ProductRepo = ProductRepo;
            _context = context;
            _elasticClient = elasticClient;
        }


        [HttpGet("get-all")]
        //[Authorize]
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
        [HttpPost("get-products-by-ids")]
        public async Task<IActionResult> GetProductsByIds([FromBody] List<int> productItemIds)
        {
            try
            {
                var result = await _ProductRepo.GetProductsByIds(productItemIds);

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
        [HttpGet]
        public async Task<IActionResult> GetData(int page = 1, int pageSize = 4)
        {
            try
            {
                var result = await _ProductRepo.GetProductsAsync();
                var pagedData = result.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                var totalCount = result.Count;

                return Ok(new
                {
                    Data = pagedData,
                    TotalCount = totalCount,
                    Page = page,
                    PageSize = pageSize
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
        [HttpGet("elastic_search/retrieve_data")]
        public async Task<IActionResult> IndexData()
        {
            var dataConvert = await _ProductRepo.GetProductsAsync();

            var bulkAllObservable = _elasticClient.BulkAll(dataConvert, b => b.Index("product")
                                                                    .BackOffTime("30s")
                                                                    .RefreshOnCompleted());

            bulkAllObservable.Wait(TimeSpan.FromMinutes(15), response => { });
            return Ok("success");
        }
    }
}
