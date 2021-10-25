using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MoneySource.Core.Application.Infrastructure.Exceptions;
using MoneySource.Core.Application.Interfaces;
using MoneySource.Core.Domain.Enums;
using MoneySource.Core.Domain.Models;
using System;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace MoneySource.Core.Application.Features.TransactionFeatures.Commands
{
    public class CreateTransactionCommand
    {
        public class Request : IRequest<Response>
        {
            public string Name { get; set; }

            public TransactionType Type { get; set; }

            public double Amount { get; set; }

            public bool IsCompleted { get; set; }

            public Guid SourceId { get; set; }

            //[JsonIgnore]
            //public Source Source { get; set; }
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
                if (!await _context.Sources.AnyAsync(s => s.Id == request.SourceId))
                {
                    throw new NotFoundException(nameof(Source));
                }

                var transaction = _mapper.Map<Transaction>(request);

                //transaction.Source = await _context.Sources.AsNoTracking().FirstOrDefaultAsync(a => a.Id == request.SourceId);

                transaction.Id = Guid.NewGuid();
                transaction.CreationDate = DateTimeOffset.UtcNow;

                await _context.Transactions.AddAsync(transaction);
                await _context.SaveAsync();
                
                return new Response
                {
                    Id = transaction.Id
                };
            }
        }

        public class Response
        {
            public Guid Id { get; set; }
        }

        public class SourceDto
        {
            public string Name { get; set; }
            public DateTimeOffset CreationDate { get; set; }
        }
    }
}
