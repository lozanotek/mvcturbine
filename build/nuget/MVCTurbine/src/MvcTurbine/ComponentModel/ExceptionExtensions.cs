namespace MvcTurbine.ComponentModel {
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;

    internal static class ExceptionExtensions {
        /// <summary>
        /// Provides formatting information for troubleshooting a missing reference issue.
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static string GetDetailedMessage(this ReflectionTypeLoadException exception, string assemblyName) {
            var buffer = new StringBuilder();
            buffer.AppendFormat("MVC Turbine could not find & load the dependencies for assembly '{0}'", assemblyName);
            buffer.AppendLine();
            buffer.AppendLine();
            
            Exception[] exceptions = exception.LoaderExceptions;

            if (exceptions != null && exceptions.Length > 0) {
                buffer.AppendLine("Dependencies that failed to load: ");
                buffer.AppendLine("-----------------------------------");
                buffer.AppendLine();

                var messages = new Dictionary<string, string>();
                foreach (Exception loaderException in exceptions) {
                    if (messages.ContainsKey(loaderException.Message)) continue;

                    messages.Add(loaderException.Message, loaderException.StackTrace);
                }

                foreach (var message in messages) {
                    buffer.AppendLine(message.Key);
                    buffer.AppendLine();
                    buffer.AppendLine("-----------------------------------");
                    buffer.AppendLine();
                }
            }

            return buffer.ToString();
        }
    }
}