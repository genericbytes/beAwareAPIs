using beAware_models.DTOs.Account;
using beAware_services.Helpers;
using beAware_services.Services.AccountService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace beAware_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPost("Login")]
        public async Task<ResponseModel> Login([FromBody] LoginDTO value)
        {
            return await accountService.Login(value);
        }

        [HttpPost("SignUp")]
        public async Task<ResponseModel> SignUp([FromBody] SignUpDTO value)
        {
            return await accountService.SignUp(value);
        }
    }
}
