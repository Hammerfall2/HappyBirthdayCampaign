using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HappyBirthdayCampain.UI.ExceptionUI
{
    public class MyExceptionHandler : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {

            TempDataDictionary keyValuePairs = new TempDataDictionary
            {
                { "0", filterContext.Exception.Source },
                { "1", filterContext.Exception.Message }
            };

            filterContext.ExceptionHandled = true;
            filterContext.Result = new ViewResult()
            {
                ViewName = "Error",
                TempData = keyValuePairs

        };
        }
    }
}