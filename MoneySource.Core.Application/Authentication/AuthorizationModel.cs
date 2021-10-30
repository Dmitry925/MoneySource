using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneySource.Core.Application.Authentication
{
    public class AuthorizationModel
    {
        public Guid UserId { get; set; }
        public string Message { get; set; }
        public bool isAuthenticated { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public List<string> Roles { get; set; }
    }
}
