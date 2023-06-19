using System.Collections.Generic;

namespace WhatsAppNETAPI
{
	public delegate void OnReceiveBusinessProfileEventHandler(IList<BusinessProfile> profiles, string sessionId);
}
