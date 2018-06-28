using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDemo.Authorization
{
    /// <summary>
    /// 用户授权特性
    /// </summary>
    public class UserAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// 策略前缀
        /// </summary>
        public const string POLICY_PREFIX = "UserRole";

        /// <summary>
        /// 要求的用户类型
        /// </summary>
        private UserRoleEnum m_thisUserRol = UserRoleEnum.None;

        /// <summary>
        /// 指定用户权限
        /// </summary>
        public UserRoleEnum UserRole
        {
            get
            {
                return m_thisUserRol;
            }
            set
            {
                m_thisUserRol = value;

                //设置权限字符串
                Policy = $"{POLICY_PREFIX}{value.ToString()}";
            }
        }

   
    }
}
