using MediatR;
using Microsoft.EntityFrameworkCore;
using MoneySource.Core.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MoneySource.Core.Application.Features.SourceFeatures.Commands
{
    public class DeleteSourceByIdCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }

        public class DeleteSourceByIdCommandHandler : IRequestHandler<DeleteSourceByIdCommand, Guid>
        {
            private readonly IApplicationDbContext _context;
            public DeleteSourceByIdCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Guid> Handle(DeleteSourceByIdCommand command, CancellationToken cancellationToken)
            {
                var source = await _context.Sources.Where(a => a.Id == command.Id).FirstOrDefaultAsync();
                if (source == null) return default;
                _context.Sources.Remove(source);
                await _context.SaveAsync();
                return source.Id;
            }
        }
    }
}
