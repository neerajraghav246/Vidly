using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.ExceptionHandling;

namespace Vidly.Exceptions
{
   public class GlobalExceptionHandler : ExceptionHandler
{
        public override Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            return base.HandleAsync(context, cancellationToken);    
        }
        public override bool ShouldHandle(ExceptionHandlerContext context)
        {
            return true;
        }
    }
    

}