using System;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;

using ScriptSystem.Core.Modules;
using ScriptSystem.Core.Utilities;

namespace ScriptSystem.Core.Hosting
{
    internal class HotloadManager
    {
        /// <summary>
        /// Loads new bytecode into a script module.
        /// </summary>
        public async Task<bool> HotloadAsync(ScriptModule module, byte[] IL)
        {
            // TODO: Serialise the current state of the loaded assembly
            // TODO: Restore the old state into the new assembly
            var state = module.RuntimeState;
            state.Unload();

            var result = state.Load(IL);
            return result;
        }
    }
}