using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using vniu_api.Models.EF.Utils;
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
        private readonly ISendMailService _sendMailService;

        public UserController(IUserRepo userRepo, IPhotoService photoService, ISendMailService sendMailService)
        {
            _userRepo = userRepo;
            _photoService = photoService;
            _sendMailService = sendMailService;
        }

        [HttpGet("get-all")]
        [ProducesResponseType(200, Type = typeof(ICollection<UserVM>))]
        public async Task<ActionResult> GetUsers()
        {
            try
            {
                var usersVM = await _userRepo.GetUsersAsync();

                return Ok(new SuccessResponse<ICollection<UserVM>>()
                {
                    Message = "Get list user successfully",
                    Data = usersVM
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

        [HttpPost("photos")]
        public async Task<IActionResult> CreatePhotos(ICollection<IFormFile> files)
        {
            try
            {
                var newPhotosVM = await _photoService.CreatePhotosAsync(files);

                return Ok(new SuccessResponse<ICollection<PhotoVM>>()
                {
                    Message = "Create list new photo successfully",
                    Data = newPhotosVM
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

        [HttpDelete("photos")]
        public async Task<IActionResult> DeletePhoto([FromQuery] ICollection<int> photoIds)
        {
            try
            {
                var photosDeleteVM = await _photoService.DeletePhotosAsync(photoIds);

                return Ok(new SuccessResponse<ICollection<PhotoVM>>()
                {
                    Message = "Delete list photo successfully",
                    Data = photosDeleteVM
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

        [HttpGet("email/test-send")]
        public async Task<IActionResult> SendMail()
        {
            MailData content = new MailData
            {
                MailTo = "trantuanvu00000@gmail.com",
                MailSubject = "Check Email",
                MailBody = "<p><strong>Hello vniu.net</strong></p>"
            };

            var result = await _sendMailService.SendHtmlMail(content);

            return Ok(new SuccessResponse<bool>()
            {
                Message = "Send email successfully",
                Data = result
            });
        }   
    }
}
