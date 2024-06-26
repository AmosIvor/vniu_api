﻿using Microsoft.AspNetCore.Authorization;
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
    }
}
