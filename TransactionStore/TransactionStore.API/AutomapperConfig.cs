using AutoMapper;
using TransactionStore.Core.Enums;
using TransactionStore.API.Models.InputModels;
using TransactionStore.API.Models.OutputModels;
using TransactionStore.Core.Utils;
using TransactionStore.Core.Models;
using System.Collections.Generic;
using System;

namespace TransactionStore.API
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<SimpleTransactionDto, SimpleTransactionOutputModel>()
                .ForMember(dest => dest.Type, opts => opts.MapFrom(src => FriendlyNames.GetFriendlyTransactionTypeName(src.Type)));
            CreateMap<SimpleTransactionInputModel, SimpleTransactionDto>()
            .ForMember(dest => dest.Currency, opts => opts.MapFrom(src => src.Account.Currency))
            .ForMember(dest => dest.Amount, opts => opts.MapFrom(src => Converters.ConvertAmount(src.Value.Currency, src.Account.Currency, src.Value.Amount)));

            CreateMap<TransferDto, TransferOutputModel>()
                .ForMember(dest => dest.Type, opts => opts.MapFrom(src => FriendlyNames.GetFriendlyTransactionTypeName(src.Type)))
                .ForMember(dest => dest.SenderId, opts => opts.MapFrom(src => src.SenderAccountId))
                .ForMember(dest => dest.RecipientId, opts => opts.MapFrom(src => src.RecipientAccountId));
            CreateMap<TransferInputModel, TransferDto>()
                .ForMember(dest => dest.SenderAmount, opts => opts.MapFrom(src => src.Amount))
                .ForMember(dest => dest.RecipientAmount, opts => opts.MapFrom(src => Converters.ConvertAmount(src.SenderAccount.Currency, src.RecipientAccount.Currency, src.Amount)))
                .ForMember(dest => dest.SenderCurrency, opts => opts.MapFrom(src => Converters.ConvertCurrencyStringToCurrencyEnum(src.SenderAccount.Currency)))
                .ForMember(dest => dest.RecipientCurrency, opts => opts.MapFrom(src => Converters.ConvertCurrencyStringToCurrencyEnum(src.RecipientAccount.Currency)));


            CreateMap<BaseTransactionDto, BaseTransactionOutputModel>().Include<SimpleTransactionDto, SimpleTransactionOutputModel>().Include<TransferDto, TransferOutputModel>();
            CreateMap<AccountBalanceDto, AccountBalanceOutputModel>();
            CreateMap<WholeBalanceDto, WholeBalanceOutputModel>();
        }
    }
}
