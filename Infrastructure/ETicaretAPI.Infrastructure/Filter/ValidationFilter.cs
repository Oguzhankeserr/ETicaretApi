using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Infrastructure.Filter
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                    .Where(x => x.Value.Errors.Any())
                    .ToDictionary(e => e.Key, e => e.Value.Errors.Select(e => e.ErrorMessage))
                    .ToArray();

                context.Result = new BadRequestObjectResult(errors);
                return;
            }

            var resultContext = await next();
            if (resultContext.Result is ObjectResult objectResult && objectResult.StatusCode == 200)
            {
                // Eğer yanıt 200 OK ise, Status Code'u 200 OK olarak ayarla
                objectResult.StatusCode = 200;
            }
        }
    }
}