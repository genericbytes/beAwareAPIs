using beAware_services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beAware_services.Services.MediaService
{
    public interface IMediaUploadService
    {
        Task<ResponseModel> UploadNewsImage(string virtualImg, int id);
    }
}
