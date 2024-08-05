using Ecommercer.Communictaion.Response;
using Ecommercer.Exception.ExceptionsBase;
using Ecommercer.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Ecommercer.Api.Filters
{
    public class FiltroExecao : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ExceptionTratamento)
                HandleProjectExcption(context);
            else
                ExcecaoDesconhecida(context);
            


        }

        private void HandleProjectExcption(ExceptionContext context)
        {
            if (context.Exception is ErroException) ;
            {
                var excecao = context.Exception as ErroException;
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Result = new BadRequestObjectResult(new ResponseErrorJson(excecao.ErroMessages));
            }
        }

        private void ExcecaoDesconhecida(ExceptionContext context)
        {

            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Result = new ObjectResult(new ResponseErrorJson(ResourceMessagesException.UNKNOWN_ERROR));

        }
    }
}
