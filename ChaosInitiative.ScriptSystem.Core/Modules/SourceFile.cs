using System;

namespace ChaosInitiative.ScriptSystem.Core.Modules
{
    /// <summary>
    /// Represents a single file of source code.
    /// </summary>
    internal class SourceFile
    {
        /// <summary>
        /// The path of the source file, relative to the IO provider.
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// The text contents of the source file.
        /// </summary>
        public string Text
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// The internal IO backend.
        /// </summary>
        private IScriptModuleRepository _moduleIO;

        public SourceFile(string path, IScriptModuleRepository moduleIO)
        {
            Path = path;
            _moduleIO = moduleIO;
        }
    }
}