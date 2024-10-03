using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ThuvienMvc.Models.Authentications
{
    public class AuthenAdmin : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userRole = context.HttpContext.Session.GetString("UserRole");
            if (userRole == null)
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        {"controller","User" },
                        {"action","SignIn" }
                    });
            }
            else if(userRole == "user")
            {
                context.Result = new RedirectToRouteResult(
                   new RouteValueDictionary
                   {
                        {"controller","Book" },
                        {"action","Index" }
                   });
            }

        }
    }
}
