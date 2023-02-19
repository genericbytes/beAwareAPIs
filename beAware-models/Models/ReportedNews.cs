using beAware_models.CommonModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beAware_models.Models
{
    public class ReportedNews : CommonEntities
    {
        public string Reason { get; set; }

        [ForeignKey("News")]
        public int NewsId { get; set; }
        public News News { get; set; }

        [ForeignKey("User")]
        public int? UserId { get; set; }
        public User Users { get; set; }
    }
}
