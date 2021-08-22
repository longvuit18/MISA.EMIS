using Microsoft.AspNetCore.Http;
using MISA.AMIS.ApplicationCore.Constants;
using MISA.AMIS.ApplicationCore.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.AMIS.API.Middlewares
{
    /// <summary>
    /// Handle Error middleware 
    /// </summary>
    /// Created by: VLVU (4/8/2021)
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            // Log error TODO

            // Gửi lỗi về cho client
            var serviceResult = new ServiceResult()
            {
                Code = MisaCode.Exception,
                MsgUser = ApplicationCore.Properties.Resources.ExceptionError,
                MsgDev = ex.Message
            };

            var resultJson = JsonConvert.SerializeObject(serviceResult);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            return context.Response.WriteAsync(resultJson);
        }
    }
}
