using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MoneySource.Core.Application.Infrastructure;
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
            public Guid UserId { get; set; }
        }

        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(v => v.Name)
                    .Length(Constants.MIN_NAME_LENGTH, Constants.MAX_NAME_LENGTH)
                    .WithMessage($"Name length must be between {Constants.MIN_NAME_LENGTH} and {Constants.MAX_NAME_LENGTH}");

                RuleFor(v => v.Type)
                    .IsInEnum()
                    .WithMessage("Invalid type value");

                RuleFor(v => v.Amount)
                    .Must(x => x >= Constants.MIN_AMOUNT && x <= Constants.MAX_AMOUNT)
                    .WithMessage($"Amount must be between {Constants.MIN_AMOUNT} and {Constants.MAX_AMOUNT}");
            }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly UserManager<User> _userManager;

            public Handler(IApplicationDbContext context, IMapper mapper, UserManager<User> userManager)
            {
                _context = context;
                _mapper = mapper;
                _userManager = userManager;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                if (!await _context.Sources.AnyAsync(s => s.Id == request.SourceId))
                {
                    throw new NotFoundException(nameof(Source));
                }
                if (!await _userManager.Users.AnyAsync(u => u.Id == request.UserId))
                {
                    throw new NotFoundException(nameof(User));
                }

                var transaction = _mapper.Map<Transaction>(request);

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
