using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace WhatsAppNETAPI
{
	public class SignalRServer : ISignalRServer
	{
		private readonly string _serverUrl = "http://127.0.0.1:{0}";

		private string _port = "8282";

		private IDisposable signalR { get; set; }

		public SignalRServer()
		{
		}

		public SignalRServer(string port)
		{
			_port = port;
		}

		public void Connect()
		{
			WriteToConsole("Starting server...");
			Task.Run(delegate
			{
				StartServer();
			});
		}

		public void Disconnect()
		{
			if (signalR != null)
			{
				signalR.Dispose();
			}
		}

		private void StartServer()
		{
			try
			{
				signalR = WebApp.Start(string.Format(_serverUrl, _port));
			}
			catch (TargetInvocationException ex)
			{
				WriteToConsole("Server failed to start. Ex: " + ex.Message);
				return;
			}
			WriteToConsole("Server started at " + string.Format(_serverUrl, _port));
		}

		private void WriteToConsole(string s)
		{
		}
	}
}
