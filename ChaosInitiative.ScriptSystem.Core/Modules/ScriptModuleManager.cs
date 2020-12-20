using System.Threading.Tasks;
using ChaosInitiative.ScriptSystem.Core.Compilation;
using ChaosInitiative.ScriptSystem.Core.Hosting;
using ChaosInitiative.ScriptSystem.Core.Utilities;
using Serilog;

namespace ChaosInitiative.ScriptSystem.Core.Modules
{
    // high-level API to manage scripts in our framework
    public class ScriptModuleManager
    {
        private readonly DependencyGraph<ScriptModule> _modules
            = new DependencyGraph<ScriptModule>();
        private readonly ScriptCompiler _compiler;
        private readonly HotloadManager _hotloader;

        public ScriptModuleManager()
        {
            _compiler = new ScriptCompiler();
            _hotloader = new HotloadManager();
        }

        /// <summary>
        /// Marks a module and all of its dependents dirty
        /// </summary>
        private void MarkModuleDirty(ScriptModule module)
        {
            foreach (var dependency in _modules.GetDependents(module))
                MarkModuleDirty(dependency);
            module.LoadState = ScriptModuleLoadState.Compiling;
        }

        public async Task CompileInternalAsync(ScriptModule module)
        {
            if (module.LoadState != ScriptModuleLoadState.Compiling) return;

            // Compile all of our dependencies
            foreach (var dependency in _modules.GetDependencies(module))
                await CompileInternalAsync(dependency);

            Log.Debug("Compiling {Name}");
            var result = await _compiler.CompileAsync(module.GetAssemblyName(), module.Sources);
            if (!result.Success) {
                Log.Error("Compilation Failure, error message here etc");
                module.LoadState = ScriptModuleLoadState.None;
                return;
            };

            module.IL = result.IL;

            // Compile all of our dependents
            foreach (var dependent in _modules.GetDependents(module))
                await CompileInternalAsync(dependent);

            module.LoadState = ScriptModuleLoadState.Loading;
        }

        public async Task HotloadInternalAsync(ScriptModule module)
        {
            if (module.LoadState != ScriptModuleLoadState.Loading) return;

            // Hotload all of our dependencies
            foreach (var dependency in _modules.GetDependencies(module))
                await HotloadInternalAsync(dependency);

            await _hotloader.HotloadAsync(module, module.IL);

            // Hotload all of our dependents
            foreach (var dependent in _modules.GetDependents(module))
                await HotloadInternalAsync(dependent);

            module.LoadState = ScriptModuleLoadState.None;
        }

        /// <summary>
        /// Forces a reload of a single module asynchronously.
        /// </summary>
        public async Task ReloadAsync(ScriptModule module)
        {
            MarkModuleDirty(module);
            await CompileInternalAsync(module);
            await HotloadInternalAsync(module);
        }

        /// <summary>
        /// Forces a reload of all modules asynchronously.
        /// </summary>
        public async Task ReloadAllAsync()
        {
            foreach (var module in _modules)
                await ReloadAsync(module);
        }

        /// <summary>
        /// Unloads a module and removes it from the runtime.
        /// </summary>
        public async Task UnloadAsync(ScriptModule module)
        {
            module.RuntimeState.Unload();
            _modules.Remove(module);

            await Task.CompletedTask;
        }
    }
}
