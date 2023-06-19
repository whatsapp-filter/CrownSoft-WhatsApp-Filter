using Newtonsoft.Json;

namespace WhatsAppNETAPI
{
	public class BusinessProfile
	{
		[JsonProperty("wid")]
		public string id { get; set; }

		public string description { get; set; }

		public string email { get; set; }

		public string category { get; set; }

		public string[] website { get; set; }

		public BusinessHours business_hours { get; set; }
	}
}
