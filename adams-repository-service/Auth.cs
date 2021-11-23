using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace adams_repository_service.Auth
{
    public static class PolicyNames
    {
        public const string MemberOrAdmin = "MemberOrAdmin";
        public const string AdminOnly = "AdminOnly";
    }


    public static class ClaimNames
    {
        public const string Member = "Member";
        public const string Admin = "Admin";
    }
}
