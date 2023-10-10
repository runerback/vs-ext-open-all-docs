using EnvDTE;
using EnvDTE80;
using System.Collections.Generic;
using System.Linq;
using KnwonCommands = OpenAllDocs.Constants.ProjectItem.Commands;

namespace OpenAllDocs.Services
{
    internal abstract class OpenAllDocsService
    {
        public void OpenAll(DTE2 dte)
        {
            if (!(dte?.SelectedItems?.Count > 0))
            {
                return;
            }

            var openFileCommand = dte.Commands.OfType<Command>()
                .FirstOrDefault(it => it.Name == KnwonCommands.File.OpenFile);
            if (openFileCommand == null || !openFileCommand.IsAvailable)
            {
                return;
            }

            var documents = dte.SelectedItems
                .OfType<SelectedItem>()
                .SelectMany(UnderlyingDocumentIterator)
                .Distinct()
                .OrderBy(it => it);

            foreach (var document in documents)
            {
                dte.ExecuteCommand(KnwonCommands.File.OpenFile, document);
            }
        }

        protected abstract IEnumerable<string> UnderlyingDocumentIterator(SelectedItem source);

        protected static IReadOnlyCollection<string> PropertyKeys(ProjectItem source)
        {
            return source.Properties.OfType<Property>().Select(it => it.Name).ToArray();
        }
    }
}