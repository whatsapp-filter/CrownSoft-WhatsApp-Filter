using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;

namespace WhatsAppNETAPI
{
	public class SignalRClient : ISignalRClient
	{
		private readonly string _serverUrl = "http://127.0.0.1:{0}";

		private string _port = "8282";

		private string _sessionId = "lsgWFcmtxHcMWfnlcsjalYRldNtLVkHxqjQd";

		private HubConnection _hubConnection;

		private IHubProxy _hubProxy;

		private IDisposable _onStartupHandler;

		private IDisposable _onChangeStateHandler;

		private IDisposable _onChangeBatteryHandler;

		private IDisposable _onScanMeHandler;

		private IDisposable _onReceiveMessageHandler;

		private IDisposable _onUnreadMessageHandler;

		private IDisposable _onReceiveMessagesHandler;

		private IDisposable _onReceiveContactsHandler;

		private IDisposable _onReceiveBusinessProfilesHandler;

		private IDisposable _onReceiveGroupsHandler;

		private IDisposable _onReceiveMessageStatusHandler;

		private IDisposable _onGroupJoinHandler;

		private IDisposable _onGroupLeaveHandler;

		private IDisposable _onCreateGroupStatusHandler;

		private IDisposable _onMessageAckEventHandler;

		private string _currentNumber = string.Empty;

		public OnStartupEventHandler OnStartup { get; set; }

		public OnChangeStateEventHandler OnChangeState { get; set; }

		public OnChangeBatteryEventHandler OnChangeBattery { get; set; }

		public OnScanMeEventHandler OnScanMe { get; set; }

		public OnConnectedEventHandler OnClientConnected { get; set; }

		public OnReceiveMessageEventHandler OnReceiveMessage { get; set; }

		public OnReceiveMessageEventHandler OnUnreadMessage { get; set; }

		public OnReceiveMessagesEventHandler OnReceiveMessages { get; set; }

		public OnReceiveContactEventHandler OnReceiveContacts { get; set; }

		public OnReceiveBusinessProfileEventHandler OnReceiveBusinessProfiles { get; set; }

		public OnReceiveGroupEventHandler OnReceiveGroups { get; set; }

		public OnReceiveMessageStatusEventHandler OnReceiveMessageStatus { get; set; }

		public OnGroupJoinLeaveEventHandler OnGroupJoin { get; set; }

		public OnGroupJoinLeaveEventHandler OnGroupLeave { get; set; }

		public OnCreatedGroupStatusEventHandler OnCreatedGroupStatus { get; set; }

		public OnMessageAckEventHandler OnMessageAck { get; set; }

		public string CurrentNumber => _currentNumber;

		public SignalRClient()
		{
		}

		public SignalRClient(string port, string sessionId)
		{
			_port = port;
			_sessionId = sessionId;
		}

		public void Connect()
		{
			_hubConnection = new HubConnection(string.Format(_serverUrl, _port));
			_hubConnection.TransportConnectTimeout = TimeSpan.FromSeconds(10.0);
			_hubProxy = _hubConnection.CreateHubProxy("ServerHub");
			SubscribeEvent();
			ConnectAsync();
		}

		public void Disconnect()
		{
			UnSubscribeEvent();
			if (_hubConnection != null && _hubConnection.State == ConnectionState.Connected)
			{
				_hubConnection.Stop(new TimeSpan(3000L));
			}
		}

		public Task Invoke(string method, params object[] args)
		{
			if (_hubConnection != null && _hubConnection.State == ConnectionState.Connected)
			{
				_hubProxy.Invoke(method, args);
			}
			return Task.FromResult(0);
		}

		private async void ConnectAsync()
		{
			try
			{
				await _hubConnection.Start();
				WriteToConsole("Client connect to server");
				if (OnClientConnected != null)
				{
					OnClientConnected(_sessionId);
				}
			}
			catch (Exception ex)
			{
				WriteToConsole("Unable to connect to server. Ex: " + ex.Message);
			}
		}

		private void SubscribeEvent()
		{
			_onStartupHandler = _hubProxy.On("OnStartup", delegate(string json)
			{
				OnStartupEventHandler(json);
			});
			_onChangeStateHandler = _hubProxy.On("OnChangeState", delegate(string json)
			{
				OnChangeStateEventHandler(json);
			});
			_onChangeBatteryHandler = _hubProxy.On("OnChangeBattery", delegate(string json)
			{
				OnChangeBatteryEventHandler(json);
			});
			_onScanMeHandler = _hubProxy.On("OnScanMe", delegate(string json)
			{
				OnScanMeEventHandler(json);
			});
			_onReceiveMessageHandler = _hubProxy.On("OnReceiveMessage", delegate(string json)
			{
				OnReceiveMessageEventHandler(json);
			});
			_onUnreadMessageHandler = _hubProxy.On("OnUnreadMessage", delegate(string json)
			{
				OnUnreadMessageEventHandler(json);
			});
			_onReceiveMessagesHandler = _hubProxy.On("OnReceiveMessages", delegate(string json)
			{
				OnReceiveMessagesEventHandler(json);
			});
			_onReceiveContactsHandler = _hubProxy.On("OnReceiveContacts", delegate(string json)
			{
				OnReceiveContactsEventHandler(json);
			});
			_onReceiveBusinessProfilesHandler = _hubProxy.On("OnReceiveBusinessProfiles", delegate(string json)
			{
				OnReceiveBusinessProfilesEventHandler(json);
			});
			_onReceiveGroupsHandler = _hubProxy.On("OnReceiveGroups", delegate(string json)
			{
				OnReceiveGroupsEventHandler(json);
			});
			_onReceiveMessageStatusHandler = _hubProxy.On("OnReceiveMessageStatus", delegate(string json)
			{
				OnReceiveMessageStatusEventHandler(json);
			});
			_onGroupJoinHandler = _hubProxy.On("OnGroupJoin", delegate(string json)
			{
				OnGroupJoinEventHandler(json);
			});
			_onGroupLeaveHandler = _hubProxy.On("OnGroupLeave", delegate(string json)
			{
				OnGroupLeaveEventHandler(json);
			});
			_onCreateGroupStatusHandler = _hubProxy.On("OnCreatedGroupStatus", delegate(string json)
			{
				CreatedGroupStatusHandler(json);
			});
			_onMessageAckEventHandler = _hubProxy.On("OnMessageAck", delegate(string json)
			{
				OnMessageAckEventHandler(json);
			});
		}

		private void UnSubscribeEvent()
		{
			if (_onStartupHandler != null)
			{
				_onStartupHandler.Dispose();
			}
			if (_onChangeStateHandler != null)
			{
				_onChangeStateHandler.Dispose();
			}
			if (_onChangeBatteryHandler != null)
			{
				_onChangeBatteryHandler.Dispose();
			}
			if (_onScanMeHandler != null)
			{
				_onScanMeHandler.Dispose();
			}
			if (_onReceiveMessageHandler != null)
			{
				_onReceiveMessageHandler.Dispose();
			}
			if (_onUnreadMessageHandler != null)
			{
				_onUnreadMessageHandler.Dispose();
			}
			if (_onReceiveMessagesHandler != null)
			{
				_onReceiveMessagesHandler.Dispose();
			}
			if (_onReceiveContactsHandler != null)
			{
				_onReceiveContactsHandler.Dispose();
			}
			if (_onReceiveBusinessProfilesHandler != null)
			{
				_onReceiveBusinessProfilesHandler.Dispose();
			}
			if (_onReceiveGroupsHandler != null)
			{
				_onReceiveGroupsHandler.Dispose();
			}
			if (_onReceiveMessageStatusHandler != null)
			{
				_onReceiveMessageStatusHandler.Dispose();
			}
			if (_onGroupJoinHandler != null)
			{
				_onGroupJoinHandler.Dispose();
			}
			if (_onGroupLeaveHandler != null)
			{
				_onGroupLeaveHandler.Dispose();
			}
			if (_onCreateGroupStatusHandler != null)
			{
				_onCreateGroupStatusHandler.Dispose();
			}
			if (_onMessageAckEventHandler != null)
			{
				_onMessageAckEventHandler.Dispose();
			}
		}

		private void OnStartupEventHandler(string json)
		{
			if (string.IsNullOrEmpty(json) || OnStartup == null)
			{
				return;
			}
			dynamic val = null;
			try
			{
				val = JsonConvert.DeserializeObject<object>(json);
			}
			catch
			{
			}
			if (!((val != null) ? true : false))
			{
				return;
			}
			string text = Convert.ToString(val.sessionId.Value);
			if (text == _sessionId)
			{
				string text2 = Convert.ToString(val.message.Value);
				if (text2.IndexOf("Current Phone Number") >= 0)
				{
					_currentNumber = text2.Split(new string[1] { "- Current Phone Number:" }, StringSplitOptions.None)[1];
				}
				else
				{
					OnStartup(text2, text);
				}
			}
		}

		private void OnScanMeEventHandler(string json)
		{
			if (string.IsNullOrEmpty(json) || OnScanMe == null)
			{
				return;
			}
			dynamic val = null;
			try
			{
				val = JsonConvert.DeserializeObject<object>(json);
			}
			catch
			{
			}
			if (val != null)
			{
				string text = Convert.ToString(val.sessionId.Value);
				if (text == _sessionId)
				{
					OnScanMe.Invoke(val.qrcodePath.Value, text);
				}
			}
		}

		private void OnChangeStateEventHandler(string json)
		{
			if (string.IsNullOrEmpty(json) || OnChangeState == null)
			{
				return;
			}
			dynamic val = null;
			try
			{
				val = JsonConvert.DeserializeObject<object>(json);
			}
			catch
			{
			}
			if (!((val != null) ? true : false))
			{
				return;
			}
			string text = Convert.ToString(val.sessionId.Value);
			if (text == _sessionId)
			{
				WAState wAState = WAState.OPEN;
				switch (((string)Convert.ToString(val.state.Value)).ToUpper())
				{
				case "OPEN":
					wAState = WAState.OPEN;
					break;
				case "CONNECTING":
					wAState = WAState.CONNECTING;
					break;
				case "CLOSE":
					wAState = WAState.CLOSE;
					break;
				default:
					wAState = WAState.UNKNOWN;
					break;
				}
				OnChangeState(wAState, text);
			}
		}

		private void OnChangeBatteryEventHandler(string json)
		{
			if (string.IsNullOrEmpty(json) || OnChangeBattery == null)
			{
				return;
			}
			dynamic val = null;
			try
			{
				val = JsonConvert.DeserializeObject<object>(json);
			}
			catch
			{
			}
			if (val != null)
			{
				string text = Convert.ToString(val.sessionId.Value);
				if (text == _sessionId)
				{
					BatteryStatus batteryStatus = new BatteryStatus();
					batteryStatus.battery = Convert.ToInt32(val.battery.Value);
					batteryStatus.plugged = Convert.ToBoolean(val.plugged.Value);
					BatteryStatus status = batteryStatus;
					OnChangeBattery(status, text);
				}
			}
		}

		private void OnGroupJoinEventHandler(string json)
		{
			if (string.IsNullOrEmpty(json) || OnGroupJoin == null)
			{
				return;
			}
			JsonMessage jsonMessage = null;
			try
			{
				jsonMessage = JsonConvert.DeserializeObject<JsonMessage>(json);
			}
			catch
			{
			}
			if (jsonMessage == null || !(jsonMessage.sessionId == _sessionId))
			{
				return;
			}
			GroupNotification groupNotification = jsonMessage.groupNotification;
			if (groupNotification == null)
			{
				return;
			}
			foreach (Recipient recipient in groupNotification.recipients)
			{
				recipient.id = recipient.id.Split('@')[0];
			}
			OnGroupJoin(groupNotification, jsonMessage.sessionId);
		}

		private void OnGroupLeaveEventHandler(string json)
		{
			if (string.IsNullOrEmpty(json) || OnGroupLeave == null)
			{
				return;
			}
			JsonMessage jsonMessage = null;
			try
			{
				jsonMessage = JsonConvert.DeserializeObject<JsonMessage>(json);
			}
			catch
			{
			}
			if (jsonMessage == null || !(jsonMessage.sessionId == _sessionId))
			{
				return;
			}
			GroupNotification groupNotification = jsonMessage.groupNotification;
			if (groupNotification == null)
			{
				return;
			}
			foreach (Recipient recipient in groupNotification.recipients)
			{
				recipient.id = recipient.id.Split('@')[0];
			}
			OnGroupLeave(groupNotification, jsonMessage.sessionId);
		}

		private void CreatedGroupStatusHandler(string json)
		{
			if (string.IsNullOrEmpty(json) || OnCreatedGroupStatus == null)
			{
				return;
			}
			JsonMessage jsonMessage = null;
			try
			{
				jsonMessage = JsonConvert.DeserializeObject<JsonMessage>(json);
			}
			catch
			{
			}
			if (jsonMessage != null && jsonMessage.sessionId == _sessionId)
			{
				GroupStatus groupStatus = jsonMessage.groupStatus;
				if (groupStatus != null)
				{
					groupStatus.id = groupStatus.id.Split('@')[0];
					OnCreatedGroupStatus(groupStatus, jsonMessage.sessionId);
				}
			}
		}

		private void OnMessageAckEventHandler(string json)
		{
			if (string.IsNullOrEmpty(json) || OnMessageAck == null)
			{
				return;
			}
			dynamic val = null;
			try
			{
				val = JsonConvert.DeserializeObject<object>(json);
			}
			catch
			{
			}
			if (!((val != null) ? true : false))
			{
				return;
			}
			string text = Convert.ToString(val.sessionId.Value);
			if (text == _sessionId)
			{
				Message message = null;
				try
				{
					message = JsonConvert.DeserializeObject<Message>(Convert.ToString(val.message.Value));
				}
				catch
				{
				}
				if (message != null)
				{
					message.to = message.to.Split('@')[0];
					message.from = message.from.Split('@')[0];
					OnMessageAck(message, text);
				}
			}
		}

		private void OnReceiveMessageEventHandler(string json)
		{
			if (string.IsNullOrEmpty(json) || OnReceiveMessage == null)
			{
				return;
			}
			JsonMessage jsonMessage = null;
			try
			{
				jsonMessage = JsonConvert.DeserializeObject<JsonMessage>(json);
			}
			catch
			{
			}
			if (jsonMessage == null || !(jsonMessage.sessionId == _sessionId))
			{
				return;
			}
			Message message = jsonMessage.message;
			if (message != null)
			{
				message.to = message.to.Split('@')[0];
				if (message.group != null)
				{
					message.group.sender.id = message.group.sender.id.Split('@')[0];
				}
				else
				{
					message.from = message.from.Split('@')[0];
					message.sender.id = message.sender.id.Split('@')[0];
				}
				OnReceiveMessage(message, jsonMessage.sessionId);
			}
		}

		private void OnUnreadMessageEventHandler(string json)
		{
			if (string.IsNullOrEmpty(json) || OnUnreadMessage == null)
			{
				return;
			}
			JsonMessage jsonMessage = null;
			try
			{
				jsonMessage = JsonConvert.DeserializeObject<JsonMessage>(json);
			}
			catch
			{
			}
			if (jsonMessage == null || !(jsonMessage.sessionId == _sessionId))
			{
				return;
			}
			Message message = jsonMessage.message;
			if (message != null)
			{
				message.to = message.to.Split('@')[0];
				if (message.group != null)
				{
					message.group.sender.id = message.group.sender.id.Split('@')[0];
				}
				else
				{
					message.from = message.from.Split('@')[0];
					message.sender.id = message.sender.id.Split('@')[0];
				}
				OnUnreadMessage(message, jsonMessage.sessionId);
			}
		}

		private void OnReceiveMessagesEventHandler(string json)
		{
			if (string.IsNullOrEmpty(json) || OnReceiveMessages == null)
			{
				return;
			}
			JsonMessage jsonMessage = null;
			try
			{
				jsonMessage = JsonConvert.DeserializeObject<JsonMessage>(json);
			}
			catch
			{
			}
			if (jsonMessage == null || !(jsonMessage.sessionId == _sessionId))
			{
				return;
			}
			IList<Message> messages = jsonMessage.messages;
			if (messages.Count <= 0)
			{
				return;
			}
			foreach (Message item in messages)
			{
				if (!(item.id == "status@broadcast"))
				{
					item.to = item.to.Split('@')[0];
					if (item.group != null)
					{
						item.group.sender.id = item.group.sender.id.Split('@')[0];
						continue;
					}
					item.from = item.from.Split('@')[0];
					item.sender.id = item.sender.id.Split('@')[0];
				}
			}
			OnReceiveMessages(messages, jsonMessage.sessionId);
		}

		private void OnReceiveContactsEventHandler(string json)
		{
			if (string.IsNullOrEmpty(json) || OnReceiveContacts == null)
			{
				return;
			}
			JsonMessage jsonMessage = null;
			try
			{
				jsonMessage = JsonConvert.DeserializeObject<JsonMessage>(json);
			}
			catch
			{
			}
			if (jsonMessage == null || !(jsonMessage.sessionId == _sessionId))
			{
				return;
			}
			IList<Contact> contacts = jsonMessage.contacts;
			if (contacts.Count <= 0)
			{
				return;
			}
			foreach (Contact item in contacts)
			{
				if (!(item.id == "status@broadcast"))
				{
					item.id = item.id.Split('@')[0];
				}
				if (string.IsNullOrEmpty(item.name))
				{
					item.name = item.id;
				}
			}
			OnReceiveContacts(contacts, jsonMessage.sessionId);
		}

		private void OnReceiveBusinessProfilesEventHandler(string json)
		{
			if (string.IsNullOrEmpty(json) || OnReceiveBusinessProfiles == null)
			{
				return;
			}
			JsonMessage jsonMessage = null;
			try
			{
				jsonMessage = JsonConvert.DeserializeObject<JsonMessage>(json);
			}
			catch
			{
			}
			if (jsonMessage == null || !(jsonMessage.sessionId == _sessionId))
			{
				return;
			}
			IList<BusinessProfile> businessProfiles = jsonMessage.businessProfiles;
			if (businessProfiles.Count <= 0)
			{
				return;
			}
			foreach (BusinessProfile item in businessProfiles)
			{
				if (!(item.id == "status@broadcast"))
				{
					item.id = item.id.Split('@')[0];
				}
			}
			OnReceiveBusinessProfiles(businessProfiles, jsonMessage.sessionId);
		}

		private void OnReceiveGroupsEventHandler(string json)
		{
			if (string.IsNullOrEmpty(json) || OnReceiveGroups == null)
			{
				return;
			}
			JsonMessage jsonMessage = null;
			try
			{
				jsonMessage = JsonConvert.DeserializeObject<JsonMessage>(json);
			}
			catch
			{
			}
			if (jsonMessage == null || !(jsonMessage.sessionId == _sessionId))
			{
				return;
			}
			IList<Group> groups = jsonMessage.groups;
			if (groups.Count <= 0)
			{
				return;
			}
			foreach (Group item in groups)
			{
				foreach (Contact member in item.members)
				{
					member.id = member.id.Split('@')[0];
					if (string.IsNullOrEmpty(member.name))
					{
						member.name = member.id;
					}
				}
			}
			OnReceiveGroups(groups, jsonMessage.sessionId);
		}

		private void OnReceiveMessageStatusEventHandler(string json)
		{
			if (string.IsNullOrEmpty(json) || OnReceiveMessageStatus == null)
			{
				return;
			}
			JsonMessage jsonMessage = null;
			try
			{
				jsonMessage = JsonConvert.DeserializeObject<JsonMessage>(json);
			}
			catch
			{
			}
			if (jsonMessage != null && jsonMessage.sessionId == _sessionId)
			{
				MessageStatus messageStatus = jsonMessage.messageStatus;
				if (messageStatus != null)
				{
					messageStatus.send_to = messageStatus.send_to.Split('@')[0];
					OnReceiveMessageStatus(messageStatus, jsonMessage.sessionId);
				}
			}
		}

		private void WriteToConsole(string s)
		{
		}
	}
}
