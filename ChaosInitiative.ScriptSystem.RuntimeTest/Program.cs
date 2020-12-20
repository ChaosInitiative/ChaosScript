using System;
using System.IO;
using System.Threading.Tasks;
using ChaosInitiative.ScriptSystem.Core.IO.Native;

namespace ChaosInitiative.ScriptSystem.RuntimeTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var fileSystem = new NativeFileSystem(".");
            using (var stream = fileSystem.Open("Program.cs", FileMode.Open, FileAccess.Read))
            {
                using (var sr = new StreamReader(stream))
                {
                    Console.Write(sr.ReadToEnd());
                }
            }

            //var manager = new ScriptModuleManager();
            //await manager.ReloadAllAsync();
        }
    }
}
