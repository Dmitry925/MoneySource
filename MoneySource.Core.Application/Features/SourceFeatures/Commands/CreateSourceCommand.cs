using AutoMapper;
using FluentValidation;
using MediatR;
using MoneySource.Core.Application.Infrastructure;
using MoneySource.Core.Application.Interfaces;
using MoneySource.Core.Domain.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MoneySource.Core.Application.Features.SourceFeatures.Commands
{
    public class CreateSourceCommand
    {
        public class Request : IRequest<Response>
        {
            public string Name { get; set; }
        }

        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(a => a.Name)
                    .MinimumLength(Constants.MIN_NAME_LENGTH)
                    .WithMessage($"Name length must be more than {Constants.MIN_NAME_LENGTH} characters");

                RuleFor(a => a.Name)
                    .MaximumLength(Constants.MAX_NAME_LENGTH)
                    .WithMessage($"Name length must be less than {Constants.MAX_NAME_LENGTH} characters");
            }
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
                var source = _mapper.Map<Source>(request);
                source.Id = Guid.NewGuid();
                source.CreationDate = DateTimeOffset.UtcNow;

                await _context.Sources.AddAsync(source);
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
