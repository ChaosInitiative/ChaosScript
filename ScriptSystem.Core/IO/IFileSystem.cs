using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ScriptSystem.Core.IO
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
        /// Enumerates the directories at the specified path.
        /// </summary>
        IEnumerable<string> EnumerateDirectories(string path);

        /// <summary>
        /// Enumerates the files at the specified path.
        /// </summary>
        IEnumerable<string> EnumerateFiles(string path);
    }
}