using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace AutoservWebAPI.ExeptionFilters
{
    /// <summary>
    /// ExeptionFilter
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ExeptionFilter : ExceptionFilterAttribute
    {
        /// <summary>
        /// Override exeption filter
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case ArgumentException e:
                    context.Result = new NotFoundObjectResult(e.Message);
                    break;
                case InvalidOperationException e:
                    context.Result = new BadRequestObjectResult(e.Message);
                    break;
                case DbUpdateException e:
                    context.Result = new BadRequestObjectResult($"Does not create new subject. \n Error:{e.InnerException}");
                    break;
            }
        }
    }
}
