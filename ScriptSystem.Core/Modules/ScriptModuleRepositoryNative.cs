using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Serilog;

namespace ScriptSystem.Core.Modules
{
    /// <summary>
    /// Loads modules from a filesystem path using C# IO APIs.
    /// </summary>
    internal class ScriptModuleRepositoryNative : IScriptModuleRepository
    {
        private DirectoryInfo _path;
        public ScriptModuleRepositoryNative(string path)
        {
            _path = new DirectoryInfo(path);
            if (!_path.Exists) throw new Exception($"The module directory at \"{path}\" does not exist!");
        }

        public async Task<IEnumerable<ScriptModule>> EnumerateModulesAsync()
        {
            var modules = new List<ScriptModule>();
            foreach (var dir in _path.GetDirectories())
            {
                var infoPath = Path.Join(dir.FullName, "module.json");
                if (!File.Exists(infoPath)) {
                    Log.Warning("Module at \"{Path}\" did not contain a module.json, skipping.", dir.FullName);
                    continue;
                }

                var text = await File.ReadAllTextAsync(infoPath);
                var meta = JsonSerializer.Deserialize<ScriptModuleMetadata>(text);

                modules.Add(new ScriptModule(dir.Name, meta));
            }

            return modules;
        }
    }
}