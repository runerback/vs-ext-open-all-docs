using Microsoft.VisualStudio.Shell;
using OpenAllDocs.Commands;
using System;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using System.Threading;
using Task = System.Threading.Tasks.Task;

namespace OpenAllDocs
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [Guid("289742e9-5792-4856-a39c-44205ce50ba3")]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    public sealed class OpenAllDocsPackage : AsyncPackage
    {
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
            if (await GetServiceAsync(typeof(IMenuCommandService)) is OleMenuCommandService commandService)
            {
                new CommandInitializer().Initialize(this, commandService);
            }
        }
    }
}