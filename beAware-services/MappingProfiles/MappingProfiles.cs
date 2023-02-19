using AutoMapper;
using beAware_models.DTOs.Account;
using beAware_models.DTOs.Category;
using beAware_models.DTOs.News;
using beAware_models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beAware_services.MappingProfiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            #region Account
            CreateMap<User, SignUpDTO>().ReverseMap();
            CreateMap<User, LoginDTO>().ReverseMap();
            #endregion

            #region Category
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<RequestedCategory, RequestedCategoryDTO>().ReverseMap();
            #endregion

            #region News
            CreateMap<News, NewsDTO>().ReverseMap();
            CreateMap<ReportedNews, ReportNewsDTO>().ReverseMap();
            #endregion
        }
    }
}
