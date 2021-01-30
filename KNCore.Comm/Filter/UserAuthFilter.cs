using KNCore.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KNCore.Comm.Filter
{
    public class UserAuthFilter:ActionFilterAttribute
    {

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            base.OnResultExecuting(context);
            dynamic result = context.Result as ObjectResult;
            var area = context.ActionDescriptor.RouteValues["area"].ToString();
            var controller = context.ActionDescriptor.RouteValues["controller"].ToString();
            var action = context.ActionDescriptor.RouteValues["action"].ToString();

            var r = result.Value;
            //dynamic main = result.Value;
            //dynamic msg = main.Message;
            //dynamic maindata = main.Main;

            var apiresult = new ApiResult() { Main = result, Message = "test" };
            context.Result = new OkObjectResult( apiresult);
            Console.WriteLine("OnActionExecuting");
        }

        public override Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            return base.OnResultExecutionAsync(context, next);
        }


        public override void OnResultExecuted(ResultExecutedContext context)
        {
            base.OnResultExecuted(context);
            
            Console.WriteLine("OnResultExecuted");
        }
    }
}
