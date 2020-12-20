namespace ChaosInitiative.ScriptSystem.Core.Exceptions
{
    public class ScriptFileSystemInaccessibleException : ScriptFileSystemException
    {
        public ScriptFileSystemInaccessibleException(string path) : base(path)
        {
        }
    }
}