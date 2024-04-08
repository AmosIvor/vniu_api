using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using vniu_api.Models.Responses;
using vniu_api.Repositories.Profiles;
using vniu_api.Repositories.Utils;
using vniu_api.ViewModels.ProfilesViewModels;
using vniu_api.ViewModels.UtilsViewModels;

namespace vniu_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        private readonly IPhotoService _photoService;
        public UserController(IUserRepo userRepo, IPhotoService photoService)
        {
            _userRepo = userRepo;
            _photoService = photoService;
        }

        [HttpGet("get-all")]
        [ProducesResponseType(200, Type = typeof(ICollection<UserVM>))]
        public async Task<ActionResult> GetUsers()
        {
            return Ok(await _userRepo.GetUsersAsync());
        }

        [HttpPost("photo")]
        public async Task<IActionResult> CreatePhoto(IFormFile file)
        {
            try
            {
                var newPhotoVM = await _photoService.CreatePhotoAsync(file);

                return Ok(new SuccessResponse<PhotoVM>()
                {
                    Message = "Create new photo successfully",
                    Data = newPhotoVM
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

        [HttpDelete("photo/{photoId}")]
        public async Task<IActionResult> DeletePhoto(int photoId)
        {
            try
            {
                var photoDeleteVM = await _photoService.DeletePhotoAsync(photoId);

                return Ok(new SuccessResponse<PhotoVM>()
                {
                    Message = "Delete photo successfully",
                    Data = photoDeleteVM
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
