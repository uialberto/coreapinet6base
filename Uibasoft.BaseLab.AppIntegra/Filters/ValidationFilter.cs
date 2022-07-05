using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Uibasoft.BaseLab.Domain.CustomEntities;
using Uibasoft.BaseLab.Domain.Enumerations;
using LocalizedText = Uibasoft.BaseLab.AppIntegra.Filters.Localization.ValidationFilter;

namespace Uibasoft.BaseLab.AppIntegra.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.SelectMany(m => m.Value.Errors.Select(me => new BaseErrorMessage()
                {
                    Key = m.Key,
                    Message = me.ErrorMessage,
                    Type = (int)TypeErrorCodeEnum.GenericError

                })).ToList();

                var apiResponse = new ApiResponse<string>(null, (int)TypeErrorCodeEnum.GenericError,
                    LocalizedText.ValidationErrorFilter, errors);

                context.HttpContext.Response.ContentType = MediaTypeNames.Application.Json;
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Result = new JsonResult(apiResponse)
                {
                    ContentType = MediaTypeNames.Application.Json,
                    StatusCode = StatusCodes.Status400BadRequest
                };
                return;
            }
            await next();
        }
    }
}
