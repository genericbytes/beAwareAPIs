using beAware_models.DTOs.Category;
using beAware_models.Models;
using beAware_services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beAware_services.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<ResponseModel> Delete(int id);
        Task<ResponseModel> GetAll();
        Task<ResponseModel> GetRequestedCategories();
        Task<ResponseModel> GetById(int id);
        Task<ResponseModel> Post(CategoryDTO obj);
        Task<ResponseModel> RequestCategory(RequestedCategoryDTO obj);
        Task<ResponseModel> Put(CategoryDTO obj);
        Task<ResponseModel> ApproveRequestedCategory(int id);
    }
}
