using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MoneySource.Core.Application.Interfaces;
using MoneySource.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MoneySource.Core.Application.Features.SourceFeatures.Queries
{
    public class GetAllSourcesQuery
    {
        public class Request : IRequest<Response>
        {

        }
        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var sourceList = _mapper.Map<List<SourceDto>>(await _context.Sources.AsNoTracking().ToListAsync());

                if (sourceList == null)
                {
                    return null;
                }

                return new Response
                {
                    Sources = sourceList
                };
            }
        }

        public class Response
        {
            public List<SourceDto> Sources { get; set; }
        }

        public class SourceDto
        {
            public Guid Id { get; set; }

            public string Name { get; set; }

            public DateTimeOffset CreationDate { get; set; }
        }
    }
}
