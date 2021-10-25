using MediatR;
using Microsoft.EntityFrameworkCore;
using MoneySource.Core.Application.Infrastructure.Exceptions;
using MoneySource.Core.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MoneySource.Core.Application.Features.SourceFeatures.Commands
{
    public class DeleteSourceCommand
    {
        public class Request : IRequest<Response>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IApplicationDbContext _context;
            public Handler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var source = await _context.Sources.AsNoTracking().FirstOrDefaultAsync(a => a.Id == request.Id);

                if (source == null)
                {
                    throw new NotFoundException(nameof(source));
                }

                var name = source.Name;

                _context.Sources.Remove(source);
                await _context.SaveAsync();

                return new Response
                {
                    Result = $"Source {name} has been deleted."
                };
            }
        }

        public class Response
        {
            public string Result { get; set; }
        }
    }
}
