using System.Collections.Generic;

namespace WhatsAppNETAPI
{
	internal class JsonMessage
	{
		public GroupNotification groupNotification { get; set; }

		public Message message { get; set; }

		public MessageStatus messageStatus { get; set; }

		public GroupStatus groupStatus { get; set; }

		public IList<Message> messages { get; set; }

		public IList<Contact> contacts { get; set; }

		public IList<BusinessProfile> businessProfiles { get; set; }

		public IList<Group> groups { get; set; }

		public string sessionId { get; set; }
	}
}
