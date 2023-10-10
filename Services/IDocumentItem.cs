namespace OpenAllDocs.Services
{
    internal interface IDocumentItem
    {
        string FullPath { get; }

        void Open();
    }
}