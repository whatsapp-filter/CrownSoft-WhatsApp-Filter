using Newtonsoft.Json;

namespace WhatsAppNETAPI
{
	public class ButtonItem
	{
		public string id { get; set; }

		[JsonProperty("body")]
		public string title { get; set; }

		public int index { get; set; }

		public UrlButton urlButton { get; set; }

		public CallButton callButton { get; set; }

		public QuickReplyButton quickReplyButton { get; set; }
	}
}
