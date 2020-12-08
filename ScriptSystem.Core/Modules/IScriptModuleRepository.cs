using System.Threading.Tasks;
using System.Collections.Generic;

namespace ScriptSystem.Core.Modules
{
    public interface IScriptModuleRepository
    {
        Task<IEnumerable<ScriptModule>> EnumerateModulesAsync();
    }
}