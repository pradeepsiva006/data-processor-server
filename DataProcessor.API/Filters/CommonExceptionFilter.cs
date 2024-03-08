using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DataProcessor.API.Filters
{
    public class CommonExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            LogException(context.Exception);

            if (context.Exception is FormatException || context.Exception is ArgumentException)
            {
                context.Result = new ObjectResult(context.Exception.Message)
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
            else
            {
                context.Result = new ObjectResult(context.Exception.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

            context.ExceptionHandled = true;
        }

        private void LogException(Exception exception)
        {
            Console.WriteLine($"Exception logged: {exception.Message}");
        }
    }
}
