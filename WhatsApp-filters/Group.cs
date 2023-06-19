using System.Collections.Generic;

namespace WhatsAppNETAPI
{
	public class Group
	{
		public string id { get; set; }

		public string name { get; set; }

		public string greeting_message { get; set; }

		public IList<Contact> members { get; set; }

		public Group()
		{
			members = new List<Contact>();
		}
	}
}
