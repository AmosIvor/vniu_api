using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vniu_api.Repositories.Profiles;
using vniu_api.ViewModels.ProfilesViewModels;

namespace vniu_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        public UserController(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpGet("get-all")]
        [ProducesResponseType(200, Type = typeof(ICollection<UserVM>))]
        public async Task<ActionResult> GetUsers()
        {
            return Ok(await _userRepo.GetUsersAsync());
        }
    }
}
