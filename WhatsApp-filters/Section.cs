using Newtonsoft.Json;

namespace WhatsAppNETAPI
{
	public class Section
	{
		public string title { get; set; }

		[JsonProperty("rows")]
		public ListItem[] items { get; set; }
	}
}
