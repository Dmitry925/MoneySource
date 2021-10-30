using AutoMapper;
using MoneySource.Core.Application.Features.UserFeatures.Commands;
using MoneySource.Core.Application.Features.UserFeatures.Queries;
using MoneySource.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneySource.Core.Application.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, GetAllUsersQuery.UserDto>();

            CreateMap<SignUpUserCommand.Request, User>();
        }
    }
}
