using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChaosInitiative.ScriptSystem.Core.Modules
{
    public interface IScriptModuleRepository
    {
        Task<IEnumerable<ScriptModule>> EnumerateModulesAsync();
    }
}