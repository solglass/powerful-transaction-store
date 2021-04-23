using AutoMapper;
using TransactionStore.Core.Enums;
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
            .ForMember(dest => dest.Currency, opts => opts.MapFrom(src => Converters.ConvertRecipientAccountCurrencyPairToCurrency(src.CurrencyPair)))
            .ForMember(dest => dest.Amount, opts => opts.MapFrom(src => Converters.ConvertAmount(src.CurrencyPair, src.Amount)));

            CreateMap<TransferDto, TransferOutputModel>()
                .ForMember(dest => dest.Type, opts => opts.MapFrom(src => FriendlyNames.GetFriendlyTransactionTypeName(src.Type)));
            CreateMap<TransferInputModel, TransferDto>()
                .ForMember(dest => dest.SenderAccountAmount, opts => opts.MapFrom(src => src.Amount))
                .ForMember(dest => dest.SenderAccountCurrency, opts => opts.MapFrom(src => Converters.ConvertSenderAccountCurrencyPairToCurrency(src.CurrencyPair)))
                .ForMember(dest => dest.RecipientAccountCurrency, opts => opts.MapFrom(src => Converters.ConvertRecipientAccountCurrencyPairToCurrency(src.CurrencyPair)))
                .ForMember(dest => dest.RecipientAccountAmount, opts => opts.MapFrom(src => Converters.ConvertAmount(src.CurrencyPair, src.Amount)));

            CreateMap<BaseTransactionDto, BaseTransactionOutputModel>().Include<SimpleTransactionDto, SimpleTransactionOutputModel>().Include<TransferDto, TransferOutputModel>();
            CreateMap<AccountBalanceDto, AccountBalanceOutputModel>();
        }
    }
}
