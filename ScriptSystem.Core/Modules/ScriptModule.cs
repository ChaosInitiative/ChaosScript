using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Generic;

using ScriptSystem.Core.Modules.IO;
using ScriptSystem.Core.Hosting.Modules;

namespace ScriptSystem.Core.Modules
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
        /// Whether this module is in the process of reloading
        /// and changes have not yet been applied to the runtime instance
        /// </summary>
        public bool Dirty { get; set; }

        /// <summary>
        /// Modules that depend on this module.
        /// </summary>
        public IList<ScriptModule> Dependents { get; set; }
            = new List<ScriptModule>();

        /// <summary>
        /// Metadata about the module.
        /// </summary>
        public ScriptModuleMetadata Metadata { get; private set; }

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

        /// <summary>
        /// Marks this module and all dependents as dirty.
        /// </summary>
        public void MarkDirty()
        {
            foreach (var module in Dependents)
                module.MarkDirty();

            Dirty = true;
        }
    }
}