namespace MvcTurbine.Samples.LoggingBlade
{
	using System;
	using System.IO;
	using Blades;
	using ComponentModel;
	using log4net.Config;
	using log4net;

	public class Log4NetBlade : Blade
	{
		public override void Spin(IRotorContext context)
		{
			// Get the path to the file
			string path = AppDomain.CurrentDomain.BaseDirectory;
			string filePath = Path.Combine(path, "log4net.config");

			var fileInfo = new FileInfo(filePath);
			if (fileInfo.Exists)
			{
				// Get the info from the file
				XmlConfigurator.ConfigureAndWatch(fileInfo);
			}
			else
			{
				// Look at the web.config for the info
				XmlConfigurator.Configure();
			}

			// Register the log instance
			var locator = GetServiceLocatorFromContext(context);
			using (locator.Batch())
			{
				locator.Register(() => LogManager.GetLogger(GetType()));
			}
		}
	}
}
