using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beAware_services.Services.ErrorLoggin
{
    public class ErrorLogginService : IErrorLogginService
    {
        public void LogError(Exception exception)
        {
            string filepath =  "~/Logs/";

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            filepath = filepath + DateTime.Today.ToString("dd-MM-yy") + ".txt";
            if (!File.Exists(filepath))
            {
                File.Create(filepath).Dispose();
            }
            using (StreamWriter sw = File.AppendText(filepath))
            {
                sw.WriteLine("=============Error Logging ===========");
                sw.WriteLine("===========Start============= " + DateTime.Now);
                sw.WriteLine("Error Message: " + exception.Message);
                sw.WriteLine("Stack Trace: " + exception.StackTrace);
                if (exception.InnerException != null)
                {
                    sw.WriteLine("InnerException Message: " + exception.InnerException);
                }
                sw.WriteLine("===========End============= " + DateTime.Now);
            }
        }
    }
}
