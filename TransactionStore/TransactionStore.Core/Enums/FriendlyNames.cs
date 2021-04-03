using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Core.Enums
{
    public static class FriendlyNames
    {
        public static string GetFriendlyCurrencyName(Currency currencyType)
        {

            string friendlyName = currencyType switch
            {
                Currency.RUB => "Рубль",
                Currency.USD => "Доллар",
                Currency.EUR => "Евро",
                Currency.JPY => "Иена",
                _ => "Некорректная валюта"
            };
            return friendlyName;

        }

        public static string GetFriendlyTransactionTypeName(TransactionType transactionType)
        {
            string friendlyName = transactionType switch
            {
                TransactionType.Deposit => "Внесение",
                TransactionType.Withdraw => "Снятие",
                TransactionType.Transfer => "Перевод",
                _ => "Некорретная операция"
            };
            return friendlyName;
        }
    }
}
