using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ChaosInitiative.ScriptSystem.Core.Modules;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace ChaosInitiative.ScriptSystem.Core.Compilation
{
    /// <summary>
    /// Handles using Roslyn to compile C# code into IL.
    /// </summary>
    internal class ScriptCompiler
    {
        /// <summary>
        /// Latest C# language version the compiler supports.
        /// </summary>
        private const LanguageVersion CurrentVersion = LanguageVersion.CSharp9;

        /// <summary>
        /// Whether the compiler should produce code built in debug mode.
        /// </summary>
        public bool Debug { get; private set; }

        public ScriptCompiler(bool debug = false)
        {
            Debug = debug;
        }

        /// <summary>
        /// Compiles an AST of code into an assembly.
        /// </summary>
        public async Task<CompileResult> CompileAsync(string assemblyName, IEnumerable<SyntaxTree> syntaxTrees)
        {
            var options = new CSharpCompilationOptions(
                OutputKind.DynamicallyLinkedLibrary,
                optimizationLevel: Debug ? OptimizationLevel.Debug : OptimizationLevel.Release,
                assemblyIdentityComparer: DesktopAssemblyIdentityComparer.Default
            );

            var compiler = CSharpCompilation.Create(assemblyName, syntaxTrees, options: options);
            var result = new CompileResult();

            using (var stream = new MemoryStream())
            {
                // use result.Diagnostics to get errors
                var emitResult = compiler.Emit(stream);

                if (emitResult.Success)
                {
                    result.Success = true;
                    result.IL = stream.ToArray();
                }
            }

            return await Task.FromResult(result);
        }

        public async Task<CompileResult> CompileAsync(string assemblyName, IEnumerable<SourceFile> files)
        {
            var trees = new List<SyntaxTree>();
            var options = CSharpParseOptions.Default.WithLanguageVersion(CurrentVersion);

            foreach (var file in files)
            {
                trees.Add(SyntaxFactory.ParseSyntaxTree(file.Text, options));
            }

            return await CompileAsync(assemblyName, trees);
        }
    }
}