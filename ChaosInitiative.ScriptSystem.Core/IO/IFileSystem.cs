using System.Collections.Generic;
using System.IO;

namespace ChaosInitiative.ScriptSystem.Core.IO
{
    /// <summary>
    /// Provides an interface to a system which can manage files.
    /// </summary>
    public interface IFileSystem
    {
        /// <summary>
        /// Opens the specified file with the specified modes as a stream.
        /// </summary>
        /// <returns>A Stream if opening was successful, otherwise null.</returns>
        Stream Open(string path, FileMode mode, FileAccess access);
        
        /// <summary>
        /// Deletes the specified file.
        /// </summary>
        void Delete(string path);

        /// <summary>
        /// Checks whether the specified path exists.
        /// </summary>
        bool Exists(string path);

        /// <summary>
        /// Checks if the path specified is a file or a directory. 
        /// </summary>
        /// <returns>True if the path is a file.</returns>
        bool IsFile(string path);
        
        /// <summary>
        /// Checks if the path specified is a file or a directory. 
        /// </summary>
        /// <returns>True if the path is a file.</returns>
        bool IsDirectory(string path);
        
        /// <summary>
        /// Checks whether the path is accessible on this filesystem.
        /// No exception will be thrown here
        /// </summary>
        /// <returns>True if the file is accessible</returns>
        bool IsAccessible(string path);
        
        /// <summary>
        /// Enumerates the directories at the specified path.
        /// </summary>
        IEnumerable<string> GetDirectories(string path);

        /// <summary>
        /// Enumerates the files at the specified path.
        /// </summary>
        IEnumerable<string> GetFiles(string path);
    }
}