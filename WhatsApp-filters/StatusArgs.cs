namespace WhatsAppNETAPI
{
	public class StatusArgs
	{
		public string send_to { get; set; } = "status@broadcast";


		public string message { get; set; }

		public string attachmentOrUrl { get; set; }

		public string type { get; set; }

		public string sessionId { get; set; }

		public StatusArgs(string status)
		{
			message = status;
			type = "text";
		}

		public StatusArgs(string status, string type, string attachmentOrUrl)
		{
			message = status;
			this.type = type;
			this.attachmentOrUrl = attachmentOrUrl;
		}
	}
}
