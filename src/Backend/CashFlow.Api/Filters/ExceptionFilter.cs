using CashFlow.Communication.Responses.Errors;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CashFlow.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if(context.Exception is CashFlowException)
        {
            HandleProjectException(context);
        }
        else 
        {
            ThrowUnknownException(context);
        }        
    }

    private static void ThrowUnknownException(ExceptionContext context)
    {        
        var errorResponse = new ResponseErrorJson(ResourceErrorMessages.UNKNOWN_ERROR);

        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }

    private static void HandleProjectException(ExceptionContext context)
    {
        var cashFlowException = context.Exception as CashFlowException;

        var errorResponse = new ResponseErrorJson(cashFlowException!.GetErrors());

        context.HttpContext.Response.StatusCode = cashFlowException.StatusCode;

        context.Result = new ObjectResult(errorResponse);
    }
}
