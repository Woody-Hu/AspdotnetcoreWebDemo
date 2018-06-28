using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDemo.Authorization
{
    /// <summary>
    /// 授权要求
    /// </summary>
    public class UserAuthorizeRequirement : IAuthorizationRequirement
    {
        public UserRoleEnum UseUserRol { get; private set; }


        public UserAuthorizeRequirement(UserRoleEnum inputUserRole)
        {
            UseUserRol = inputUserRole;
        }
    }
}
