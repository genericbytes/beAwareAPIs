using beAware_models.DTOs.News;
using beAware_services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beAware_services.Services.NewsService
{
    public interface INewsService
    {
        Task<ResponseModel> Delete(int id);
        Task<ResponseModel> GetAll();
        Task<ResponseModel> GetById(int id);
        Task<ResponseModel> GetNewsByCategoryId(int id);
        Task<ResponseModel> GetReportedNewsByUserId(int id);
        Task<ResponseModel> GetNewsByUserCity(int id);
        Task<ResponseModel> GetNewsByUserId(int id);
        Task<ResponseModel> GetTodayNewsByUserCity(int id);
        Task<ResponseModel> Post(NewsDTO obj);
        Task<ResponseModel> ReportNews(ReportNewsDTO obj);
        Task<ResponseModel> Put(NewsDTO obj);
        Task<ResponseModel> GetReportedNews();
    }
}
