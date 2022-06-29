using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace S
{
    public class Admin: AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session["type"] !=null && httpContext.Session["type"].ToString() == "admin")
            {
                return true;
            }
            return false;
        }
    }
}