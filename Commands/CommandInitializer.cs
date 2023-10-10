using Microsoft.VisualStudio.Shell;
using System.ComponentModel.Design;

namespace OpenAllDocs.Commands
{
    internal sealed class CommandInitializer
    {
        public void Initialize(AsyncPackage package, IMenuCommandService commandService)
        {
            OpenAllDocsUnderProjectCommand.Initialize(package, commandService);
            OpenAllDocsUnderFolderCommand.Initialize(package, commandService);
        }
    }
}