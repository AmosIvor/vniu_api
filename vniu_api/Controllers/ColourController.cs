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
        public class ColourController : ControllerBase
        {
            private readonly IColourRepo _ColourRepo;

            public ColourController(IColourRepo ColourRepo)
            {
                _ColourRepo = ColourRepo;
            }

            [HttpGet("get-all")]
            [Authorize]
            public async Task<IActionResult> GetColours()
            {
                try
                {
                    var result = await _ColourRepo.GetColoursAsync();

                    return Ok(new SuccessResponse<ICollection<ColourVM>>()
                    {
                        Message = "Get list Colours successfully",
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

            [HttpGet("{ColourId}")]
            public async Task<IActionResult> GetColourById(int ColourId)
            {
                try
                {
                    var result = await _ColourRepo.GetColourByIdAsync(ColourId);

                    return Ok(new SuccessResponse<ColourVM>()
                    {
                        Message = "Get Colour successfully",
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
            public async Task<IActionResult> CreateColour(ColourVM ColourVM)
            {
                try
                {
                    var newColour = await _ColourRepo.CreateColourAsync(ColourVM);

                    return Ok(new SuccessResponse<ColourVM>()
                    {
                        Message = "Create Colour successfully",
                        Data = newColour
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
