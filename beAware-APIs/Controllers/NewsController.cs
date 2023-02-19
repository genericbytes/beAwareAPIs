using beAware_models.DTOs.News;
using beAware_services.Helpers;
using beAware_services.Services.NewsService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace beAware_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService newsService;

        public NewsController(INewsService newsService)
        {
            this.newsService = newsService;
        }
        // GET: api/<NewsController>
        [HttpGet]
        public async Task<ResponseModel> Get()
        {
            return await newsService.GetAll();
        }

        // GET api/<NewsController>/5
        [HttpGet("{id}")]
        public async Task<ResponseModel> Get(int id)
        {
            return await newsService.GetById(id);
        }

        [HttpGet("GetNewsByUserCity")]
        public async Task<ResponseModel> GetNewsByUserCity(int id)
        {
            return await newsService.GetNewsByUserCity(id);
        }

        [HttpGet("GetNewsByUserId")]
        public async Task<ResponseModel> GetNewsByUserId(int id)
        {
            return await newsService.GetNewsByUserId(id);
        }

        [HttpGet("GetNewsByCategoryId")]
        public async Task<ResponseModel> GetNewsByCategoryId(int id)
        {
            return await newsService.GetNewsByCategoryId(id);
        }

        [HttpGet("GetTodayNewsByUserCity")]
        public async Task<ResponseModel> GetTodayNewsByUserCity(int id)
        {
            return await newsService.GetTodayNewsByUserCity(id);
        }

        [HttpGet("GetReportedNewsByUserId")]
        public async Task<ResponseModel> GetReportedNewsByUserId(int id)
        {
            return await newsService.GetReportedNewsByUserId(id);
        }

        [HttpGet("GetReportedNews")]
        public async Task<ResponseModel> GetReportedNews()
        {
            return await newsService.GetReportedNews();
        }

        // POST api/<NewsController>
        [HttpPost]
        public async Task<ResponseModel> Post([FromBody] NewsDTO value)
        {
            return await newsService.Post(value);
        }

        [HttpPost("ReportNews")]
        public async Task<ResponseModel> ReportNews([FromBody] ReportNewsDTO value)
        {
            return await newsService.ReportNews(value);
        }

        // PUT api/<NewsController>/5
        [HttpPut]
        public async Task<ResponseModel> Put([FromBody] NewsDTO value)
        {
            return await newsService.Put(value);
        }

        // DELETE api/<NewsController>/5
        [HttpDelete("{id}")]
        public async Task<ResponseModel> Delete(int id)
        {
            return await newsService.Delete(id);
        }
    }
}
