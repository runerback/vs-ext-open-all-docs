using System;

namespace OpenAllDocs
{
    internal static class Constants
    {
        internal static class ProjectItem
        {
            public static class Properties
            {
                public const string FullPath = "FullPath";
            }

            public static class Commands
            {
                public static class File
                {
                    public const string OpenFile = "File.OpenFile";
                }
            }
        }

        internal static class Symbols
        {
            public static class Commands
            {
                public static class OpenAllDocsUnderProject
                {
                    public const int CommandId = 0x500A;

                    public static readonly Guid CommandSet = new Guid("cbf06481-f299-4271-886f-2d4e4950f2f7");
                }

                public static class OpenAllDocsUnderFolder
                {
                    public const int CommandId = 0x500B;

                    public static readonly Guid CommandSet = new Guid("8b96e8dc-095e-4cb8-8df4-cebd3039f9fc");
                }
            }
        }
    }
}