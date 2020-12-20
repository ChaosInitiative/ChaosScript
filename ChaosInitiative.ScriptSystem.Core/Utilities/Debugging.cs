using System.Diagnostics;
using Serilog;

namespace ChaosInitiative.ScriptSystem.Core.Utilities
{
    internal static class Debugging
    {
        public static void Assert(bool condition, string message = null)
        {
#if DEBUG
            if (condition) return;

            var stackTrace = new StackTrace();
            var stackFrame = stackTrace.GetFrame(1);

            var fileName = stackFrame.GetFileName();
            var lineNumber = stackFrame.GetFileLineNumber();

            var format = $"Assertion triggered: {fileName}, line {lineNumber}";

            if (!string.IsNullOrWhiteSpace(message))
            {
                Log.Error(format + ": {Message}", message);
            }
            else
            {
                Log.Error(format);
            }
#endif
        }
    }
}