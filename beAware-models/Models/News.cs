using beAware_models.CommonModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beAware_models.Models
{
    public class News : CommonEntities
    {
        public string Title { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string CountryName { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Categories { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User Users { get; set; }
    }
}
