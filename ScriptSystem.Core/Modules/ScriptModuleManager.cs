using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Serilog;

using ScriptSystem.Core.Compilation;
using ScriptSystem.Core.Hosting;

namespace ScriptSystem.Core.Modules
{
    // high-level API to manage scripts in our framework
    public class ModuleManager
    {
        private List<IScriptModuleRepository> _repos;
        private List<ScriptModule> _modules;
        private ScriptCompiler _compiler;
        private HotloadManager _hotloader;

        public ModuleManager()
        {
            // testing, get this from source later
            var path = "D:\\src\\chaos\\p2ce\\game\\content\\modules";
            _repos.Add(new ScriptModuleRepositoryNative(path));

            _compiler = new ScriptCompiler();
            _hotloader = new HotloadManager();
        }

        /// <summary>
        /// Reloads a single module and all of its dependents asynchronously.
        /// </summary>
        public async Task ReloadAsync(ScriptModule module)
        {
            // Dependencies we've already reloaded
            var reloaded = new List<ScriptModule>();

            // Compile the module code
            Log.Debug("Compiling {Name}");
            var result = await _compiler.CompileAsync(module.GetAssemblyName(), module.Sources);
            if (!result.Success) return;

            await _hotloader.HotloadAsync(module, result.IL);
        }

        /// <summary>
        /// Reloads all modules asynchronously.
        /// </summary>
        public async Task ReloadAllAsync()
        {
            foreach (var module in _modules)
            {
                await ReloadAsync(module);
            }
        }
    }
}
