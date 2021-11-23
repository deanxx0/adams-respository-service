using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace adams_repository_service.Auth
{
    public static class PolicyNames
    {
        public static readonly string MemberOrAdmin = "MemberOrAdmin";
        public static readonly string AdminOnly = "AdminOnly";
    }


    public static class ClaimNames
    {
        public static readonly string Member = "Member";
        public static readonly string Admin = "Admin";
    }
}
