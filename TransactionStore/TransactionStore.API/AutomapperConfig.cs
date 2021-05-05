using AutoMapper;
using TransactionStore.Core.Enums;
using TransactionStore.API.Models.InputModels;
using TransactionStore.API.Models.OutputModels;
using TransactionStore.Business;
using TransactionStore.Core.Models;

namespace TransactionStore.API
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<SimpleTransactionDto, SimpleTransactionOutputModel>()
                .ForPath(dest => dest.Type, opts => opts.MapFrom(src => FriendlyNames.GetFriendlyTransactionTypeName(src.Type)))
                .ForPath(dest => dest.Value.Amount, opts => opts.MapFrom(src => src.Amount))
                .ForPath(dest => dest.Value.Currency, opts => opts.MapFrom(src => src.Currency));
            CreateMap<SimpleTransactionInputModel, SimpleTransactionDto>()
            .ForPath(dest => dest.Currency, opts => opts.MapFrom(src => src.Account.Currency))
            .ForPath(dest => dest.AccountId, opts => opts.MapFrom(src => src.Account.AccountId))
            .ForPath(dest => dest.Amount, opts => opts.MapFrom(src => ConverterService.ConvertAmount(src.Value.Currency, src.Account.Currency, src.Value.Amount)));

            CreateMap<TransferDto, TransferOutputModel>()
                .ForPath(dest => dest.Type, opts => opts.MapFrom(src => FriendlyNames.GetFriendlyTransactionTypeName(src.Type)))
                .ForPath(dest => dest.Sender.AccountId, opts => opts.MapFrom(src => src.SenderAccountId))
                .ForPath(dest => dest.Sender.Currency, opts => opts.MapFrom(src => src.SenderCurrency))
                .ForPath(dest => dest.Recipient.AccountId, opts => opts.MapFrom(src => src.RecipientAccountId))
                .ForPath(dest => dest.Recipient.Currency, opts => opts.MapFrom(src => src.RecipientCurrency));
            CreateMap<TransferInputModel, TransferDto>()
                .ForPath(dest => dest.SenderAccountId, opts => opts.MapFrom(src => src.SenderAccount.AccountId))
                .ForPath(dest => dest.RecipientAccountId, opts => opts.MapFrom(src => src.RecipientAccount.AccountId))
                .ForPath(dest => dest.SenderAmount, opts => opts.MapFrom(src => src.Amount))
                .ForPath(dest => dest.RecipientAmount, opts => opts.MapFrom(src => ConverterService.ConvertAmount(src.SenderAccount.Currency, src.RecipientAccount.Currency, src.Amount)))
                .ForPath(dest => dest.SenderCurrency, opts => opts.MapFrom(src => ConverterService.ConvertCurrencyStringToCurrencyEnum(src.SenderAccount.Currency)))
                .ForPath(dest => dest.RecipientCurrency, opts => opts.MapFrom(src => ConverterService.ConvertCurrencyStringToCurrencyEnum(src.RecipientAccount.Currency)));


            CreateMap<BaseTransactionDto, BaseTransactionOutputModel>().Include<SimpleTransactionDto, SimpleTransactionOutputModel>().Include<TransferDto, TransferOutputModel>();
            CreateMap<AccountBalanceDto, AccountBalanceOutputModel>();
            CreateMap<WholeBalanceDto, WholeBalanceOutputModel>();
        }
    }
}
