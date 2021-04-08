﻿using AutoMapper;
using EducationSystem.Core.Enums;
using TransactionStore.API.Models.InputModels;
using TransactionStore.API.Models.OutputModels;
using TransactionStore.API.Utils;
using TransactionStore.Core.Models;

namespace TransactionStore.API
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<SimpleTransactionDto, SimpleTransactionOutputModel>()
                .ForMember(dest => dest.Type, opts => opts.MapFrom(src => FriendlyNames.GetFriendlyTransactionTypeName(src.Type)));
            CreateMap<SimpleTransactionInputModel, SimpleTransactionDto>()
            .ForMember(dest => dest.Currency, opts => opts.MapFrom(src => Converters.RecipientCurrencyPairToCurrency(src.CurrencyPair)))
            .ForMember(dest => dest.Amount, opts => opts.MapFrom(src => Converters.ConvertAmount(src.CurrencyPair, src.Amount)));

            CreateMap<TransferDto, TransferOutputModel>()
                .ForMember(dest => dest.Type, opts => opts.MapFrom(src => FriendlyNames.GetFriendlyTransactionTypeName(src.Type)));
            CreateMap<TransferInputModel, TransferDto>()
                .ForMember(dest => dest.SenderAmount, opts => opts.MapFrom(src => src.Amount))
                .ForMember(dest => dest.SenderCurrency, opts => opts.MapFrom(src => Converters.SenderCurrencyPairToCurrency(src.CurrencyPair)))
                .ForMember(dest => dest.RecipientCurrency, opts => opts.MapFrom(src => Converters.RecipientCurrencyPairToCurrency(src.CurrencyPair)))
                .ForMember(dest => dest.RecipientAmount, opts => opts.MapFrom(src => Converters.ConvertAmount(src.CurrencyPair, src.Amount)));

            CreateMap<BaseTransactionDto, BaseTransactionOutputModel>().Include<SimpleTransactionDto, SimpleTransactionOutputModel>().Include<TransferDto, TransferOutputModel>();
            CreateMap<LeadBalanceDto, LeadBalanceOutputModel>();
        }
    }
}
