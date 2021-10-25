using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MoneySource.Core.Application.Interfaces;
using MoneySource.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MoneySource.Core.Application.Features.TransactionFeatures.Queries
{
    public class GetAllTransactionsQuery
    {
        public class Request : IRequest<Response>
        {
            public string Something { get; set; }
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
                var transactions = await _context.Transactions.AsNoTracking().ProjectTo<TransactionDto>(_mapper.ConfigurationProvider).ToListAsync();
                return new Response
                {
                    Transactions = transactions
                };
            }
        }

        public class Response
        {
            public List<TransactionDto> Transactions { get; set; }
        }

        public class TransactionDto
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public TransactionType Type { get; set; }
            public double Amount { get; set; }
            public bool IsCompleted { get; set; }
            public DateTimeOffset ComplitionDate { get; set; }
            public string SourceName { get; set; }
            public DateTimeOffset CreationDate { get; set; }
        }
    }
}
