//======================================================//
// Interface between ScriptSystem and the engine.
// Everything in here is designed for simple and easy
// interop between the engine and ScriptSystem
//======================================================//
using System;
using ScriptSystem.Source;

namespace ScriptSystem.Core.Interface
{
	/* Must match definitions in-engine */
	public enum EventType
	{
		GENERIC = 0,            // Anything else
		GAME_CLIENT = 1,        // Game events originating from the client
		GAME_SERVER = 2,        // Game events originating from the server
		SCRIPTSYSTEM = 3,       // Events originating from the scriptsystem itself
		SCRIPTSYSTEM_LOAD = 4,  // Called when the module is loaded and passes the security audit
		SCRIPTSYSTEM_UNLOAD = 5,        // Called just before the module is unloaded
		SCRIPTSYSTEM_SHUTDOWN = 6,      // Called when the module is being shutdown to be unloaded
		ENGINE = 7,                     // Event originating from the engine, not necessarily tied with the client or server
	}
	
	public class EventParameters
	{
		public string Name;
	}
	
	public class EngineInterface
	{
		public static LogChannel channel;
		static bool Init()
		{
			channel = new LogChannel("ScriptSystemManaged", Logging.LogSeverity.LS_MESSAGE, new Color(200, 255, 200));
			
			
			channel.Log("ScriptSystem.Core initialized\n");
			channel.Log("\tRunning on .NET " + Environment.Version.ToString() + "\n");
			channel.Log("\tOS: " + Environment.OSVersion.ToString() + "\n"); 
			channel.Log("\tPage Size: " + Environment.SystemPageSize + "\n");
			channel.Log("\tIs 64-bit OS: " + Environment.Is64BitOperatingSystem + "\n");
			channel.Log("\tCore Count: " + Environment.ProcessorCount + "\n");
			return true;
		}

		static void Shutdown()
		{
			
		}

		static int LoadModule(string modulePath)
		{
			return -1;
		}

		static bool UnloadModule(int module)
		{
			return false;
		}

		static void InvokeEvent(int eventType)
		{
			
		}
		
		
	}
}