using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Owin;

namespace WhatsAppNETAPI
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			HubConfiguration hubConfiguration = new HubConfiguration();
			hubConfiguration.EnableDetailedErrors = true;
			app.UseCors(CorsOptions.AllowAll);
			app.MapSignalR(hubConfiguration);
		}
	}
}
