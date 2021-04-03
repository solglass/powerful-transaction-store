using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Core.Enums
{
    public static class FriendlyNames
    {
        public static string GetFriendlyCurrencyName(Currency attachmentType)
        {

            string friendlyName = attachmentType switch
            {
                Currency.RUB => "Рубль",
                Currency.USD => "Доллар",
                Currency.EUR => "Евро",
                Currency.JPY => "Иена",
                _ => "Некорректная валюта"
            };
            return friendlyName;

        }
        public static string GetFriendlyTransactionTypeName(TransactionType groupStatus)
        {
            string friendlyName = groupStatus switch
            {
                TransactionType.Deposit => "Внести на счет",
                TransactionType.Withdraw => "Снять со счета",
                TransactionType.Transfer => "Перевод",
                _ => "Некорретная операция"
            };
            return friendlyName;

        }
    }
}
