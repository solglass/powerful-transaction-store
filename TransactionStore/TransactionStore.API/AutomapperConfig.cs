using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionStore.API.Models.InputModels;
using TransactionStore.API.Models.OutputModels;
using TransactionStore.Core.Models;

namespace TransactionStore.API
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<TransactionDto, TransactionOutputModel>();
            CreateMap<TransactionInputModel, TransactionDto>();

            CreateMap<TransferDto, TransferOutputModel>();
            CreateMap<TransferInputModel, TransferDto>();

            CreateMap<LeadBalanceDto, LeadBalanceOutputModel>();
        }
    }
}
