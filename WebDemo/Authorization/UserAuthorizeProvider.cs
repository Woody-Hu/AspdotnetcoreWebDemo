using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDemo.Authorization
{
    /// <summary>
    /// 授权提供器
    /// </summary>
    public class UserAuthorizeProvider : IAuthorizationPolicyProvider
    {
        /// <summary>
        /// 默认授权提供器
        /// </summary>
        private readonly DefaultAuthorizationPolicyProvider m_useDeaultProvider = null;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="options"></param>
        public UserAuthorizeProvider(IOptions<AuthorizationOptions> options)
        {
            m_useDeaultProvider = new DefaultAuthorizationPolicyProvider(options);
        }

        public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        {
            return m_useDeaultProvider.GetDefaultPolicyAsync();
        }

        public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            if (policyName.StartsWith(UserAuthorizeAttribute.POLICY_PREFIX, StringComparison.OrdinalIgnoreCase) 
                && Enum.TryParse<UserRoleEnum>(policyName.Substring(UserAuthorizeAttribute.POLICY_PREFIX.Length),out UserRoleEnum useRole))
            {
                var policy = new AuthorizationPolicyBuilder();
                policy.AddRequirements(new UserAuthorizeRequirement(useRole));
                return Task.FromResult(policy.Build());
            }

            return m_useDeaultProvider.GetPolicyAsync(policyName);
        }
    }
}
