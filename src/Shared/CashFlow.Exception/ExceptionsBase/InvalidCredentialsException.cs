
using System.Net;

namespace CashFlow.Exception.ExceptionsBase;
public class InvalidCredentialsException : CashFlowException
{
    public InvalidCredentialsException(string errorMessage) : base(errorMessage)
    {
    }

    public override int StatusCode => (int)HttpStatusCode.Unauthorized;

    public override List<string> GetErrors()
    {
        return new List<string> { Message };
    }
}
