using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeguridadOAuth
{
    public static class Rutas
    {
        public const string AuthorizationServerBaseAddress = "http://104.198.185.19:8080";

        public const string ResourceServerBaseAddress = "http://localhost:2253/";

        public const string AuthorizeCodeCallBackPath = "http://localhost:8660/";

        public const string AuthorizePath = "openam/oauth2/authorize";
        public const string TokenPath = "openam/oauth2/access_token";
        public const string LoginPath = "/Account/Login";
        public const string LogoutPath = "/Account/Logout";
        public const string MePath = "/api/Me";
    }
}
