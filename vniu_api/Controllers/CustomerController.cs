using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vniu_api.Repositories;
using vniu_api.ViewModels;

namespace vniu_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepo _customerRepo;
        public CustomerController(ICustomerRepo customerRepo)
        {
            _customerRepo = customerRepo;
        }

        [HttpGet("get-all")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CustomerVM>))]
        public async Task<ActionResult> GetCustomers()
        {
            return Ok(await _customerRepo.GetAll());
        }
    }
}
