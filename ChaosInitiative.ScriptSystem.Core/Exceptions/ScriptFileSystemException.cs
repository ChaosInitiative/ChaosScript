namespace ChaosInitiative.ScriptSystem.Core.Exceptions
{
    public class ScriptFileSystemException : ScriptException
    {
        
        public string Path { get; set; }
            
        public ScriptFileSystemException(string path)
        {
            Path = path;
        }

    }
}