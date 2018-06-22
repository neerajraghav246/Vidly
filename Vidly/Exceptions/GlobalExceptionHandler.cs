using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace Vidly.Exceptions
{
    public class GlobalExceptionHandler : ExceptionHandler
    {
        //public override void Handle(ExceptionHandlerContext context)
        //{
        //    context.Result = new ExceptionResponse
        //    {
        //        statusCode = context.Exception is SecurityException ? HttpStatusCode.Unauthorized : HttpStatusCode.InternalServerError,
        //        message = "An internal exception occurred. We'll take care of it.",
        //        request = context.Request
        //    };

        //}
        public override Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            var log = $"Method : {context.Request.Method}, Uri: {context.Request.RequestUri}, Exception : {context.Exception.ToString()}";
            context.Result = new ExceptionResponse
            {
                statusCode = context.Exception is SecurityException ? HttpStatusCode.Unauthorized : HttpStatusCode.InternalServerError,
                message = "An internal exception occurred. We'll take care of it.",
                request = context.Request
            };
            return base.HandleAsync(context, cancellationToken);
        }
        public override bool ShouldHandle(ExceptionHandlerContext context)
        {
            return true;
        }
    }

    public class ExceptionResponse : IHttpActionResult
    {
        public HttpStatusCode statusCode { get; set; }
        public string message { get; set; }
        public HttpRequestMessage request { get; set; }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(statusCode);
            response.RequestMessage = request;
            response.Content = new StringContent(message);
            return Task.FromResult(response);
        }
    }

}