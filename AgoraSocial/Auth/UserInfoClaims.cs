using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AgoraSocial.Auth
{
    public class UserInfoClaims : IClaimsTransformation
    {
        private HttpClient _client;
        private IConfiguration _configuration;
        private IHttpContextAccessor _ctx;

        public UserInfoClaims(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _ctx = httpContextAccessor;
            _client = new HttpClient();
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            await Task.CompletedTask;

            var identityUrl = _configuration.GetValue<string>("IdentityUrl");
            var test = principal.Claims.FirstOrDefault(c => c.Type == "access_token")?.Value;

            var userinfo = await _client.GetUserInfoAsync(new UserInfoRequest
            {
                Address = $"{identityUrl}/connect/userinfo",
                Token = principal.Claims.FirstOrDefault(c => c.Type == "access_token")?.Value
            });

            var transformed = new ClaimsPrincipal();
            transformed.AddIdentities(principal.Identities);
            transformed.AddIdentity(new ClaimsIdentity(userinfo.Claims.Select(c => new Claim(c.Type, c.Value))));
            return transformed;
        }


    }
}
