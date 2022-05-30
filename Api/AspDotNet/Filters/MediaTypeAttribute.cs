using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;

namespace AspDotNet.Filters;

public class MediaTypeAttribute : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var accept_header_present = context.HttpContext.Request.Headers.ContainsKey("Accept");
        
        if (!accept_header_present)
        {
            context.Result = new BadRequestObjectResult($"Accept header is missing.");
            return;
        }
        
        string? media_type = context.HttpContext.Request.Headers["Accept"].FirstOrDefault();
        
        Console.WriteLine(media_type);
        
        if (!MediaTypeHeaderValue.TryParse(media_type, out var out_media_type))
        {
            context.Result = new BadRequestObjectResult($"Media type not present. Please add Accept header with the required media type.");
            return;
        }
        
        context.HttpContext.Items.Add("AcceptHeaderMediaType", out_media_type);
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}
