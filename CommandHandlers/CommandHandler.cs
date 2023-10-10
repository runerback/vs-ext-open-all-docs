using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Threading;
using OpenAllDocs.Services;
using System;
using System.ComponentModel.Design;
using System.Threading.Tasks;
using static OpenAllDocs.Constants.Symbols.Commands;

namespace OpenAllDocs.CommandHandlers
{
    internal sealed class CommandHandler
    {
        private readonly IAsyncServiceProvider _serviceProvider;

        public CommandHandler(IAsyncServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public EventHandler Register(CommandID id, JoinableTaskFactory taskFactory)
        {
            return (sender, e) =>
            {
                _ = taskFactory.RunAsync(async () =>
                {
                    await taskFactory.SwitchToMainThreadAsync();
                    try
                    {
                        await ExecuteAsync(id);
                    }
                    catch (Exception)
                    {
                        // TODO: add log
                    }
                }).JoinAsync().ConfigureAwait(false);
            };
        }

        private async Task ExecuteAsync(CommandID command)
        {
            switch (command.ID)
            {
                case OpenAllDocsUnderProject.CommandId:
                    await new OpenAllDocsUnderProjectCommandHandler(new OpenAllDocsUnderProjectService())
                        .HandleAsync(_serviceProvider);
                    break;

                case OpenAllDocsUnderFolder.CommandId:
                    await new OpenAllDocsUnderFolderCommandHandler(new OpenAllDocsUnderFolderService())
                        .HandleAsync(_serviceProvider);
                    break;

                default: break;
            }
        }
    }
}