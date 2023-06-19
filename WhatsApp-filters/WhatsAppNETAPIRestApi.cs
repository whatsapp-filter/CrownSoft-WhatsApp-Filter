using System;
using System.Collections.Generic;
using System.Threading;
using JamesWright.SimpleHttp;
using Newtonsoft.Json;

namespace WhatsAppNETAPI
{
	public class WhatsAppNETAPIRestApi : IWhatsAppNETAPIRestApi
	{
		private App _app;

		private IWhatsAppNETAPI _wa;

		private AutoResetEvent _are;

		private IList<Contact> _contacts = new List<Contact>();

		private IList<Group> _groups = new List<Group>();

		private IList<Message> _messages = new List<Message>();

		private BatteryStatus _batteryStatus;

		public WhatsAppNETAPIRestApi(IWhatsAppNETAPI wa)
		{
			_app = new App();
			_wa = wa;
			RegisterSystemRoute();
			RegisterMessageRoute();
			RegisterSendMessageRoute();
			RegisterArchiveDeleteChatRoute();
			RegisterContactAndGroupRoute();
		}

		public void Start(string url, string port = "8005")
		{
			_app.Start(url, port);
		}

		public void Stop()
		{
			_app.Stop();
		}

		private void RegisterSystemRoute()
		{
			_app.Get("/waNumber", async delegate(Request req, Response res)
			{
				res.Content = JsonConvert.SerializeObject(new
				{
					waNumber = _wa.GetCurrentNumber
				});
				res.ContentType = "application/json";
				await res.SendAsync();
			});
		}

		private void RegisterMessageRoute()
		{
			_app.Get("/allMessagesByContact", async delegate(Request req, Response res)
			{
				_messages.Clear();
				_are = new AutoResetEvent(initialState: false);
				string phoneNumber = req.Parameters["contact"];
				string s = req.Parameters["limit"];
				_wa.OnReceiveMessages += OnReceiveMessagesHandler;
				_wa.GetAllMessage(phoneNumber, int.Parse(s));
				_are.WaitOne(TimeSpan.FromSeconds(30.0));
				_wa.OnReceiveMessages -= OnReceiveMessagesHandler;
				res.Content = JsonConvert.SerializeObject(_messages);
				res.ContentType = "application/json";
				await res.SendAsync();
			});
		}

		private void RegisterSendMessageRoute()
		{
			_app.Post("/sendText", async delegate(Request req, Response res)
			{
				MessageSend messageSend4 = JsonConvert.DeserializeObject<MessageSend>(await req.GetBodyAsync());
				MsgArgs message7 = new MsgArgs(messageSend4.contact, messageSend4.message, messageSend4.type);
				_wa.SendMessage(message7);
				SetRestOutput("Pesan sudah dikirim", res);
				await res.SendAsync();
			});
			_app.Post("/sendImage", async delegate(Request req, Response res)
			{
				MessageSend messageSend3 = JsonConvert.DeserializeObject<MessageSend>(await req.GetBodyAsync());
				MsgArgs message6 = new MsgArgs(messageSend3.contact, messageSend3.message, messageSend3.type, messageSend3.attachmentOrUrl);
				_wa.SendMessage(message6);
				SetRestOutput("Pesan sudah dikirim", res);
				await res.SendAsync();
			});
			_app.Post("/sendFile", async delegate(Request req, Response res)
			{
				MessageSend messageSend2 = JsonConvert.DeserializeObject<MessageSend>(await req.GetBodyAsync());
				MsgArgs message5 = new MsgArgs(messageSend2.contact, messageSend2.message, messageSend2.type, messageSend2.attachmentOrUrl);
				_wa.SendMessage(message5);
				SetRestOutput("Pesan sudah dikirim", res);
				await res.SendAsync();
			});
			_app.Post("/sendMediaFromUrl", async delegate(Request req, Response res)
			{
				MessageSend messageSend = JsonConvert.DeserializeObject<MessageSend>(await req.GetBodyAsync());
				MsgArgs message4 = new MsgArgs(messageSend.contact, messageSend.message, messageSend.type, messageSend.attachmentOrUrl);
				_wa.SendMessage(message4);
				SetRestOutput("Pesan sudah dikirim", res);
				await res.SendAsync();
			});
			_app.Post("/sendLocation", async delegate(Request req, Response res)
			{
				MessageLocation messageLocation = JsonConvert.DeserializeObject<MessageLocation>(await req.GetBodyAsync());
				MsgArgs message3 = new MsgArgs(messageLocation.contact, messageLocation.message);
				_wa.SendMessage(message3);
				SetRestOutput("Pesan sudah dikirim", res);
				await res.SendAsync();
			});
			_app.Post("/sendList", async delegate(Request req, Response res)
			{
				MessageList messageList = JsonConvert.DeserializeObject<MessageList>(await req.GetBodyAsync());
				MsgArgs message2 = new MsgArgs(messageList.contact, messageList.message);
				_wa.SendMessage(message2);
				SetRestOutput("Pesan sudah dikirim", res);
				await res.SendAsync();
			});
			_app.Post("/sendButton", async delegate(Request req, Response res)
			{
				MessageButton messageButton = JsonConvert.DeserializeObject<MessageButton>(await req.GetBodyAsync());
				MsgArgs message = new MsgArgs(messageButton.contact, messageButton.message);
				_wa.SendMessage(message);
				SetRestOutput("Pesan sudah dikirim", res);
				await res.SendAsync();
			});
		}

		private void RegisterArchiveDeleteChatRoute()
		{
			_app.Post("/archiveChat", async delegate(Request req, Response res)
			{
				string value = await req.GetBodyAsync();
				if (string.IsNullOrEmpty(value))
				{
					_wa.ArchiveChat();
				}
				else
				{
					object obj = JsonConvert.DeserializeObject<object>(value);
					string text = Convert.ToString(((dynamic)obj).contact.Value);
					if (string.IsNullOrEmpty(text))
					{
						_wa.ArchiveChat();
					}
					else
					{
						_wa.ArchiveChat(text);
					}
				}
				SetRestOutput("Pesan sudah diarsipkan", res);
				await res.SendAsync();
			});
		}

		private void RegisterContactAndGroupRoute()
		{
			_app.Get("/contacts", async delegate(Request req, Response res)
			{
				_contacts.Clear();
				_are = new AutoResetEvent(initialState: false);
				_wa.OnReceiveContacts += OnReceiveContactsHandler;
				_wa.GetContacts();
				_are.WaitOne(TimeSpan.FromSeconds(30.0));
				_wa.OnReceiveContacts -= OnReceiveContactsHandler;
				res.Content = JsonConvert.SerializeObject(_contacts);
				res.ContentType = "application/json";
				await res.SendAsync();
			});
			_app.Get("/groups", async delegate(Request req, Response res)
			{
				_groups.Clear();
				_are = new AutoResetEvent(initialState: false);
				_wa.OnReceiveGroups += OnReceiveGroupsHandler;
				_wa.GetGroups();
				_are.WaitOne(TimeSpan.FromSeconds(30.0));
				_wa.OnReceiveGroups -= OnReceiveGroupsHandler;
				res.Content = JsonConvert.SerializeObject(_groups);
				res.ContentType = "application/json";
				await res.SendAsync();
			});
		}

		private void OnChangeBatteryHandler(BatteryStatus status, string sessionId)
		{
			_batteryStatus = status;
			_are.Set();
		}

		private void OnReceiveMessagesHandler(IList<Message> messages, string sessionId)
		{
			foreach (Message message in messages)
			{
				if (!(message.id == "status@broadcast"))
				{
					_messages.Add(message);
				}
				else
				{
					_are.Set();
				}
			}
		}

		private void OnReceiveContactsHandler(IList<Contact> contacts, string sessionId)
		{
			foreach (Contact contact in contacts)
			{
				if (!(contact.id == "status@broadcast"))
				{
					_contacts.Add(contact);
				}
				else
				{
					_are.Set();
				}
			}
		}

		private void OnReceiveGroupsHandler(IList<Group> groups, string sessionId)
		{
			foreach (Group group in groups)
			{
				if (!(group.id == "status@broadcast"))
				{
					_groups.Add(group);
				}
				else
				{
					_are.Set();
				}
			}
		}

		private void SetRestOutput(string message, Response res)
		{
			res.Content = JsonConvert.SerializeObject(new { message });
			res.ContentType = "application/json";
		}
	}
}
