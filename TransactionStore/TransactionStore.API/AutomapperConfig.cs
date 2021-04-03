using AutoMapper;
using EducationSystem.Core.Enums;
using TransactionStore.API.Models.InputModels;
using TransactionStore.API.Models.OutputModels;
using TransactionStore.Core.Models;

namespace TransactionStore.API
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<TransactionDto, TransactionOutputModel>()
                .ForMember(dest => dest.Currency, opts => opts.MapFrom(src => FriendlyNames.GetFriendlyCurrencyName(src.Currency)))
                .ForMember(dest => dest.Type, opts => opts.MapFrom(src => FriendlyNames.GetFriendlyTransactionTypeName(src.Type)));
            CreateMap<TransactionInputModel, TransactionDto>();

            CreateMap<TransferDto, TransferOutputModel>();
            CreateMap<TransferInputModel, TransferDto>();

            CreateMap<LeadBalanceDto, LeadBalanceOutputModel>()
                .ForMember(dest => dest.Currency, opts => opts.MapFrom(src => FriendlyNames.GetFriendlyCurrencyName(src.Currency)));
        }
    }
}
