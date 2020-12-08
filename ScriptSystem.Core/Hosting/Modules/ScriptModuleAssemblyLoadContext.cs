using System;
using System.Reflection;
using System.Runtime.Loader;

using ScriptSystem.Core.Modules;

namespace ScriptSystem.Core.Hosting.Modules
{
    internal class ScriptModuleAssemblyLoadContext : AssemblyLoadContext
    {
        private ScriptModule _module;
        public ScriptModuleAssemblyLoadContext(ScriptModule module) : base(isCollectible: true)
        {
            _module = module;
        }

        protected override Assembly Load(AssemblyName assemblyName)
        {
            return null;
        }
    }
}