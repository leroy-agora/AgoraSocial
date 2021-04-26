using AgoraSocial.Models.API;
using AgoraSocial.Models.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CSharpVitamins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Raven.Client.Documents.Session;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AgoraSocial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TokensController : ControllerBase
    {
        private static IAsyncDocumentSession _ravenSession;

        public TokensController(IAsyncDocumentSession ravenSession)
        {
            _ravenSession = ravenSession;
        }

        // GET: api/<TokensController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        public IActionResult Get()
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }

        // GET api/<TokensController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Token>> Get(string id)
        {
            var token = await _ravenSession.LoadAsync<Token>($"Tokens/{id}");

            if (token == null) {
                return NotFound();
            }

            return Ok(token);
        }

        // POST api/<TokensController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Token>> Post([FromBody] TokenCreateModel tokenInfo)
        {
            // TODO: Verify ClientId etc

            var token = new Token
            {
                Id = $"Tokens/{ShortGuid.NewGuid()}",
                ClientId = tokenInfo.ClientId,
                ContentId = tokenInfo.ContentId,
                ContentUrl = tokenInfo.ContentUrl,
                UserId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value
            };

            await _ravenSession.StoreAsync(token);
            await _ravenSession.SaveChangesAsync();

            var id = _ravenSession.Advanced.GetDocumentId(token)[7..];

            return CreatedAtAction(nameof(Get), new { Id = id }, new TokenModel
            {
                Id = id,
                ContentId = token.ContentId,
                ContentUrl = token.ContentUrl
            });
        }

        // POST api/<TokensController>
        [HttpPost("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Token>> Post(string id)
        {
            var token = await _ravenSession.LoadAsync<Token>($"Tokens/{id}");

            if (token == null)
            {
                return NotFound();
            }

            var userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            if (token.Redeemed && token.RedeemedBy!= userId)
            {
                return BadRequest("Token already used");
            }
            
            if (!token.Redeemed)
            {
                token.Redeemed = true;
                token.RedeemedBy = userId;
            }

            var counters = _ravenSession.CountersFor(token);
            counters.Increment("Used", 1);

            await _ravenSession.SaveChangesAsync();

            return NoContent();
        }

        // PUT api/<TokensController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TokensController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
