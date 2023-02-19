using beAware_models.DTOs.Category;
using beAware_services.Helpers;
using beAware_services.Services.CategoryService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace beAware_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        // GET: api/<CategoryController>
        [HttpGet]
        public async Task<ResponseModel> Get()
        {
            return await categoryService.GetAll();
        }

        [HttpGet("GetRequestedCategories")]
        public async Task<ResponseModel> GetRequestedCategories()
        {
            return await categoryService.GetRequestedCategories();
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public async Task<ResponseModel> GetById(int id)
        {
            return await categoryService.GetById(id);
        }

        // POST api/<CategoryController>
        [HttpPost]
        public async Task<ResponseModel> Post([FromBody] CategoryDTO value)
        {
            return await categoryService.Post(value);
        }

        [HttpPost("ApproveRequestedCategory")]
        public async Task<ResponseModel> ApproveRequestedCategory(int id)
        {
            return await categoryService.ApproveRequestedCategory(id);
        }

        [HttpPost("RequestCategory")]
        public async Task<ResponseModel> RequestCategory([FromBody] RequestedCategoryDTO value)
        {
            return await categoryService.RequestCategory(value);
        }

        // PUT api/<CategoryController>/5
        [HttpPut]
        public async Task<ResponseModel> Put([FromBody] CategoryDTO value)
        {
            return await categoryService.Put(value);
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public async Task<ResponseModel> Delete(int id)
        {
            return await categoryService.Delete(id);
        }
    }
}
