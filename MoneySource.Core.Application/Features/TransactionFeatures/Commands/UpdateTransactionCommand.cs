using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MoneySource.Core.Application.Infrastructure.Exceptions;
using MoneySource.Core.Application.Interfaces;
using MoneySource.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace MoneySource.Core.Application.Features.TransactionFeatures.Commands
{
    public class UpdateTransactionCommand
    {
        public class Request : IRequest<Response>
        {
            [JsonIgnore]
            public Guid Id { get; set; }

            public string Name { get; set; }

            public TransactionType Type { get; set; }

            public double Amount { get; set; }

            public bool IsCompleted { get; set; }

            public DateTimeOffset ComplitionDate { get; set; }

            public Guid SourceId { get; set; }
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
                var transaction = await _context.Transactions.Where(a => a.Id == request.Id).FirstOrDefaultAsync();

                if(transaction == null)
                {
                    throw new NotFoundException(nameof(transaction));
                }

                _mapper.Map(request, transaction);
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
    }
}
