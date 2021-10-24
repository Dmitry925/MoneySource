using AutoMapper;
using MoneySource.Core.Application.Features.SourceFeatures.Commands;
using MoneySource.Core.Application.Features.SourceFeatures.Queries;
using MoneySource.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneySource.Core.Application.Profiles
{
    public class SourceProfile : Profile
    {
        public SourceProfile()
        {
            CreateMap<Source, GetAllSourcesQuery.SourceDTO>();
            CreateMap<Source, GetSourceByIdQuery.Response>();
            CreateMap<PostSourceCommand.Request, Source>();
            CreateMap<PutSourceCommand.Request, Source>();
        }
    }
}
