using CashFlow.Communication.Enums.Expenses;

namespace CashFlow.Domain.Extensions;
public static class PaymentTypeExtensions
{
    public static string ToFormatString(this PaymentType paymentType)
    {        
        return paymentType switch
        {
            PaymentType.Cash => "Dinheiro",
            PaymentType.CreditCard => "Cartão de Crédito",
            PaymentType.DebitCard => "Cartão de Débito",
            PaymentType.EletronicTransfer => "Transferência Eletrônica",
            _ => string.Empty
        };
    }
}
