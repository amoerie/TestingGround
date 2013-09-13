using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMatrix.WebData;

namespace TestingGround.Web
{
    public static class SecurityConfig
    {
        public static void Register()
        {
            //WebSecurity.InitializeDatabaseConnection("DbContext", "UserProfile", "Id", "UserName", autoCreateTables: true);
        }
    }
}