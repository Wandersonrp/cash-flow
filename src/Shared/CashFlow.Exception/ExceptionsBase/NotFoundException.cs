namespace CashFlow.Exception.ExceptionsBase;
public class NotFoundException : CashFlowException
{
    public NotFoundException(string errorMessage) : base(errorMessage)
    {
    }
}
