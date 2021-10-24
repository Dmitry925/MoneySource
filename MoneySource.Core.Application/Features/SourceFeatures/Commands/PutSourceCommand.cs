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
    public class PutSourceCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public class PutSourceCommandHandler : IRequestHandler<PutSourceCommand, Guid>
        {
            private readonly IApplicationDbContext _context;

            public PutSourceCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Guid> Handle(PutSourceCommand command, CancellationToken cancellationToken)
            {
                var source = await _context.Sources.Where(a => a.Id == command.Id).FirstOrDefaultAsync();

                if (source == null) return default;
                else
                {
                    source.Name = command.Name;
                    source.CreationDate = DateTimeOffset.Now;
                    await _context.SaveAsync();
                    return source.Id;
                }
            }
        }
    }
}
