using System.Collections.Generic;
using System.IO;
using System.Linq;
using ChaosInitiative.ScriptSystem.Core.Exceptions;

namespace ChaosInitiative.ScriptSystem.Core.IO.Native
{
    /// <summary>
    /// File system which accesses files using native C# APIs
    /// </summary>
    public class NativeFileSystem : IFileSystem
    {
        private readonly string _root;
        public NativeFileSystem(string root)
        {
            _root = Path.GetFullPath(root);
        }

        private string GetPathFromRoot(string path)
        {
            var fullPath = Path.GetFullPath(Path.Combine(_root, path));

            // security check to ensure our path is within the root
            if (!fullPath.StartsWith(_root)) 
                throw new ScriptFileSystemInaccessibleException(path);

            return fullPath;
        }

        public Stream Open(string path, FileMode mode, FileAccess access)
        {
            if (!Exists(path))
                throw new ScriptFileSystemNotFoundException(path);
            
            var fullPath = GetPathFromRoot(path); // This will throw if file is inaccessible
            return File.Open(fullPath, mode, access);
        }

        public void Delete(string path)
        {
            if (!Exists(path))
                throw new ScriptFileSystemNotFoundException(path);
            
            var fullPath = GetPathFromRoot(path);

            File.Delete(fullPath);
        }

        public bool Exists(string path)
        {
            var fullPath = GetPathFromRoot(path);
            return File.Exists(fullPath) || Directory.Exists(fullPath);
        }

        public bool IsFile(string path)
        {
            if (!Exists(path))
                throw new ScriptFileSystemNotFoundException(path);
            
            var fullPath = GetPathFromRoot(path);
            FileAttributes attributes = File.GetAttributes(fullPath);
            return (attributes & FileAttributes.Normal) == FileAttributes.Normal;
        }

        public bool IsDirectory(string path)
        {
            if (!Exists(path))
                throw new ScriptFileSystemNotFoundException(path);
            
            var fullPath = GetPathFromRoot(path);
            FileAttributes attributes = File.GetAttributes(fullPath);
            return (attributes & FileAttributes.Directory) == FileAttributes.Directory;
        }

        public bool IsAccessible(string path)
        {
            try
            {
                GetPathFromRoot(path);
                return true;
            }
            catch (ScriptFileSystemInaccessibleException)
            {
                return false;
            }
        }

        public IEnumerable<string> GetDirectories(string path)
        {
            var fullPath = GetPathFromRoot(path);
            if (fullPath == null || !Directory.Exists(fullPath)) return null;

            return Directory.EnumerateDirectories(fullPath).Select(d => Path.GetRelativePath(_root, d));
        }

        public IEnumerable<string> GetFiles(string path)
        {
            var fullPath = GetPathFromRoot(path);
            if (fullPath == null || !Directory.Exists(fullPath)) return null;

            return Directory.EnumerateFiles(fullPath).Select(f => Path.GetRelativePath(_root, f));
        }
    }
}