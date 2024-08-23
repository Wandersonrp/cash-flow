namespace CashFlow.Communication.Responses.Errors;

public record ResponseErrorJson
{
    public List<string> ErrorMessages { get; set; }

    public ResponseErrorJson(List<string> errorMessages)
    {
        ErrorMessages = errorMessages;
    }   

    public ResponseErrorJson(string errorMessage)
    {
        ErrorMessages = [errorMessage];
    }
}
