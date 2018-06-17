using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;
using Vidly.Models;

namespace Vidly.Exceptions
{
    public class ExceptionManager: ExceptionLogger
    {
        public override Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
        {
            var log = context.Exception.ToString();
            var _context = new ApplicationDbContext();
            _context.ExceptionLogs.Add(new ExceptionLog { Message = log });
            _context.SaveChangesAsync();
            return base.LogAsync(context, cancellationToken);
        }
        public override bool ShouldLog(ExceptionLoggerContext context)
        {
            return true; 
        }
        //public override Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        //{
        //    const string errorMessage = "An unexpected error occured";
        //    var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
        //    {
        //        Content = new StringContent(context.Exception.Message),
        //        ReasonPhrase = "Employee Not Found"
        //    };
        //    response.Headers.Add("X-Error", errorMessage);
        //    context.Result = new ResponseMessageResult(response);
        //    return base.HandleAsync(context, cancellationToken);
        //}
    }
}