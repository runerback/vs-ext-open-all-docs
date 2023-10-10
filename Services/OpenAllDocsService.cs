using EnvDTE;
using EnvDTE80;
using System.Collections.Generic;
using System.Linq;

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

            var documents = dte.SelectedItems
                .OfType<SelectedItem>()
                .SelectMany(UnderlyingDocumentIterator)
                .GroupBy(it => it.FullPath)
                .Select(it => it.First())
                .OrderBy(it => it.FullPath);

            foreach (var document in documents)
            {
                document.Open();
            }
        }

        protected abstract IEnumerable<IDocumentItem> UnderlyingDocumentIterator(SelectedItem source);

        protected static IReadOnlyCollection<string> PropertyKeys(ProjectItem source)
        {
            return source.Properties.OfType<Property>().Select(it => it.Name).ToArray();
        }
    }
}