using beAware_models.Models;
using beAware_services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beAware_services.Services.UserService
{
    public interface IUserService
    {
        Task<ResponseModel> BlockUser(int id);
        Task<ResponseModel> Delete(int id);
        Task<ResponseModel> GetAll();
        Task<ResponseModel> GetBlockedUsers();
        Task<ResponseModel> GetById(int id);
        Task<ResponseModel> Post(User obj);
        Task<ResponseModel> Put(User obj);
    }
}
