using System;
using System.Collections.Generic;
using ChaosInitiative.ScriptSystem.Core.Hosting.Modules;

namespace ChaosInitiative.ScriptSystem.Core.Modules
{
    /// <summary>
    /// Analagous to a Mono assembly, represents a distinct module that can be compiled
    /// </summary>
    public class ScriptModule
    {
        /// <summary>
        /// The name of the module.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Unique identifier for this module instance.
        /// </summary>
        public readonly Guid Id;

        /// <summary>
        /// Metadata about the module.
        /// </summary>
        public ScriptModuleMetadata Metadata { get; private set; }

        /// <summary>
        /// The current load state of this module (compiling, loading, etc)
        /// </summary>
        public ScriptModuleLoadState LoadState { get; set; }
            = ScriptModuleLoadState.None;

        /// <summary>
        /// Stored runtime state.
        /// </summary>
        internal ScriptModuleRuntimeState RuntimeState { get; set; }
            = new ScriptModuleRuntimeState();

        /// <summary>
        /// List of source code files, for compilation.
        /// </summary>
        internal IList<SourceFile> Sources { get; private set; }
            = new List<SourceFile>();

        /// <summary>
        /// The compiled code of this module. Not guaranteed to match that of the runtime state's.
        /// </summary>
        internal byte[] IL { get; set; }

        public ScriptModule(string name, ScriptModuleMetadata metadata)
        {
            Name = name;
            Id = Guid.NewGuid();
            Metadata = metadata;
        }

        public string GetAssemblyName()
        {
            var str = Id.ToString().Replace("-", String.Empty).ToUpper();
            return $"ScriptSystem.DynamicAssemblies.{str}";
        }
    }
}