using AutoMapper;
using beAware_models.DTOs.Account;
using beAware_models.Models;
using beAware_services.Data;
using beAware_services.Enums;
using beAware_services.Helpers;
using beAware_services.Services.ErrorLoggin;
using EnumsNET;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beAware_services.Services.AccountService
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IErrorLogginService errorLoggin;

        public AccountService(ApplicationDbContext context, IMapper mapper, IErrorLogginService errorLoggin)
        {
            this.context = context;
            this.mapper = mapper;
            this.errorLoggin = errorLoggin;
        }
        public async Task<ResponseModel> Login(LoginDTO obj)
        {
            ResponseModel response = new ResponseModel();

            try
            {
                var isExist = await context.Users.Where(x => x.Email == obj.Email && x.Password == obj.Password && x.IsDeleted == false).FirstOrDefaultAsync();

                if (isExist != null && isExist.IsActive == true && isExist.TillBlocked == null)
                {
                    response.Data = isExist;
                    response.Status = true;
                    response.Message = (ResponseEnums.Success).AsString(EnumFormat.Description);
                }
                else if (isExist != null && isExist.TillBlocked != null)
                {
                    if (isExist.TillBlocked >= DateTime.Now)
                    {
                        response.Status = false;
                        response.Message = (ResponseEnums.Failure).AsString(EnumFormat.Description);
                        response.ValidationMessage = (ValidationMessage.BlockedByAdmin).AsString(EnumFormat.Description);
                    }
                    else
                    {
                        response.Data = isExist;
                        response.Status = true;
                        response.Message = (ResponseEnums.Success).AsString(EnumFormat.Description);
                    }
                }
                else
                {
                    response.Status = false;
                    response.Message = (ResponseEnums.Failure).AsString(EnumFormat.Description);
                    response.ValidationMessage = (ValidationMessage.InvalidCredentials).AsString(EnumFormat.Description);
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = (ResponseEnums.Failure).AsString(EnumFormat.Description);
                response.ValidationMessage = (ValidationMessage.Expection).AsString(EnumFormat.Description);
                errorLoggin.LogError(ex);
            }
            return response;
        }

        public async Task<ResponseModel> SignUp(SignUpDTO obj)
        {
            ResponseModel response = new ResponseModel();

            try
            {
                User user = mapper.Map<User>(obj);

                var isExist = await context.Users.Where(x => x.Email == user.Email || x.UserName == user.UserName && x.IsDeleted == false && x.IsActive == true).FirstOrDefaultAsync();

                if (isExist == null)
                {
                    if (obj.RoleId <= 0)
                    {
                        user.RoleId = (int)RoleEnums.User;
                    }

                    await context.Users.AddAsync(user);
                    await context.SaveChangesAsync();

                    response.Status = true;
                    response.Message = (ResponseEnums.Success).AsString(EnumFormat.Description);
                }
                else
                {
                    response.Status = false;
                    response.Message = (ResponseEnums.Failure).AsString(EnumFormat.Description);
                    response.ValidationMessage = (ValidationMessage.EmailOrUserNameAlreadyExist).AsString(EnumFormat.Description);
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = (ResponseEnums.Failure).AsString(EnumFormat.Description);
                response.ValidationMessage = (ValidationMessage.Expection).AsString(EnumFormat.Description);
                errorLoggin.LogError(ex);
            }
            return response;
        }
    }
}
