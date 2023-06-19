using System;
using System.Collections.Generic;

namespace WhatsAppNETAPI
{
	public class Message
	{
		public string id { get; set; }

		public string content { get; set; }

		public MessageAck ack { get; set; }

		public string type { get; set; }

		public string from { get; set; }

		public string to { get; set; }

		public string filename { get; set; }

		public Sender sender { get; set; }

		public GroupSender group { get; set; }

		public Location location { get; set; }

		public IList<VCard> vcards { get; set; }

		public IList<string> vcardFilenames { get; set; }

		public int unixTimestamp { get; set; }

		public string selectedButtonId { get; set; }

		public string selectedRowId { get; set; }

		public DateTime datetime => new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(unixTimestamp).ToLocalTime();

		public Message()
		{
			vcards = new List<VCard>();
			vcardFilenames = new List<string>();
		}
	}
}
