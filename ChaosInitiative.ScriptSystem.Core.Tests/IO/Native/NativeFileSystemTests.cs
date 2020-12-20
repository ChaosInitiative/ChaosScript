using System;
using System.IO;
using ChaosInitiative.ScriptSystem.Core.Exceptions;
using ChaosInitiative.ScriptSystem.Core.IO.Native;
using NUnit.Framework;

namespace ChaosInitiative.ScriptSystem.Core.Tests.IO.Native
{
    public class NativeFileSystemTests
    {
        private string _rootPath;
        private NativeFileSystem _fileSystem;
        private const string TestFileName = "testfile.txt";
        private const string InaccessibleFileName = "../inaccessible.txt";

        [OneTimeSetUp]
        public void Init()
        {
            _rootPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "test_filesystem"));
            Directory.CreateDirectory(_rootPath);
            _fileSystem = new NativeFileSystem(_rootPath);
        }

        private void CreateTestFile()
        {
            File.WriteAllText(Path.Combine(_rootPath, TestFileName), "Test content");
        }

        private void DeleteTestFile()
        {
            File.Delete(Path.Combine(_rootPath, TestFileName));
        }
        
        [Test]
        public void TestOpenAccessibleFileThatExists()
        {
            CreateTestFile();
            Assume.That(_fileSystem.Exists(TestFileName), Is.True);
            var file = _fileSystem.Open(TestFileName, FileMode.Open, FileAccess.Read);
            Assert.That(file, Is.Not.Null);
            file.Close();
            DeleteTestFile();
        }

        [Test]
        public void TestOpenAccessibleFileThatDoesntExistThrows()
        {
            Assume.That(_fileSystem.Exists(TestFileName), Is.False);
            Assert.That(() => _fileSystem.Open(TestFileName, FileMode.Open, FileAccess.Read), Throws.TypeOf<ScriptFileSystemNotFoundException>());
        }

        [Test]
        public void TestOpenInaccessibleFileThrows()
        {
            Assume.That(() => _fileSystem.Open(InaccessibleFileName, FileMode.Open, FileAccess.Read),
                        Throws.TypeOf<ScriptFileSystemInaccessibleException>());
        }

        [Test]
        public void TestDeleteAccessibleFile()
        {
            CreateTestFile();
            Assume.That(_fileSystem.Exists(TestFileName), Is.True);
            Assert.That(() => _fileSystem.Delete(TestFileName), Throws.Nothing);
            Assert.That(_fileSystem.Exists(TestFileName), Is.False);
        }

        [Test]
        public void TestDeleteAccessibleFileThatDoesNotExistThrows()
        {
            Assume.That(_fileSystem.Exists(TestFileName), Is.False);
            Assert.That(() => _fileSystem.Delete(TestFileName), 
                        Throws.TypeOf<ScriptFileSystemNotFoundException>());
        }

        [Test]
        public void TestDeleteInaccessibleFileThrows()
        {
            Assume.That(() => _fileSystem.Delete(InaccessibleFileName), 
                        Throws.TypeOf<ScriptFileSystemInaccessibleException>());
        }

        [Test]
        public void TestExistsWhenFileExists()
        {
            CreateTestFile();
            bool existsOnDisk = File.Exists(Path.Combine(_rootPath, TestFileName));
            
            Assume.That(existsOnDisk, Is.True);
            bool existsInFileSystem = _fileSystem.Exists(TestFileName);
            
            Assert.That(existsOnDisk, Is.EqualTo(existsInFileSystem));
            DeleteTestFile();
        }

        [Test]
        public void TestExistsWhenFileDoesNotExist()
        {
            bool existsOnDisk = File.Exists(Path.Combine(_rootPath, TestFileName));
            
            Assume.That(existsOnDisk, Is.False);
            bool existsInFileSystem = _fileSystem.Exists(TestFileName);
            
            Assert.That(existsOnDisk, Is.EqualTo(existsInFileSystem));
        }

        [Test]
        public void TestGetDirectories()
        {
            
        }

        [Test]
        public void TestGetFiles()
        {
            
        }
    }
}