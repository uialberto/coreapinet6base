using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Uibasoft.BaseLab.Domain.CustomEntities;
using Uibasoft.BaseLab.Domain.Enumerations;
using Uibasoft.BaseLab.Domain.Exceptions;
using LocalizedText = Uibasoft.BaseLab.AppIntegra.Filters.Localization.GlobalExceptionFilter;

namespace Uibasoft.BaseLab.AppIntegra.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;
        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> pLogger)
        {
            _logger = pLogger ?? throw new ArgumentNullException(nameof(pLogger));
        }
        public void OnException(ExceptionContext context)
        {
            _logger?.LogError($"{this.GetType().Name},{System.Reflection.MethodBase.GetCurrentMethod().Name} , JsonData: {JsonConvert.SerializeObject(context.Exception)}");

            var error = new BaseErrorMessage()
            {
                Message = $"{context.Exception.Message}. Source: {context?.Exception?.Source?.ToUpper()}",
                Type = (int)TypeErrorCodeEnum.GenericError,
                Key = context?.Exception?.Source?.ToUpper()
            };


            var errors = new List<BaseErrorMessage>() { error };

            var response = new ApiResponse<string>(null, (int)TypeErrorCodeEnum.GenericError,
                LocalizedText.ExcepcionesProducidas, errors)
            {
                Status = (int)TypeErrorCodeEnum.GenericError
            };

            if (context.Exception.GetType() == typeof(BussinesException))
            {
                context.HttpContext.Response.ContentType = MediaTypeNames.Application.Json;
                context.Result = new JsonResult(response)
                {
                    ContentType = MediaTypeNames.Application.Json,
                    StatusCode = StatusCodes.Status400BadRequest
                };
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.ExceptionHandled = true;
            }
            else
            {
                context.HttpContext.Response.ContentType = MediaTypeNames.Application.Json;
                context.Result = new JsonResult(response)
                {
                    ContentType = MediaTypeNames.Application.Json,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.ExceptionHandled = true;
            }
        }
    }
}
