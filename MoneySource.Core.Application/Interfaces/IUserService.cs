using MoneySource.Core.Application.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneySource.Core.Application.Interfaces
{
    public interface IUserService
    {
        Task<AuthorizationModel> GetTokenAsync(AuthenticationModel model);
    }
}
