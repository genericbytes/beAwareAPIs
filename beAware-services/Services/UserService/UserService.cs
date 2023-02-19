using beAware_models.Models;
using beAware_services.Data;
using beAware_services.Enums;
using beAware_services.Helpers;
using beAware_services.Services.EmailService;
using beAware_services.Services.ErrorLoggin;
using EnumsNET;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beAware_services.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext context;
        private readonly IErrorLogginService errorLoggin;
        private readonly IEmailService emailService;

        public UserService(ApplicationDbContext context, IErrorLogginService errorLoggin, IEmailService emailService)
        {
            this.context = context;
            this.errorLoggin = errorLoggin;
            this.emailService = emailService;
        }
        public async Task<ResponseModel> GetAll()
        {
            ResponseModel response = new ResponseModel();

            try
            {
                response.Data = await context.Users.Where(x => x.RoleId == (int)RoleEnums.User).ToListAsync();
                response.Status = true;
                response.Message = (ResponseEnums.Success).AsString(EnumFormat.Description);
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

        public async Task<ResponseModel> GetBlockedUsers()
        {
            ResponseModel response = new ResponseModel();

            try
            {
                response.Data = await context.Users.Where(x => x.RoleId == (int)RoleEnums.User && x.TillBlocked >= DateTime.Now).ToListAsync();
                response.Status = true;
                response.Message = (ResponseEnums.Success).AsString(EnumFormat.Description);
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

        public async Task<ResponseModel> GetById(int id)
        {
            ResponseModel response = new ResponseModel();

            try
            {
                var isExist = await context.Users.Where(x => x.Id == id && x.IsDeleted == false && x.IsActive == true).FirstOrDefaultAsync();

                if (isExist != null)
                {
                    response.Data = isExist;
                    response.Status = true;
                    response.Message = (ResponseEnums.Success).AsString(EnumFormat.Description);
                }
                else
                {
                    response.Status = false;
                    response.Message = (ResponseEnums.Failure).AsString(EnumFormat.Description);
                    response.ValidationMessage = (ValidationMessage.DataNotFound).AsString(EnumFormat.Description);
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

        public async Task<ResponseModel> Post(User obj)
        {
            ResponseModel response = new ResponseModel();

            try
            {
                var isExist = await context.Users.Where(x => x.Email == obj.Email || x.UserName == obj.UserName && x.IsDeleted == false && x.IsActive == true).FirstOrDefaultAsync();

                if (isExist == null)
                {
                    if (obj.RoleId <= 0)
                    {
                        obj.RoleId = (int)RoleEnums.User;
                    }

                    await context.Users.AddAsync(obj);
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

        public async Task<ResponseModel> Put(User obj)
        {
            ResponseModel response = new ResponseModel();

            try
            {
                var isExist = await context.Users.Where(x => x.Id == obj.Id && x.IsDeleted == false && x.IsActive == true).FirstOrDefaultAsync();

                if (isExist != null)
                {
                    var userName = await context.Users.Where(x => x.UserName == obj.UserName && x.IsDeleted == false && x.IsActive == true).FirstOrDefaultAsync();
                    var email = await context.Users.Where(x => x.Email == obj.Email && x.IsDeleted == false && x.IsActive == true).FirstOrDefaultAsync();
                    if (userName == null || (userName != null && userName.Id == isExist.Id) && email == null || (email != null && email.Id == isExist.Id))
                    {
                        isExist.FirstName = obj.FirstName;
                        isExist.LastName = obj.LastName;
                        isExist.UserName = obj.UserName;
                        isExist.Email = obj.Email;
                        isExist.PhoneNumber = obj.PhoneNumber;
                        isExist.DOB = obj.DOB;
                        isExist.Country = obj.Country;
                        isExist.State = obj.State;
                        isExist.City = obj.City;

                        context.Entry(isExist).State = EntityState.Modified;
                        await context.SaveChangesAsync();

                        response.Status = true;
                        response.Message = (ResponseEnums.Success).AsString(EnumFormat.Description);
                    }
                    else if (userName.Id != isExist.Id || email.Id != isExist.Id)
                    {
                        response.Status = false;
                        response.Message = (ResponseEnums.Failure).AsString(EnumFormat.Description);
                        response.ValidationMessage = (ValidationMessage.EmailOrUserNameAlreadyExist).AsString(EnumFormat.Description);
                    }
                }
                else
                {
                    response.Status = false;
                    response.Message = (ResponseEnums.Failure).AsString(EnumFormat.Description);
                    response.ValidationMessage = (ValidationMessage.DataNotFound).AsString(EnumFormat.Description);
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

        public async Task<ResponseModel> BlockUser(int id)
        {
            ResponseModel response = new ResponseModel();

            try
            {
                var isExist = await context.Users.Where(x => x.Id == id && x.IsDeleted == false && x.IsActive == true).FirstOrDefaultAsync();

                if (isExist != null)
                {
                    if (isExist.TillBlocked == null)
                    {
                        isExist.TillBlocked = DateTime.Now.AddDays(30);

                        context.Entry(isExist).State = EntityState.Modified;
                        await context.SaveChangesAsync();

                        await emailService.SendBlockedUserEmail(isExist.Email);
                    }
                    else if (isExist.TillBlocked >= DateTime.Now)
                    {
                        isExist.TillBlocked = DateTime.Now.AddDays(30);

                        context.Entry(isExist).State = EntityState.Modified;
                        await context.SaveChangesAsync();

                        await emailService.SendBlockedUserEmail(isExist.Email);
                    }

                    response.Status = true;
                    response.Message = (ResponseEnums.Success).AsString(EnumFormat.Description);
                }
                else
                {
                    response.Status = false;
                    response.Message = (ResponseEnums.Failure).AsString(EnumFormat.Description);
                    response.ValidationMessage = (ValidationMessage.DataNotFound).AsString(EnumFormat.Description);
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

        public async Task<ResponseModel> Delete(int id)
        {
            ResponseModel response = new ResponseModel();

            try
            {
                var isExist = await context.Users.Where(x => x.Id == id && x.IsDeleted == false && x.IsActive == true).FirstOrDefaultAsync();

                if (isExist != null)
                {
                    isExist.IsDeleted = true;

                    context.Entry(isExist).State = EntityState.Modified;
                    await context.SaveChangesAsync();

                    response.Status = true;
                    response.Message = (ResponseEnums.Success).AsString(EnumFormat.Description);
                }
                else
                {
                    response.Status = false;
                    response.Message = (ResponseEnums.Failure).AsString(EnumFormat.Description);
                    response.ValidationMessage = (ValidationMessage.DataNotFound).AsString(EnumFormat.Description);
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
