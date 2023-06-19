using System.Collections.Generic;

namespace WhatsAppNETAPI
{
	public delegate void OnReceiveContactEventHandler(IList<Contact> contacts, string sessionId);
}
