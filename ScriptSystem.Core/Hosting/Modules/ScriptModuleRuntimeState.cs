using System;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;

using ScriptSystem.Core.Modules;
using ScriptSystem.Core.Utilities;

namespace ScriptSystem.Core.Hosting.Modules
{
    /// <summary>
    /// Represents the runtime state of a loaded script module.
    /// </summary>
    internal class ScriptModuleRuntimeState
    {
        public bool Loaded { get; private set; }
        public Assembly Assembly { get; private set; }
        public AssemblyLoadContext Context { get; private set; }

        public bool Load(byte[] IL)
        {
            if (Loaded) return true;

            Context = new ScriptModuleAssemblyLoadContext(null);
            using (var stream = new MemoryStream())
            {
                stream.Write(IL);
                stream.Seek(0, SeekOrigin.Begin);

                try
                {
                    Assembly = Context.LoadFromStream(stream);
                }
                catch (BadImageFormatException)
                {
                    return false;
                }
            }

            return true;
        }

        public void Unload()
        {
            var weakRef = new WeakReference(Context);
            Context.Unload();
            Context = null;

            for (int i = 0; weakRef.IsAlive && (i < 10); i++)
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

            if (weakRef.IsAlive)
            {
                // failed to GC? then we have serious issues!
                Debugging.Assert(false, "**** LEAKED: Failed to GC loaded assembly after 10 attempts");
            }
        }

        /// <summary>
        /// Retrieves and serialises the state of the runtime
        /// </summary>
        public byte[] Save()
        {
            return null;
        }

        /// <summary>
        /// Deserialises and loads the state of the runtime
        /// </summary>
        public byte[] Restore()
        {
            return null;
        }
    }
}