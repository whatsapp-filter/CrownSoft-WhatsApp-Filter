using System;

namespace WhatsAppNETAPI
{
	public class Sender
	{
		public string id { get; set; }

		public string name { get; set; }

		public string shortName { get; set; }

		[Obsolete("Property ini sudah tidak digunakan silahkan menggunakan property name.", false)]
		public string pushname { get; set; }
	}
}
