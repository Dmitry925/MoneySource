using AutoMapper;
using FluentValidation;
using FluentValidation.Validators;
using MediatR;
using Microsoft.AspNetCore.Identity;
using MoneySource.Core.Domain.Enums;
using MoneySource.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MoneySource.Core.Application.Features.UserFeatures.Commands
{
    public class SignUpUserCommand
    {
        public class Request : IRequest<Response>
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public string UserName { get; set; }
            public BaseRole Role { get; set; }
        }

        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(v => v.Email)
                    .NotNull()
                    .NotEmpty()
                    .WithMessage("Email mustn't be empty or null!");

                RuleFor(v => v.Email)
                    .EmailAddress(EmailValidationMode.Net4xRegex)
                    .WithMessage("Invalid email format!");

                RuleFor(v => v.Password)
                    .NotNull()
                    .NotEmpty()
                    .WithMessage("Password mustn't be empty or null!");

                RuleFor(v => v.UserName)
                    .NotNull()
                    .NotEmpty()
                    .WithMessage("UserName mustn't be empty!");
            }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly UserManager<User> _userManager;
            private readonly IMapper _mapper;

            public Handler(UserManager<User> userManager, IMapper mapper)
            {
                _userManager = userManager;
                _mapper = mapper;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                if(_userManager.Users.Any(x => x.Email == request.Email))
                {
                    throw new ValidationException($"User with Email {request.Email} already exists!");
                }
                if (_userManager.Users.Any(x => x.UserName == request.UserName))
                {
                    throw new ValidationException($"User with UserName {request.UserName} already exists!");
                }

                var user = _mapper.Map<User>(request);
                user.Id = Guid.NewGuid();

                var createdUser = await _userManager.CreateAsync(user, request.Password);
                if(createdUser.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, request.Role.ToString());
                }
                else
                {
                    throw new ValidationException(string.Join("\n", createdUser.Errors.Select(x => "\nCode: " + x.Code + "\nDescription: " + x.Description)));
                }

                return new Response
                {
                    UserId = user.Id
                };
            }
        }

        public class Response
        {
            public Guid UserId { get; set; }
        }
    }
}
