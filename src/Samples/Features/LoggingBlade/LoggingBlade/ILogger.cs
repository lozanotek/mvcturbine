namespace MvcTurbine.Samples.LoggingBlade {
    using System;

    public interface ILogger {
        void LogMessage(string message);
        void LogException(string message, Exception ex);
    }
}