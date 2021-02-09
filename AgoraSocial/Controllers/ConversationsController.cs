using AgoraSocial.Models.API;
using AgoraSocial.Models.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AgoraSocial.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ConversationsController : ControllerBase
    {

        private static IAsyncDocumentSession _ravenSession;

        public ConversationsController(IAsyncDocumentSession ravenSession)
        {
            _ravenSession = ravenSession;
        }

        // GET: api/<ConversationsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ConversationsController>/5
        [HttpGet("{id}")]
        public async Task<ConversationModel> Get(string id)
        {
            var conversation = await _ravenSession.LoadAsync<Conversation>($"Conversation/{id}");

            return new ConversationModel
            {
                Id = Guid.Parse(conversation.Id[(conversation.Id.IndexOf("Conversation/") + "Conversation/".Length)..]),
                Title = conversation.Title
            };
        }

        // POST api/<ConversationsController>
        [HttpPost]
        public async Task <ConversationModel> Post([FromBody] ConversationModel conversationData)
        {
            var claims = (User.Identity as ClaimsIdentity).Claims;

            var conversation = new Conversation
            {
                Id = $"Conversation/{Guid.NewGuid()}",
                Title = conversationData.Title,
                CreatedBy = (User.Identity as ClaimsIdentity).Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value
            };

            await _ravenSession.StoreAsync(conversation);

            var id = _ravenSession.Advanced.GetDocumentId(conversation);

            await _ravenSession.SaveChangesAsync();

            return new ConversationModel
            {
                Id = Guid.Parse(id[(id.IndexOf("Conversation/") + "Conversation/".Length)..]),
                Title = conversation.Title
            };
        }

        // PUT api/<ConversationsController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] string value)
        {
            await Task.Delay(100);
            var user = User;
            var ident = User.Identity;

            return;
        }

        // DELETE api/<ConversationsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
