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
                RuleFor(v => v.Name)
                    .Length(Constants.MIN_NAME_LENGTH, Constants.MAX_NAME_LENGTH)
                    .WithMessage($"Name length must be between {Constants.MIN_NAME_LENGTH} and {Constants.MAX_NAME_LENGTH}");
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
