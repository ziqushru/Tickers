using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace AspDotNet.Filters;

public class ValidationFilterAttribute : IActionFilter
{
    private readonly ILogger logger;
    
    public ValidationFilterAttribute(ILogger logger)
    {
        this.logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var action = context.RouteData.Values["action"];
        var controller = context.RouteData.Values["controller"];

        var param = context.ActionArguments
            .SingleOrDefault(x => x.Value != null && x.Value.ToString()!.Contains("Dto")).Value;

        if (context.ModelState.IsValid) return;
        logger.Error("Invalid model state for the object. Controller: {Controller}, action: {Action}", controller, action);
        context.Result = new UnprocessableEntityObjectResult(context.ModelState);
    }

    public void OnActionExecuted(ActionExecutedContext context){}
}
