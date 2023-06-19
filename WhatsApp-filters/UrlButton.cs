using Newtonsoft.Json;

namespace WhatsAppNETAPI
{
	public class UrlButton
	{
		[JsonProperty("body")]
		public string title { get; set; }

		public string url { get; set; }
	}
}
