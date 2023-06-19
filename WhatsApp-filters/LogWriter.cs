using System;
using System.IO;
using System.Reflection;

namespace WhatsAppNETAPI
{
	public class LogWriter
	{
		public static void LogWrite(string logMessage)
		{
			try
			{
				using (StreamWriter txtWriter = File.AppendText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\logs.txt"))
				{
					Log(logMessage, txtWriter);
				}
			}
			catch
			{
			}
		}

		private static void Log(string logMessage, TextWriter txtWriter)
		{
			try
			{
				txtWriter.Write("\r\nLog Entry:");
				txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
				txtWriter.WriteLine("  :{0}", logMessage);
				txtWriter.WriteLine("-------------------------------");
			}
			catch
			{
			}
		}
	}
}
