using AutoMapper;
using beAware_models.DTOs.News;
using beAware_models.Models;
using beAware_services.Data;
using beAware_services.Enums;
using beAware_services.Helpers;
using beAware_services.Services.ErrorLoggin;
using beAware_services.Services.MediaService;
using EnumsNET;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beAware_services.Services.NewsService
{
    public class NewsService : INewsService
    {
        private readonly ApplicationDbContext context;
        private readonly IMediaUploadService mediaUploadService;
        private readonly IMapper mapper;
        private readonly IErrorLogginService errorLoggin;

        public NewsService(ApplicationDbContext context, IMediaUploadService mediaUploadService, IMapper mapper, IErrorLogginService errorLoggin)
        {
            this.context = context;
            this.mediaUploadService = mediaUploadService;
            this.mapper = mapper;
            this.errorLoggin = errorLoggin;
        }

        public async Task<ResponseModel> GetAll()
        {
            ResponseModel response = new ResponseModel();

            try
            {
                response.Data = await context.News.Where(x => x.IsDeleted == false && x.IsActive == true).Select(q => new
                {
                    q.Id,
                    q.Title,
                    Image = GlobalVariables.BaseURL + q.Image,
                    q.Description,
                    q.CountryName,
                    q.StateName,
                    q.CityName,
                    CreatedDateTime = q.ModifiedDateTime == null ? q.CreatedDateTime : q.ModifiedDateTime,
                    q.CategoryId,
                    CategoryName = q.Categories.Name,
                    q.UserId,
                    FullName = q.Users.FirstName + " " + q.Users.LastName
                }).ToListAsync();
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

        public async Task<ResponseModel> GetNewsByUserCity(int id)
        {
            ResponseModel response = new ResponseModel();

            try
            {
                var isExist = await context.Users.Where(x => x.Id == id && x.IsDeleted == false && x.IsActive == true).FirstOrDefaultAsync();

                if (isExist != null)
                {
                    response.Data = await context.News.Where(x => x.CityName == isExist.City && x.IsDeleted == false && x.IsActive == true).Select(q => new
                    {
                        q.Id,
                        q.Title,
                        Image = GlobalVariables.BaseURL + q.Image,
                        q.Description,
                        q.CountryName,
                        q.StateName,
                        q.CityName,
                        CreatedDateTime = q.ModifiedDateTime == null ? q.CreatedDateTime : q.ModifiedDateTime,
                        q.CategoryId,
                        CategoryName = q.Categories.Name,
                        q.UserId,
                        FullName = q.Users.FirstName + " " + q.Users.LastName
                    }).OrderByDescending(q => q.CreatedDateTime).ToListAsync();

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

        public async Task<ResponseModel> GetTodayNewsByUserCity(int id)
        {
            ResponseModel response = new ResponseModel();

            try
            {
                var isExist = await context.Users.Where(x => x.Id == id && x.IsDeleted == false && x.IsActive == true).FirstOrDefaultAsync();

                if (isExist != null)
                {
                    response.Data = await context.News.Where(x => x.CityName == isExist.City && x.CreatedDateTime.Date == DateTime.Now.Date && x.IsDeleted == false && x.IsActive == true).Select(q => new
                    {
                        q.Id,
                        q.Title,
                        Image = GlobalVariables.BaseURL + q.Image,
                        q.Description,
                        q.CountryName,
                        q.StateName,
                        q.CityName,
                        CreatedDateTime = q.ModifiedDateTime == null ? q.CreatedDateTime : q.ModifiedDateTime,
                        q.CategoryId,
                        CategoryName = q.Categories.Name,
                        q.UserId,
                        FullName = q.Users.FirstName + " " + q.Users.LastName
                    }).Take(6).ToListAsync();

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

        public async Task<ResponseModel> GetNewsByUserId(int id)
        {
            ResponseModel response = new ResponseModel();

            try
            {
                var isExist = await context.Users.Where(x => x.Id == id && x.IsDeleted == false).FirstOrDefaultAsync();

                if (isExist != null)
                {
                    response.Data = await context.News.Where(x => x.UserId == id && x.IsDeleted == false).Select(q => new
                    {
                        q.Id,
                        q.Title,
                        Image = GlobalVariables.BaseURL + q.Image,
                        q.Description,
                        q.CountryName,
                        q.StateName,
                        q.CityName,
                        CreatedDateTime = q.ModifiedDateTime == null ? q.CreatedDateTime : q.ModifiedDateTime,
                        q.CategoryId,
                        CategoryName = q.Categories.Name,
                        q.UserId,
                        FullName = q.Users.FirstName + " " + q.Users.LastName,
                        Active = q.IsActive,
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

        public async Task<ResponseModel> GetNewsByCategoryId(int id)
        {
            ResponseModel response = new ResponseModel();

            try
            {
                var isExist = await context.Categories.Where(x => x.Id == id && x.IsDeleted == false && x.IsActive == true).FirstOrDefaultAsync();

                if (isExist != null)
                {
                    if (isExist.Name == "Latest")
                    {
                        response.Data = await context.News.Where(x => x.IsDeleted == false && x.IsActive == true).Select(q => new
                        {
                            q.Id,
                            q.Title,
                            Image = GlobalVariables.BaseURL + q.Image,
                            q.Description,
                            q.CountryName,
                            q.StateName,
                            q.CityName,
                            CreatedDateTime = q.ModifiedDateTime == null ? q.CreatedDateTime : q.ModifiedDateTime,
                            q.CategoryId,
                            CategoryName = q.Categories.Name,
                            q.UserId,
                            FullName = q.Users.FirstName + " " + q.Users.LastName
                        }).OrderByDescending(q => q.CreatedDateTime).ToListAsync();
                    }
                    else
                    {
                        response.Data = await context.News.Where(x => x.CategoryId == id && x.IsDeleted == false && x.IsActive == true).Select(q => new
                        {
                            q.Id,
                            q.Title,
                            Image = GlobalVariables.BaseURL + q.Image,
                            q.Description,
                            q.CountryName,
                            q.StateName,
                            q.CityName,
                            CreatedDateTime = q.ModifiedDateTime == null ? q.CreatedDateTime : q.ModifiedDateTime,
                            q.CategoryId,
                            CategoryName = q.Categories.Name,
                            q.UserId,
                            FullName = q.Users.FirstName + " " + q.Users.LastName
                        }).ToListAsync();
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

        public async Task<ResponseModel> GetReportedNewsByUserId(int id)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                var isExist = await context.Users.Where(x => x.Id == id && x.IsDeleted == false && x.IsActive == true).FirstOrDefaultAsync();
                if (isExist != null)
                {
                    response.Data = await context.ReportedNews.Where(x => x.UserId == id).Select(q => new
                    {
                        q.Id,
                        q.UserId,
                        q.NewsId,
                        q.News.Title,
                        q.Reason,
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

        public async Task<ResponseModel> GetReportedNews()
        {
            ResponseModel response = new ResponseModel();
            try
            {
                response.Data = await context.ReportedNews.Select(q => new
                {
                    q.Id,
                    ReportedByUserId = q.UserId,
                    ReportedByEmail = q.Users.Email,
                    q.NewsId,
                    q.News.Title,
                    ReportedUserId = q.News.UserId,
                    ReportedEmail = q.News.Users.Email,
                    q.Reason,
                    CreatedDateTime = q.ModifiedDateTime == null ? q.CreatedDateTime : q.ModifiedDateTime,
                    q.IsActive,
                }).ToListAsync();

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
                var isExist = await context.News.Where(x => x.Id == id && x.IsDeleted == false && x.IsActive == true).Select(q => new
                {
                    q.Id,
                    q.Title,
                    Image = GlobalVariables.BaseURL + q.Image,
                    q.Description,
                    q.CountryName,
                    q.StateName,
                    q.CityName,
                    CreatedDateTime = q.ModifiedDateTime == null ? q.CreatedDateTime : q.ModifiedDateTime,
                    q.CategoryId,
                    CategoryName = q.Categories.Name,
                    q.UserId,
                    FullName = q.Users.FirstName + " " + q.Users.LastName
                }).FirstOrDefaultAsync();

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

        public async Task<ResponseModel> Post(NewsDTO obj)
        {
            ResponseModel response = new ResponseModel();

            try
            {
                News news = mapper.Map<News>(obj);

                var isExist = await context.News.Where(x => x.Title == news.Title && x.IsDeleted == false && x.IsActive == true).FirstOrDefaultAsync();

                if (isExist == null)
                {
                    await context.News.AddAsync(news);
                    await context.SaveChangesAsync();

                    if (!string.IsNullOrWhiteSpace(news.Image) && news.Image != null)
                    {
                        await this.mediaUploadService.UploadNewsImage(news.Image, news.Id);
                    }

                    response.Status = true;
                    response.Message = (ResponseEnums.Success).AsString(EnumFormat.Description);
                }
                else
                {
                    response.Status = false;
                    response.Message = (ResponseEnums.Failure).AsString(EnumFormat.Description);
                    response.ValidationMessage = (ValidationMessage.NewsTitleAlreadyExist).AsString(EnumFormat.Description);
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

        public async Task<ResponseModel> ReportNews(ReportNewsDTO obj)
        {
            ResponseModel response = new ResponseModel();

            try
            {
                ReportedNews reportNews = mapper.Map<ReportedNews>(obj);

                var isExist = await context.News.Where(x => x.Id == reportNews.NewsId && x.IsDeleted == false && x.IsActive == true).FirstOrDefaultAsync();
                if (isExist != null)
                {
                    var isReported = await context.ReportedNews.Where(x => x.NewsId == reportNews.NewsId && x.UserId == reportNews.UserId && x.IsDeleted == false && x.IsActive == true).FirstOrDefaultAsync();

                    if (isReported == null)
                    {
                        await context.ReportedNews.AddAsync(reportNews);
                        await context.SaveChangesAsync();

                        response.Status = true;
                        response.Message = (ResponseEnums.Success).AsString(EnumFormat.Description);
                    }
                    else
                    {
                        response.Status = false;
                        response.Message = (ResponseEnums.Failure).AsString(EnumFormat.Description);
                        response.ValidationMessage = (ValidationMessage.NewsAlreadyReported).AsString(EnumFormat.Description);
                    }


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

        public async Task<ResponseModel> Put(NewsDTO obj)
        {
            ResponseModel response = new ResponseModel();

            try
            {
                News news = mapper.Map<News>(obj);

                var isExist = await context.News.Where(x => x.Id == news.Id && x.IsDeleted == false && x.IsActive == true).FirstOrDefaultAsync();

                if (isExist != null)
                {
                    isExist.Title = news.Title;
                    isExist.Description = news.Description;
                    isExist.CountryName = news.CountryName;
                    isExist.StateName = news.StateName;
                    isExist.CityName = news.CityName;
                    isExist.CategoryId = news.CategoryId;

                    context.Entry(isExist).State = EntityState.Modified;
                    await context.SaveChangesAsync();

                    if (!string.IsNullOrWhiteSpace(news.Image) && news.Image != null)
                    {
                        await this.mediaUploadService.UploadNewsImage(news.Image, news.Id);
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
                var isExist = await context.News.Where(x => x.Id == id && x.IsDeleted == false && x.IsActive == true).FirstOrDefaultAsync();

                if (isExist != null)
                {
                    var reportedNews = await context.ReportedNews.Where(x => x.NewsId == id).ToListAsync();

                    if (reportedNews != null)
                    {
                        foreach (var item in reportedNews)
                        {
                            item.IsActive = false;
                            context.Entry(item).State = EntityState.Modified;
                        }
                    }

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
