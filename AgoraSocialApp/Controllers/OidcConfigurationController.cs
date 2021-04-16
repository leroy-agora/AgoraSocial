using IdentityServer4.Extensions;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgoraSocialApp.Controllers
{
    public class OidcConfigurationController : Controller
    {
        private readonly ILogger<OidcConfigurationController> _logger;
        private readonly IClientStore _clientStore;

        private readonly IResourceStore _resourceStore;

        public OidcConfigurationController(ILogger<OidcConfigurationController> logger, IClientStore clientStore, IResourceStore resourceStore)
        {
            _logger = logger;
            _clientStore = clientStore;
            _resourceStore = resourceStore;

        }

        [HttpGet("_configuration/{clientId}")]
        public async Task<IActionResult> GetClientRequestParameters([FromRoute] string clientId)
        {
            var context = HttpContext;
            var authority = context.GetIdentityServerIssuerUri();
            var client = await _clientStore.FindClientByIdAsync(clientId);

            var resources = await _resourceStore.GetAllResourcesAsync();

            return Ok(new Dictionary<string, string>
            {
                ["authority"] = authority,
                ["client_id"] = client.ClientId,
                ["redirect_uri"] = client.RedirectUris.FirstOrDefault(),
                ["post_logout_redirect_uri"] = client.PostLogoutRedirectUris.FirstOrDefault(),
                ["response_type"] = "code",
                ["scope"] = string.Join(" ", client.AllowedScopes)
            });
        }
    }
}
