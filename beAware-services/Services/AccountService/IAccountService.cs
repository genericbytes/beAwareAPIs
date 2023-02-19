using beAware_models.DTOs.Account;
using beAware_services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beAware_services.Services.AccountService
{
    public interface IAccountService
    {
        Task<ResponseModel> Login(LoginDTO obj);
        Task<ResponseModel> SignUp(SignUpDTO obj);
    }
}
