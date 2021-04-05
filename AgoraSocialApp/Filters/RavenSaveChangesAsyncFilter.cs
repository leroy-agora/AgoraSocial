using Microsoft.AspNetCore.Mvc.Filters;
using Raven.Client.Documents.Session;
using System.Threading.Tasks;

namespace AgoraSocialApp.Filters
{
    /// <summary>
    /// Razor Pages filter that saves any changes after the action completes.
    /// </summary>
    public class RavenSaveChangesAsyncFilter : IAsyncPageFilter
    {
        private readonly IAsyncDocumentSession dbSession;

        public RavenSaveChangesAsyncFilter(IAsyncDocumentSession dbSession)
        {
            this.dbSession = dbSession;
        }

        public async Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
        {
            await Task.CompletedTask;
        }

        public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
        {
            var result = await next.Invoke();

            // If there was no exception, and the action wasn't cancelled, save changes.
            if (result.Exception == null && !result.Canceled)
            {
                await dbSession.SaveChangesAsync();
            }
        }
    }
}