using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MoneySource.Core.Application.Infrastructure.Exceptions;
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
    public class GetSourceByIdQuery
    {
        public class Request : IRequest<Response>
        {
            public Guid Id { get; set; }
        }

        public class GetSourceByIdQueryHandler : IRequestHandler<Request, Response>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetSourceByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var source = await _context.Sources.AsNoTracking().FirstOrDefaultAsync(a => a.Id == request.Id);

                if (source == null)
                {
                    throw new NotFoundException(nameof(source));
                }

                var sourceMapped = _mapper.Map<Response>(source);
                return sourceMapped;
            }
        }

        public class Response
        {
            public Guid Id { get; set; }

            public string Name { get; set; }

            public DateTimeOffset CreationDate { get; set; }
        }
    }
}
