using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASBM.Models
{
    public class LoginModel
    {
        public string userId { get; set; }
        public string userRole { get; set; }
        public int userRoleId { get; set; }
        public string user_type_name { get; set; }
    }
}