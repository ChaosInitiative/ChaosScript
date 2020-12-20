namespace ChaosInitiative.ScriptSystem.Core.Exceptions
{
    public class ScriptFileSystemNotFoundException : ScriptFileSystemException
    {
        public ScriptFileSystemNotFoundException(string path) : base(path)
        {
        }
    }
}