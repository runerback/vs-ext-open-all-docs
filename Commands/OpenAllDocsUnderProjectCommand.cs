﻿using Microsoft.VisualStudio.Shell;
using OpenAllDocs.CommandHandlers;
using System;
using System.ComponentModel.Design;
using static OpenAllDocs.Constants.Symbols.Commands.OpenAllDocsUnderProject;

namespace OpenAllDocs.Commands
{
    internal sealed class OpenAllDocsUnderProjectCommand
    {
        private static readonly CommandID ID = new CommandID(CommandSet, CommandId);

        private readonly AsyncPackage _package;
        private readonly CommandHandler _handler;

        private OpenAllDocsUnderProjectCommand(AsyncPackage package, IMenuCommandService commandService)
        {
            _package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            _handler = new CommandHandler(_package);

            commandService.AddCommand(new MenuCommand(
                _handler.Register(ID, _package.JoinableTaskFactory),
                ID));
        }

        public static OpenAllDocsUnderProjectCommand Instance { get; private set; }

        public static void Initialize(AsyncPackage package, IMenuCommandService commandService)
        {
            Instance = new OpenAllDocsUnderProjectCommand(package, commandService);
        }
    }
}