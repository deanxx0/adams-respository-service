using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace adams_repository_service.Models
{
    public class CreateUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserClaim { get; set; }

        public CreateUser()
        {

        }

        public CreateUser(string userName, string password, string userClaim)
        {
            UserName = userName;
            Password = password;
            UserClaim = userClaim;
        }
    }
}
