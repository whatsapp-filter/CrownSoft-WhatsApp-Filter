using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WhatsAppNETAPI
{
	public class MsgArgs
	{
		public string send_to { get; set; }

		public string message { get; set; }

		public string type { get; set; }

		public string attachmentOrUrl { get; set; }

		public string timestamp { get; set; }

		public string sessionId { get; set; }

		public string[] mentions { get; set; }

		public MsgArgs(string contact, Sticker sticker)
		{
			send_to = contact;
			message = JsonConvert.SerializeObject(sticker);
			type = "sticker";
			timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
		}

		public MsgArgs(string contact, VCard[] vcards)
		{
			List<string> list = new List<string>();
			foreach (VCard vCard in vcards)
			{
				list.Add(vCard.ToString());
			}
			send_to = contact;
			message = JsonConvert.SerializeObject(new
			{
				vcards = list.ToArray()
			});
			type = "vcard";
			timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
		}

		public MsgArgs(string contact, Button button)
		{
			send_to = contact;
			message = JsonConvert.SerializeObject(button);
			type = "button";
			timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
		}

		public MsgArgs(string contact, Button button, string attachmentOrUrl)
			: this(contact, button)
		{
			this.attachmentOrUrl = attachmentOrUrl;
		}

		public MsgArgs(string contact, List list)
		{
			send_to = contact;
			message = JsonConvert.SerializeObject(list);
			type = "list";
			timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
		}

		public MsgArgs(string contact, Location location)
		{
			send_to = contact;
			message = JsonConvert.SerializeObject(location);
			type = "location";
			timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
		}

		public MsgArgs(string contact, string message, string type)
		{
			send_to = contact;
			this.message = message;
			this.type = type;
			timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
		}

		public MsgArgs(string contact, string message, string type, string[] mentions)
			: this(contact, message, type)
		{
			this.mentions = mentions;
		}

		public MsgArgs(string contact, string message, string type, string attachmentOrUrl)
			: this(contact, message, type)
		{
			this.attachmentOrUrl = attachmentOrUrl;
		}
	}
}
