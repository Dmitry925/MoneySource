using MoneySource.Core.Domain.Enums;
using MoneySource.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneySource.Core.Application.Authentication
{
    public static class DemoUsers
    {
        public static Dictionary<BaseRole, List<User>> DemoUsersList { get; set; }

        public static string DefaultPassword { get; set; } = @"Pa$$w0rd.";

        static DemoUsers()
        {
            DemoUsersList = new Dictionary<BaseRole, List<User>>
            {
                {
                    (BaseRole)0,
                    new List<User>
                    {
                        new User
                        {
                            Id = Guid.NewGuid(),
                            Email = "demo.admin1@gmail.com",
                            UserName = "Admin1"
                        },
                        new User
                        {
                            Id = Guid.NewGuid(),
                            Email = "demo.admin2@gmail.com",
                            UserName = "Admin2"
                        }
                    }
                },
                { 
                    (BaseRole)1,
                    new List<User>
                    {
                        new User
                        {
                            Id = Guid.NewGuid(),
                            Email = "demo.user1@gmail.com",
                            UserName = "User1"
                        },
                        new User
                        {
                            Id = Guid.NewGuid(),
                            Email = "demo.user2@gmail.com",
                            UserName = "User2"
                        }
                    }
                }
            };
        }
    }
}
