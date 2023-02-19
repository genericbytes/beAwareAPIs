using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beAware_services.Helpers
{
    public class ResponseModel
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public string ValidationMessage { get; set; }
        public object Data { get; set; }
    }
}
