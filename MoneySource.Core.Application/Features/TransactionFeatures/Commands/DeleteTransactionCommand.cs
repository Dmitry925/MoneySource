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

namespace MoneySource.Core.Application.Features.TransactionFeatures.Commands
{
    public class DeleteTransactionCommand
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
                var transaction = await _context.Transactions.AsNoTracking().FirstOrDefaultAsync(a => a.Id == request.Id);

                if(transaction == null)
                {
                    throw new NotFoundException(nameof(transaction));
                }

                var name = transaction.Name;

                _context.Transactions.Remove(transaction);
                await _context.SaveAsync();

                return new Response
                {
                    Result = $"Transaction {name} has been deleted."
                };
            }
        }

        public class Response
        {
            public string Result { get; set; }
        }
    }
}
