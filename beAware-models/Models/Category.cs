using beAware_models.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beAware_models.Models
{
    public class Category : CommonEntities
    {
        public string Name { get; set; }
        public virtual ICollection<News> News { get; set; }
    }
}
