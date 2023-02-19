using beAware_models.Models;
using beAware_services.Helpers;
using beAware_services.Services.UserService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace beAware_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        // GET: api/<UserController>
        [HttpGet]
        public async Task<ResponseModel> Get()
        {
            return await userService.GetAll();
        }

        [HttpGet("GetBlockedUsers")]
        public async Task<ResponseModel> GetBlockedUsers()
        {
            return await userService.GetBlockedUsers();
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<ResponseModel> GetById(int id)
        {
            return await userService.GetById(id);
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<ResponseModel> Post([FromBody] User value)
        {
            return await userService.Post(value);
        }

        // PUT api/<UserController>
        [HttpPut]
        public async Task<ResponseModel> Put([FromBody] User value)
        {
           return await userService.Put(value);
        }

        [HttpPut("BlockUser")]
        public async Task<ResponseModel> BlockUser(int id)
        {
            return await userService.BlockUser(id);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<ResponseModel> Delete(int id)
        {
            return await userService.Delete(id);
        }
    }
}
