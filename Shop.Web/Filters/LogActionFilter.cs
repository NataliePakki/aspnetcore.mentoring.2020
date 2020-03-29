using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Serilog;

namespace Shop.Web.Filters
{
    public class LogActionAttribute : ActionFilterAttribute
    {
        private bool _log;

        public LogActionAttribute(bool log = false)
        {
            _log = log;
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            LogAction("OnActionExecuting", filterContext.RouteData);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            LogAction("OnActionExecuted", filterContext.RouteData);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            LogAction("OnResultExecuting", filterContext.RouteData);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            LogAction("OnResultExecuted", filterContext.RouteData);
        }

        private void LogAction(string methodName, RouteData routeData)
        {
            if (_log)
            {
                var controllerName = routeData.Values["controller"];
                var actionName = routeData.Values["action"];
                var message = string.Format("{0} controller:{1} action:{2}", methodName, controllerName, actionName);
                Log.Information(message);
            }
        }
    }
}
