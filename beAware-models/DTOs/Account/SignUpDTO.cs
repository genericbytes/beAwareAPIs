﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beAware_models.DTOs.Account
{
    public class SignUpDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DOB { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public int RoleId { get; set; }
    }
}
