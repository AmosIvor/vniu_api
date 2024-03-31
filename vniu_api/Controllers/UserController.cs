using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vniu_api.Repositories;
using vniu_api.ViewModels;

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
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserVM>))]
        public async Task<ActionResult> GetUsers()
        {
            return Ok(await _userRepo.GetAll());
        }
    }
}
