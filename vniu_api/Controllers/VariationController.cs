using AutoMapper;
using vniu_api.Models.EF.Products;
using vniu_api.Repositories.Products;
using vniu_api.Repositories;
using vniu_api.ViewModels.ProductsViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using vniu_api.Models.Responses;
using vniu_api.Services.Products;

namespace vniu_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VariationController : ControllerBase
    {
        private readonly IVariationRepo _VariationRepo;

        public VariationController(IVariationRepo VariationRepo)
        {
            _VariationRepo = VariationRepo;
        }

        [HttpGet("get-all")]
        [Authorize]
        public async Task<IActionResult> GetVariations()
        {
            try
            {
                var result = await _VariationRepo.GetVariationsAsync();

                return Ok(new SuccessResponse<ICollection<VariationVM>>()
                {
                    Message = "Get list Variations successfully",
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

        [HttpGet("{VariationId}")]
        public async Task<IActionResult> GetVariationById(int VariationId)
        {
            try
            {
                var result = await _VariationRepo.GetVariationByIdAsync(VariationId);

                return Ok(new SuccessResponse<VariationVM>()
                {
                    Message = "Get Variation successfully",
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
        public async Task<IActionResult> CreateVariation(VariationVM VariationVM)
        {
            try
            {
                var newVariation = await _VariationRepo.CreateVariationAsync(VariationVM);

                return Ok(new SuccessResponse<VariationVM>()
                {
                    Message = "Create Variation successfully",
                    Data = newVariation
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