using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Uibasoft.BaseLab.Domain.CustomEntities;
using Uibasoft.BaseLab.Domain.Enumerations;

using LocalizedText = Uibasoft.BaseLab.AppIntegra.Extensions.Localization.RequestAuthFarmaMiddleware;

namespace Uibasoft.BaseLab.AppIntegra.Extensions
{
    public static class AuthFarmaMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthFarma(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestAuthFarmaMiddleware>();
        }
    }
    public class RequestAuthFarmaMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestAuthFarmaMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);

            #region Status401Unauthorized

            if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
            {
                var errors = new List<BaseErrorMessage>()
                            {
                                new BaseErrorMessage()
                                {
                                    Key = StatusCodes.Status401Unauthorized.ToString(),
                                    Message = LocalizedText.ErrorAutenticacion,
                                    Type = (int)TypeErrorCodeEnum.GenericError
                                }
                            };

                var response = new ApiResponse<string>(null, (int)TypeErrorCodeEnum.GenericError,
                    LocalizedText.TitleErrorAutenticacion, errors);
                response.Status = (int)TypeErrorCodeEnum.GenericError;

                context.Response.ContentType = MediaTypeNames.Application.Json;

                var jsonLiteralize = Newtonsoft.Json.JsonConvert.SerializeObject(response, Newtonsoft.Json.Formatting.Indented, new Newtonsoft.Json.JsonSerializerSettings()
                {
                    NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
                });

                await context.Response.WriteAsync(jsonLiteralize);
            }

            #endregion

            #region Status403Forbidden

            if (context.Response.StatusCode == (int)HttpStatusCode.Forbidden)
            {
                var errors = new List<BaseErrorMessage>()
                            {
                                new BaseErrorMessage()
                                {
                                    Key = StatusCodes.Status403Forbidden.ToString(),
                                    Message = LocalizedText.ErrorAuthorizationRequerido,
                                    Type = (int)TypeErrorCodeEnum.GenericError
                                }
                            };

                var response = new ApiResponse<string>(null, (int)TypeErrorCodeEnum.GenericError,
                    LocalizedText.TitleAuthorizationRequerido, errors);
                response.Status = (int)TypeErrorCodeEnum.GenericError;

                context.Response.ContentType = MediaTypeNames.Application.Json;

                var jsonLiteralize = Newtonsoft.Json.JsonConvert.SerializeObject(response, Newtonsoft.Json.Formatting.Indented, new Newtonsoft.Json.JsonSerializerSettings()
                {
                    NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
                });

                await context.Response.WriteAsync(jsonLiteralize);
            }

            #endregion

        }
    }
}
