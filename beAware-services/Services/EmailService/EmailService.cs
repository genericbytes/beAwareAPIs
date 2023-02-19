using AutoMapper;
using beAware_models.Models;
using beAware_services.Data;
using beAware_services.Enums;
using beAware_services.Helpers;
using beAware_services.Services.ErrorLoggin;
using EnumsNET;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace beAware_services.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IErrorLogginService errorLoggin;

        public EmailService(ApplicationDbContext context, IMapper mapper, IErrorLogginService errorLoggin)
        {
            this.context = context;
            this.mapper = mapper;
            this.errorLoggin = errorLoggin;
        }
        public async Task<ResponseModel> SendBlockedUserEmail(string email)
        {
			ResponseModel response = new ResponseModel();
			try
			{
                string MailText = "<html><head><title>Blocked User | BeAware</title></head><body>" +
                            "<p>You're blocked due to posted some abusive, violent or fake news on BeAware. You'll be able to use it again after one month.</p>" + 
                            "<p>Regards</p>" +
                            "<p>Team | BeAware</p>" +
                            "</body></html>";

                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true, //true
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("jamshaid.dev@gmail.com", "lbjjzcyzohbtotrd")
                };

                message.From = new MailAddress("jamshaid.dev@gmail.com", "BeAware");
                message.To.Add(new MailAddress(email));
                message.Subject = "Blocked By Admin | BeAware";
                message.Body = MailText;
                message.IsBodyHtml = true;
                smtp.Send(message);

                response.Status = true;
                response.Message = (ResponseEnums.Success).AsString(EnumFormat.Description);
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
