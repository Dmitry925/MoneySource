using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MoneySource.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MoneySource.Core.Application.Features.UserFeatures.Queries
{
    public class GetAllUsersQuery
    {
        public class Request : IRequest<Response>
        {
            
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
                var userList = _mapper.Map<List<UserDto>>(await _userManager.Users.AsNoTracking().ToListAsync());

                return new Response
                {
                    Users = userList
                };
            }
        }

        public class Response
        {
            public List<UserDto> Users { get; set; }
        }

        public class UserDto
        {
            public Guid Id { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
        }
    }
}
