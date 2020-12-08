using System;
using System.Collections.Generic;

namespace ScriptSystem.Core.Compilation
{
    /// <summary>
    /// Represents the result from a code compilation attempt.
    /// </summary>
    
    internal struct CompileResult
    {
        /// <summary>
        /// Whether or not the compilation succeeded.
        /// </summary>
        public bool Success;

        /// <summary>
        /// Intermediate Language compiled bytecode.
        /// </summary>
        public byte[] IL;
    }
}