using AutoMapper;
using MediatR;
using MoneySource.Core.Application.Interfaces;
using MoneySource.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MoneySource.Core.Application.Features.SourceFeatures.Commands
{
    public class PostSourceCommand
    {
        public class Request : IRequest<Response>
        {
            public string Name { get; set; }
        }

        public class PostSourceCommandHandler : IRequestHandler<Request, Response>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public PostSourceCommandHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var source = _mapper.Map<Source>(request);
                source.CreationDate = DateTimeOffset.UtcNow;
                _context.Sources.Add(source);
                await _context.SaveAsync();
                return new Response
                {
                    Id = source.Id
                };
            }
        }

        public class Response
        {
            public Guid Id { get; set; }
        }
    }
}
