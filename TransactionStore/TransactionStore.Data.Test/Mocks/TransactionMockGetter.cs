using TransactionStore.Core.Models;
using System;
using System.Diagnostics.CodeAnalysis;

namespace TransactionStore.Data.Tests.Mocks
{
    [ExcludeFromCodeCoverage]
    public static class TransactionMockGetter
    {
        public static SimpleTransactionDto GetSimpleTransactionDto(int mockId)
        {

            SimpleTransactionDto transactionDto = mockId switch
            {
                1 => new SimpleTransactionDto()
                {
                 
                },

                2 => new SimpleTransactionDto()
                {

                },
                3 => new SimpleTransactionDto(),
                _ => throw new NotImplementedException()
            };

            return transactionDto;
        }

    }
}