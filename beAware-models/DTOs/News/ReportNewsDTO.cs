using beAware_models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beAware_models.DTOs.News
{
    public class ReportNewsDTO
    {
        public string Reason { get; set; }
        public int NewsId { get; set; }
        public int? UserId { get; set; }
    }
}
