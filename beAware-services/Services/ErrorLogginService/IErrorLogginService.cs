using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beAware_services.Services.ErrorLoggin
{
    public interface IErrorLogginService
    {
        void LogError(Exception exception);
    }
}
