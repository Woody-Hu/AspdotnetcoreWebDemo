using AutofacMiddleware;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebDemo.Authorization
{
    /// <summary>
    /// 使用的授权处理器
    /// </summary>
    public class UserAuthorizeHanlder : AuthorizationHandler<UserAuthorizeRequirement>
    {
        private readonly ILogger<UserAuthorizeHanlder> m_useLogger;


        public UserAuthorizeHanlder(ILogger<UserAuthorizeHanlder> inputLogger)
        {
            m_useLogger = inputLogger;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserAuthorizeRequirement requirement)
        {

            //若无要求
            if (requirement.UseUserRol == UserRoleEnum.None)
            {
                context.Succeed(requirement);
            }
            else
            {
                //获取标示
                var useRoleClaim = context.User.FindFirst(k => k.Type == ClaimTypes.Role);

                //判断标示是否存在
                if (null != useRoleClaim && Enum.TryParse(useRoleClaim.Value,out UserRoleEnum useRole))
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
