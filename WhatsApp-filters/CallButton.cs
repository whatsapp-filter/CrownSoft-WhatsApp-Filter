using Newtonsoft.Json;

namespace WhatsAppNETAPI
{
	public class CallButton
	{
		[JsonProperty("body")]
		public string title { get; set; }

		public string phoneNumber { get; set; }
	}
}
