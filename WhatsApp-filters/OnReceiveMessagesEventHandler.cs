using System.Collections.Generic;

namespace WhatsAppNETAPI
{
	public delegate void OnReceiveMessagesEventHandler(IList<Message> messages, string sessionId);
}
