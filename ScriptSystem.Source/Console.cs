using System;
using ScriptSystem.Source;

namespace ScriptSystem.Source
{
    public class Console
    {
        public void Msg(String msg)
        {
            Logging.Msg(msg);
        }

        public void DevMsg(String msg)
        {
            Logging.DevMsg(msg);
        }

        public void Warning(String msg)
        {
            Logging.Warning(msg);
        }

        public void DevWarning(String msg)
        {
            Logging.DevWarning(msg);
        }

        public void MsgC(Color color, String message)
        {
            Logging.ConColorMsg(color, message);
            
        }
    }
}