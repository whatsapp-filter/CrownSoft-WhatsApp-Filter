using System.Collections.Generic;

namespace WhatsAppNETAPI
{
	public class GroupNotification
	{
		public string id { get; set; }

		public string name { get; set; }

		public IList<Recipient> recipients { get; set; }

		public GroupNotification()
		{
			recipients = new List<Recipient>();
		}
	}
}
