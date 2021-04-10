using TransactionStore.Core.Models;
using System;
using System.Diagnostics.CodeAnalysis;

namespace TransactionStore.Data.Tests.Mocks
{
    [ExcludeFromCodeCoverage]
    public static class TransferMockGetter
    {
        public static TransferDto GetTransferDtoMock(int mockId)
        {

            TransferDto transferDto = mockId switch
            {
                1 => new TransferDto()
                {

                },

                2 => new TransferDto()
                {

                },
                3 => new TransferDto(),
                _ => throw new NotImplementedException()
            };

            return transferDto;
        }

    }
}