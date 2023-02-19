using AutoMapper;
using beAware_models.DTOs.Category;
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

namespace beAware_services.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IErrorLogginService errorLoggin;

        public CategoryService(ApplicationDbContext context, IMapper mapper, IErrorLogginService errorLoggin)
        {
            this.context = context;
            this.mapper = mapper;
            this.errorLoggin = errorLoggin;
        }

        public async Task<ResponseModel> GetAll()
        {
            ResponseModel response = new ResponseModel();

            try
            {
                response.Data = await context.Categories.Where(x => x.IsDeleted == false && x.IsActive == true).ToListAsync();
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
                var isExist = await context.Categories.Where(x => x.Id == id && x.IsDeleted == false && x.IsActive == true).FirstOrDefaultAsync();

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

        public async Task<ResponseModel> GetRequestedCategories()
        {
            ResponseModel response = new ResponseModel();

            try
            {
                var isExist = await context.RequestedCategories.Where(x => x.IsDeleted == false && x.IsActive == true).ToListAsync();

                if (isExist != null)
                {
                    response.Data = await context.RequestedCategories.Where(x => x.IsDeleted == false && x.IsActive == true).Select(q => new
                    {
                        q.Id,
                        q.Name,
                        q.UserId,
                        q.Users.Email,
                        q.IsCreated,
                        CreatedDateTime = q.ModifiedDateTime == null ? q.CreatedDateTime : q.ModifiedDateTime,
                    }).ToListAsync();
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
        
        public async Task<ResponseModel> Post(CategoryDTO obj)
        {
            ResponseModel response = new ResponseModel();

            try
            {
                Category category = mapper.Map<Category>(obj);

                var isExist = await context.Categories.Where(x => x.Name == category.Name && x.IsDeleted == false && x.IsActive == true).FirstOrDefaultAsync();

                if (isExist == null)
                {
                    await context.Categories.AddAsync(category);
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
        
        public async Task<ResponseModel> RequestCategory(RequestedCategoryDTO obj)
        {
            ResponseModel response = new ResponseModel();

            try
            {
                RequestedCategory requestedCategories = mapper.Map<RequestedCategory>(obj);

                var isExist = await context.RequestedCategories.Where(x => x.Name == requestedCategories.Name && x.IsDeleted == false && x.IsActive == true).FirstOrDefaultAsync();

                if (isExist == null)
                {
                    await context.RequestedCategories.AddAsync(requestedCategories);
                    await context.SaveChangesAsync();

                    response.Status = true;
                    response.Message = (ResponseEnums.Success).AsString(EnumFormat.Description);
                }
                else
                {
                    response.Status = false;
                    response.Message = (ResponseEnums.Failure).AsString(EnumFormat.Description);
                    response.ValidationMessage = (ValidationMessage.CategoryAlreadyExist).AsString(EnumFormat.Description);
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
        
        public async Task<ResponseModel> ApproveRequestedCategory(int id)
        {
            ResponseModel response = new ResponseModel();

            try
            {
                var isExist = await context.RequestedCategories.Where(x => x.Id == id && x.IsDeleted == false && x.IsActive == true).FirstOrDefaultAsync();

                if (isExist != null)
                {
                    isExist.IsCreated = true;
                    context.Entry(isExist).State = EntityState.Modified;

                    Category category = new Category()
                    {
                        Name = isExist.Name,
                        IsActive = true,
                        IsDeleted = false,
                        CreatedDateTime = DateTime.Now
                    };

                    await context.Categories.AddAsync(category);
                    await context.SaveChangesAsync();

                    response.Status = true;
                    response.Message = (ResponseEnums.Success).AsString(EnumFormat.Description);
                }
                else
                {
                    response.Status = false;
                    response.Message = (ResponseEnums.Failure).AsString(EnumFormat.Description);
                    response.ValidationMessage = (ValidationMessage.CategoryAlreadyExist).AsString(EnumFormat.Description);
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

        public async Task<ResponseModel> Put(CategoryDTO obj)
        {
            ResponseModel response = new ResponseModel();

            try
            {
                Category category = mapper.Map<Category>(obj);

                var isExist = await context.Categories.Where(x => x.Id == category.Id && x.IsDeleted == false && x.IsActive == true).FirstOrDefaultAsync();

                if (isExist != null)
                {
                    var categoryName = await context.Categories.Where(x => x.Name == category.Name && x.IsDeleted == false && x.IsActive == true).FirstOrDefaultAsync();
                    if (categoryName == null)
                    {
                        isExist.Name = category.Name;

                        context.Entry(isExist).State = EntityState.Modified;
                        await context.SaveChangesAsync();

                        response.Status = true;
                        response.Message = (ResponseEnums.Success).AsString(EnumFormat.Description);
                    }else if(categoryName.Id == isExist.Id)
                    {
                        isExist.Name = category.Name;

                        context.Entry(isExist).State = EntityState.Modified;
                        await context.SaveChangesAsync();

                        response.Status = true;
                        response.Message = (ResponseEnums.Success).AsString(EnumFormat.Description);
                    }
                    else
                    {
                        response.Status = false;
                        response.Message = (ResponseEnums.Failure).AsString(EnumFormat.Description);
                        response.ValidationMessage = (ValidationMessage.CategoryAlreadyExist).AsString(EnumFormat.Description);
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

        public async Task<ResponseModel> Delete(int id)
        {
            ResponseModel response = new ResponseModel();

            try
            {
                var isExist = await context.Categories.Where(x => x.Id == id && x.IsDeleted == false && x.IsActive == true).FirstOrDefaultAsync();

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
