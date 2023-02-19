using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beAware_models.CommonModels
{
    public class CommonEntities
    {
        public CommonEntities()
        {
            CreatedDateTime = DateTime.Now;
            IsActive = true;
            IsDeleted = false;
        }
        public int Id { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
