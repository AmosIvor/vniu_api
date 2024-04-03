using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using vniu_api.Models.EF.Auths;
using vniu_api.Models.Responses;
using vniu_api.Repositories.Auths;
using vniu_api.ViewModels.ProfilesViewModels;

namespace vniu_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthRepo _authRepo;
        public AuthController(IAuthRepo authRepo)
        {
            _authRepo = authRepo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> SignUp(UserRegister userRegister)
        {
            try
            {
                var result = await _authRepo.SignUpAsync(userRegister);

                return Ok(new SuccessResponse<UserVM>()
                {
                    Message = "Register Successfully",
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

        [HttpPost("login")]
        public async Task<IActionResult> SignIn(UserLogin userLogin)
        {
            try
            {
                var result = await _authRepo.SignInAsync(userLogin);

                return Ok(new SuccessResponse<AuthResponse>()
                {
                    Message = "Login Successfully",
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

    }
}
