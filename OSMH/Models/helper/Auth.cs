using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OSMH.Models.helper
{
    public static class Auth
    {
        public static bool checkLogin()
        {
            if (HttpContext.Current.Session["userid"] == null)
            {
                return false;
            }
            return true;
        }
    }
}