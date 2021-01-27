using System;
using System.Runtime.InteropServices;
using ScriptSystem.Source;

namespace ScriptSystem.Source
{
    public class LogChannel
    {
        private Logging.LogSeverity _ChannelSeverity { set; get; }
        private String _ChannelName { set; get; }
        private Color _ChannelColor { set; get; }
        private SWIGTYPE_p_LoggingChannelID_t ChannelId;
        
        public LogChannel(String channelName, Logging.LogSeverity severity, Color color)
        {
            _ChannelName = channelName;
            _ChannelSeverity = severity;
            _ChannelColor = color;
            
            ChannelId = Logging.RegisterLoggingChannel(channelName, severity, color);
        }

        public void Log(String msg, Logging.LogSeverity severity = Logging.LogSeverity.LS_MESSAGE)
        {
            Logging.Log(ChannelId, severity, msg);
        }

        public void Log(Color color, String msg, Logging.LogSeverity severity = Logging.LogSeverity.LS_MESSAGE)
        {
            Logging.Log(ChannelId, severity, color, msg);
        }

        public void LogWarning(String msg)
        {
            Logging.Log(ChannelId, Logging.LogSeverity.LS_WARNING, msg);
        }

        public void LogWarning(Color color, String msg)
        {
            Logging.Log(ChannelId, Logging.LogSeverity.LS_WARNING, color, msg);
        }

        public void SetChannelColor(Color color)
        {
            Logging.SetChannelColor(ChannelId, (uint)color.GetRawColor());
            _ChannelColor = color;
        }

        public Color GetChannelColor()
        {
            return _ChannelColor;
        }

        public String GetChannelName()
        {
            return _ChannelName;
        }

        public Logging.LogSeverity GetChannelSeverity()
        {
            return _ChannelSeverity;
        }

        public void SetChannelSeverity(Logging.LogSeverity severity)
        {
            _ChannelSeverity = severity;
            Logging.SetLoggingLevel(ChannelId, severity);
        }
    }
}