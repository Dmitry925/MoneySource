using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MoneySource.Core.Application.Infrastructure.Exceptions;
using MoneySource.Core.Application.Interfaces;
using MoneySource.Core.Domain.Enums;
using MoneySource.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MoneySource.Core.Application.Features.TransactionFeatures.Queries
{
    public class GetTransactionByIdQuery
    {
        public class Request : IRequest<Response>
        {
            public Guid Id { get; set; }
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
                var transaction = await _context.Transactions.AsNoTracking().ProjectTo<Response>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(a => a.Id == request.Id);

                if(transaction == null)
                {
                    throw new NotFoundException(nameof(transaction));
                }

                return transaction;
            }
        }

        public class Response
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public TransactionType Type { get; set; }
            public double Amount { get; set; }
            public bool IsCompleted { get; set; }
            public DateTimeOffset ComplitionDate { get; set; }
            public SourceDto Source { get; set; }
            public UserDto User { get; set; }
            public DateTimeOffset CreationDate { get; set; }
        }

        public class SourceDto
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }

        public class UserDto
        {
            public Guid Id { get; set; }
            public string UserName { get; set; }
        }
    }
}
