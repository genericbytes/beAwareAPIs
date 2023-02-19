using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beAware_services.Enums
{
    public enum ValidationMessage
    {
        [Description("There is an expection. Please contact admin.")]
        Expection = 1,
        [Description("Data Not Found")]
        DataNotFound = 2,
        [Description("Email or Username already exist in the system.")]
        EmailOrUserNameAlreadyExist = 3,
        [Description("Invalid Credentials. Please try again with valid credentials.")]
        InvalidCredentials = 4,
        [Description("Your account has been blocked by admin. Please contact admin.")]
        BlockedByAdmin = 5,
        [Description("Category is already exist in the system.")]
        CategoryAlreadyExist = 6,
        [Description("News Title is already exist in the system.")]
        NewsTitleAlreadyExist = 6,
        [Description("News is already reported.")]
        NewsAlreadyReported = 7,
    }
}
