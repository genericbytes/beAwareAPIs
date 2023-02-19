using beAware_models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beAware_models.DTOs.Category
{
    public class RequestedCategoryDTO
    {
        public string Name { get; set; }
        public int UserId { get; set; }
    }
}
