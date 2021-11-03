using AutoMapper;
using MoneySource.Core.Application.Features.TransactionFeatures.Commands;
using MoneySource.Core.Application.Features.TransactionFeatures.Queries;
using MoneySource.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneySource.Core.Application.Profiles
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<CreateTransactionCommand.Request, Transaction>();

            CreateMap<Transaction, GetAllTransactionsQuery.TransactionDto>()
                .ForMember(dest => dest.SourceName, src => src.MapFrom(x => x.Source.Name))
                .ForMember(dest => dest.UserName, src => src.MapFrom(x => x.User.UserName));

            CreateMap<Transaction, GetTransactionByIdQuery.Response>();
            CreateMap<Source, GetTransactionByIdQuery.SourceDto>();
            CreateMap<User, GetTransactionByIdQuery.UserDto>();

            CreateMap<UpdateTransactionCommand.Request, Transaction>();

        }
    }
}
