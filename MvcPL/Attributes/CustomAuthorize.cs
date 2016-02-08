//using System.Web.Mvc;
//using System.Web.Routing;
//using BLL.Interface.Services;

//namespace MvcPL.Attributes
//{
//    public class CustomAuthorize : AuthorizeAttribute
//    {
//        private IUserService UserService
//            => (IUserService) System.Web.Mvc.DependencyResolver.Current.GetService(typeof (IUserService));

//        public override void OnAuthorization(AuthorizationContext filterContext)
//        {
//            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
//            {
//                var user = UserService.GetUserEntityByLogin(filterContext.HttpContext.User.Identity.Name);
//                if (user.IsBlocked)
//                {
//                    filterContext.Result = new RedirectToRouteResult(new
//                        RouteValueDictionary(new { controller = "User", action = "UserBlocked"}));
//                }
//                else
//                {
//                    filterContext.Result = new RedirectToRouteResult(new
//                       RouteValueDictionary(new { controller = filterContext.RouteData.Values["controller"],
//                           action = filterContext.RouteData.Values["action"]}));
//                    //HandleUnauthorizedRequest(filterContext);
//                }
//            }
//            else
//            {
//                //filterContext.Result = new RedirectToRouteResult(new
//                //    RouteValueDictionary(new { controller = "Account", action = "Login" }));
//                HandleUnauthorizedRequest(filterContext);
//            }
//        }
//    }
//}