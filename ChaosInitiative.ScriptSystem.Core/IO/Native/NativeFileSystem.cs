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

        private string ResolveFromRoot(string path)
        {
            var fullPath = Path.GetFullPath(Path.Combine(_root, path));

            // security check to ensure our path is within the root
            if (!fullPath.StartsWith(_root)) return null;

            return fullPath;
        }

        public Stream Open(string path, FileMode mode, FileAccess access)
        {
            var fullPath = ResolveFromRoot(path);
            return fullPath == null ? null : File.Open(path, mode, access);
        }

        public void Delete(string path)
        {
            var fullPath = ResolveFromRoot(path);
            if (fullPath == null) return;

            File.Delete(fullPath);
        }

        public bool Exists(string path)
        {
            var fullPath = ResolveFromRoot(path);
            return fullPath != null && (File.Exists(fullPath) || Directory.Exists(fullPath));
        }

        public IEnumerable<string> EnumerateDirectories(string path)
        {
            var fullPath = ResolveFromRoot(path);
            if (fullPath == null || !Directory.Exists(fullPath)) return null;

            return Directory.EnumerateDirectories(fullPath).Select(d => Path.GetRelativePath(_root, d));
        }

        public IEnumerable<string> EnumerateFiles(string path)
        {
            var fullPath = ResolveFromRoot(path);
            if (fullPath == null || !Directory.Exists(fullPath)) return null;

            return Directory.EnumerateFiles(fullPath).Select(f => Path.GetRelativePath(_root, f));
        }
    }
}