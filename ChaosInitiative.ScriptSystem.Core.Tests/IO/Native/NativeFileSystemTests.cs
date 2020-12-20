using System;
using System.IO;
using ChaosInitiative.ScriptSystem.Core.Exceptions;
using ChaosInitiative.ScriptSystem.Core.IO.Native;
using NUnit.Framework;

namespace ChaosInitiative.ScriptSystem.Core.Tests.IO.Native
{
    public class NativeFileSystemTests
    {
        private readonly NativeFileSystem _fileSystem;
        public NativeFileSystemTests()
        {
            var root = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "../../../data/root"));
            _fileSystem = new NativeFileSystem(root);
        }

        [Fact]
        public void TestOpen()
        {
            var file = _fileSystem.Open("testfile.txt", FileMode.Open, FileAccess.Read);
            Assert.True(file != null);
            file.Close();

            var file2 = _fileSystem.Open("../inaccessible.txt", FileMode.Open, FileAccess.Read);
            Assert.True(file2 == null);
        }

        [Fact]
        public void TestDelete()
        {
            
        }

        [Fact]
        public void TestExists()
        {

        }

        [Fact]
        public void TestEnumerateDirectories()
        {
            
        }

        [Fact]
        public void TestEnumerateFiles()
        {
            
        }
    }
}