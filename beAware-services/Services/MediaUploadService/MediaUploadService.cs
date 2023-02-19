using beAware_services.Data;
using beAware_services.Enums;
using beAware_services.Helpers;
using beAware_services.Services.ErrorLoggin;
using EnumsNET;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace beAware_services.Services.MediaService
{
    public class MediaUploadService : IMediaUploadService
    {
        private readonly ApplicationDbContext context;
        private readonly IErrorLogginService errorLoggin;

        public MediaUploadService(ApplicationDbContext context, IErrorLogginService errorLoggin)
        {
            this.context = context;
            this.errorLoggin = errorLoggin;
        }

        public async Task<ResponseModel> UploadNewsImage(string virtualImg, int id)
        {
            ResponseModel response = new ResponseModel();

            try
            {
                if (!string.IsNullOrWhiteSpace(virtualImg))
                {
                    var imagedate = DateTime.Now.Ticks;
                    var data = context.News.FirstOrDefault(x => x.Id.Equals(id));

                    string nameWithExtension = imagedate.ToString() + Guid.NewGuid().ToString().Substring(0, 4) + ".jpeg";
                    data.Image = "Images/NewsImage/" + nameWithExtension;
                    await context.SaveChangesAsync();

                    string path = "wwwroot/Images/NewsImage/"; //Path

                    //Check if directory exist
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path); //Create directory if it doesn't exist
                    }

                    string imageName = nameWithExtension;

                    //set the image path
                    string imgPath = Path.Combine(path, imageName);

                    string convert = virtualImg.Substring(virtualImg.LastIndexOf(',') + 1);

                    byte[] imageBytes = Convert.FromBase64String(convert);

                    //Image Reduction
                    Image tmpOriginalImage = Image.FromStream(new MemoryStream(imageBytes));
                    double dblScaleImg = 640 / (double)tmpOriginalImage.Width;

                    Graphics tmpGraphics = default;
                    Bitmap tmpResizedImage = new Bitmap(Convert.ToInt32(dblScaleImg * tmpOriginalImage.Width), Convert.ToInt32(dblScaleImg * tmpOriginalImage.Height));
                    tmpGraphics = Graphics.FromImage(tmpResizedImage);

                    tmpGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;

                    tmpGraphics.DrawImage(tmpOriginalImage, 0, 0, tmpResizedImage.Width + 1, tmpResizedImage.Height + 1);

                    Image imageOut = tmpResizedImage;
                    imageOut.Save(imgPath, System.Drawing.Imaging.ImageFormat.Jpeg);

                    response.Status = true;
                    response.Message = (ResponseEnums.Success).AsString(EnumFormat.Description);
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = (ResponseEnums.Failure).AsString(EnumFormat.Description);
                response.ValidationMessage = (ValidationMessage.Expection).AsString(EnumFormat.Description);
                errorLoggin.LogError(ex);
            }

            return response;
        }
    }
}
