namespace TransactionStore.Core.Enums
{
    public static class FriendlyNames
    {

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
