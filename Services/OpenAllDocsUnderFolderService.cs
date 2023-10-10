using EnvDTE;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using KnwonProperties = OpenAllDocs.Constants.ProjectItem.Properties;

namespace OpenAllDocs.Services
{
    internal sealed class OpenAllDocsUnderFolderService : OpenAllDocsService
    {
        protected override IEnumerable<IDocumentItem> UnderlyingDocumentIterator(SelectedItem source)
        {
            foreach (var projectItem in ProjectItemIterator(source.ProjectItem))
            {
                var propertyKeys = PropertyKeys(projectItem);
                if (!propertyKeys.Contains(KnwonProperties.FullPath))
                {
                    continue;
                }

                var fullpath = projectItem.Properties.Item(KnwonProperties.FullPath).Value.ToString();
                if (!File.Exists(fullpath))
                {
                    continue;
                }

                yield return new DocumentItemUnderFolder(projectItem, fullpath);
            }
        }

        private static IEnumerable<ProjectItem> ProjectItemIterator(ProjectItem source)
        {
            if (source == null || source.FileCount == 0)
            {
                yield break;
            }

            yield return source;

            foreach (var innerItem in source.ProjectItems.OfType<ProjectItem>())
            {
                foreach (var next in ProjectItemIterator(innerItem))
                {
                    yield return next;
                }
            }
        }

        private sealed class DocumentItemUnderFolder : IDocumentItem
        {
            private readonly ProjectItem _source;

            public DocumentItemUnderFolder(ProjectItem source, string fullpath)
            {
                _source = source;
                FullPath = fullpath;
            }

            public string FullPath { get; }

            public void Open()
            {
                if (!_source.IsOpen)
                {
                    if (_source.Document == null)
                    {
                        _source.Open();
                    }

                    _source.Document.Activate();
                }
            }
        }
    }
}