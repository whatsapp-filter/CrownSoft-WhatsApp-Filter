using System.Collections.Generic;

namespace WhatsAppNETAPI
{
	public delegate void OnReceiveGroupEventHandler(IList<Group> groups, string sessionId);
}
