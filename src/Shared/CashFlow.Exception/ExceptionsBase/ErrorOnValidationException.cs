using System.Net;

namespace CashFlow.Exception.ExceptionsBase;

public class ErrorOnValidationException : CashFlowException
{
    private readonly List<string> _errorMessages;

    public override int StatusCode => (int)HttpStatusCode.BadRequest;

    public ErrorOnValidationException(List<string> errorMessages)
    {
        _errorMessages = errorMessages;
    }

    public override List<string> GetErrors()
    {
        return _errorMessages;
    }
}
