namespace MvcTurbine.Samples.LoggingBlade {
    using System;
    using System.IO;
    using Blades;
    using ComponentModel;
    using log4net.Config;

    public class Log4netBlade : Blade, ISupportAutoRegistration {
        public override void Spin(IRotorContext context) {
            // Setup log4net
            SetupLogging();
        }

        private static void SetupLogging() {
            // Get the path to the file
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(path, "log4net.config");

            var fileInfo = new FileInfo(filePath);
            if (fileInfo.Exists) {
                // Get the info from the file
                XmlConfigurator.ConfigureAndWatch(fileInfo);
            }
            else {
                // Look at the web.config for the info
                XmlConfigurator.Configure();
            }
        }

        public void AddRegistrations(AutoRegistrationList registrationList) {
            // Tell the system that this blade provides an ILogger and 
            // that it should auto-register anything that implements it.
            registrationList.Add(Registration.Simple<ILogger>());
        }
    }
}