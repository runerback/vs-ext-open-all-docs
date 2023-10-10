using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using OpenAllDocs.Services;
using System.Threading.Tasks;

namespace OpenAllDocs.CommandHandlers
{
    internal sealed class OpenAllDocsUnderFolderCommandHandler
    {
        private readonly OpenAllDocsService _service;

        public OpenAllDocsUnderFolderCommandHandler(OpenAllDocsService service)
        {
            _service = service;
        }

        public async Task HandleAsync(IAsyncServiceProvider serviceProvider)
        {
            _service.OpenAll(await serviceProvider.GetServiceAsync(typeof(SDTE)) as DTE2);
        }
    }
}