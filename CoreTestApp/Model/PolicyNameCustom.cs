using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreTestApp.Model
{
    public static class PolicyNameCustom
    {
        public const string Admin = nameof(Admin);
        public const string User = nameof(User);
    }

    public static class IncludeRoles
    {
        public const string Admin = nameof(Admin);
        public static string[] User =  { PolicyNameCustom.Admin, PolicyNameCustom.User } ;
    }
}
