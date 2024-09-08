using System.Net;

namespace CashFlow.Exception.ExceptionsBase;
public class ConflictException : CashFlowException
{
    public ConflictException(string errorMessage) : base(errorMessage)
    {
    }

    public override int StatusCode => (int)HttpStatusCode.Conflict;

    public override List<string> GetErrors()
    {
        return new List<string> { Message };
    }
}
