using System.Text.Json.Serialization;

namespace CashFlow.Communication.Enums.Expenses;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PaymentType
{
    Cash, 
    CreditCard,
    DebitCard,
    EletronicTransfer
}
