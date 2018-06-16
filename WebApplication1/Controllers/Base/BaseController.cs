using log4net;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Enums;
using WebApplication1.Helpers;
using WebApplication1.Services;

namespace WebApplication1.Controllers.Base
{
    public class BaseController: Controller
    {
        protected readonly ILog Log = CustomLogger.Log();

        protected JsonResult ToJsonResult<T>(Result<T> serviceResult) => 
            serviceResult.ResultType != ResultType.Fail ?
                ToJsonResult(false, serviceResult.Data) :
                ToJsonResult(true, "Operation Failed");

        private JsonResult ToJsonResult<T>(bool isError, T data = default(T)) => 
            Json(new { isError, message = data });
    }
}
