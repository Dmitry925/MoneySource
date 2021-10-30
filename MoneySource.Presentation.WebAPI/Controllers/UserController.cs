using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneySource.Core.Application.Authentication;
using MoneySource.Core.Application.Features.UserFeatures.Commands;
using MoneySource.Core.Application.Features.UserFeatures.Queries;
using MoneySource.Core.Application.Interfaces;
using MoneySource.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneySource.Presentation.WebAPI.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet, Authorize(Roles = nameof(BaseRole.Admin))]
        public async Task<IActionResult> GetAllUsers([FromQuery] GetAllUsersQuery.Request request)
        {
            return Ok(await SendAsync(request));
        }

        [HttpPost("token")]
        public async Task<IActionResult> SignIn([FromBody] AuthenticationModel model)
        {
            return Ok(await _userService.GetTokenAsync(model));
        }

        [HttpPost("signUp")]
        public async Task<IActionResult> SignUp([FromBody] SignUpUserCommand.Request request)
        {
            return Ok(await SendAsync(request));
        }
    }
}
