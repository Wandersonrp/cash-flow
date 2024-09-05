using CashFlow.Communication.Enums.Expenses;

namespace CashFlow.Web.Client.StringConverters;

public static class FormatPaymentType
{
    public static string ToFriendlyString(this PaymentType paymentType)
    {
        return paymentType switch
        {
            PaymentType.Cash => "Dinheiro",
            PaymentType.CreditCard => "Cartão de Crédito",
            PaymentType.DebitCard => "Cartão de Débito",
            PaymentType.EletronicTransfer => "Transferência Eletrônica",
            _ => throw new ArgumentOutOfRangeException(nameof(paymentType), paymentType, null)
        };
    }
}
