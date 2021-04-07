using AutoMapper;
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
            CreateMap<TransactionDto, TransactionOutputModel>()
                .ForMember(dest => dest.Type, opts => opts.MapFrom(src => FriendlyNames.GetFriendlyTransactionTypeName(src.Type)));
            CreateMap<TransactionInputModel, TransactionDto>()
            .ForMember(dest => dest.Currency, opts => opts.MapFrom(src => (Currency)Converters.CurrencyPairToCurrency(src.CurrencyPair)))
            .ForMember(dest => dest.Amount, opts => opts.MapFrom(src => Converters.ConvertAmount(src.CurrencyPair, src.Amount)));

            CreateMap<TransferDto, TransferOutputModel>();
            CreateMap<TransferInputModel, TransferDto>()
            .ForMember(dest => dest.Currency, opts => opts.MapFrom(src => (Currency)Converters.CurrencyPairToCurrency(src.CurrencyPair)))
            .ForMember(dest => dest.Amount, opts => opts.MapFrom(src => Converters.ConvertAmount(src.CurrencyPair, src.Amount)));

            CreateMap<LeadBalanceDto, LeadBalanceOutputModel>();
        }
    }
}
