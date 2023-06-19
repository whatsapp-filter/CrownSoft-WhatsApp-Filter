using Newtonsoft.Json;

namespace WhatsAppNETAPI
{
	public class QuickReplyButton
	{
		public string id { get; set; }

		[JsonProperty("body")]
		public string title { get; set; }
	}
}
