using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;

namespace WhatsAppNETAPI
{
	public class WhatsAppNETAPI : IDisposable, IWhatsAppNETAPI
	{
		private bool _isConnected;

		private ISignalRServer _signalRServer;

		private ISignalRClient _signalRClient;

		private string _port = "8282";

		private string _sessionId = "lsgWFcmtxHcMWfnlcsjalYRldNtLVkHxqjQd";

		// private string _proxy = "socks5|gate4.rola.info|2198|18211180301_static_1|kjw2023";
		private string _proxy = "";
        //private string _proxy = "http|gate4.rola.info|1198|18211180301_static_5|kjw2023";

        private string _jsonSessionId = string.Empty;

		public string WaNetApiNodeJsPath { get; set; }

		public string CountryCode { get; set; } = "62";


		public BrowserInfo BrowserInfo { get; set; }

		public string ChromePath { get; set; }

		public string ImageAndDocumentPath { get; set; }

		public bool CreateLog { get; set; }

		public bool IsMultiDevice { get; set; }

		public bool Headless { get; set; } = true;


		public string GetCurrentNumber
		{
			get
			{
				if (_signalRClient != null)
				{
					return _signalRClient.CurrentNumber.Trim();
				}
				return string.Empty;
			}
		}

		public bool IsConnected => _isConnected;

		public bool IsWaNetApiNodeJsPathExists => File.Exists($"{WaNetApiNodeJsPath}\\src\\index.js");

		public event OnConnectedEventHandler OnClientConnected;

		public event OnMonitoringLogEventHandler OnMonitoringLog;

		private event OnReceiveMessageEventHandler _onReceiveMessage;

		private event OnReceiveMessageEventHandler _onUnreadMessage;

		event OnReceiveMessageEventHandler IWhatsAppNETAPI.OnReceiveMessage
		{
			add
			{
				_onReceiveMessage += value;
				_signalRClient.OnReceiveMessage = this._onReceiveMessage;
			}
			remove
			{
				_onReceiveMessage -= value;
				_signalRClient.OnReceiveMessage = this._onReceiveMessage;
			}
		}

		event OnReceiveMessageEventHandler IWhatsAppNETAPI.OnUnreadMessage
		{
			add
			{
				_onUnreadMessage += value;
				_signalRClient.OnUnreadMessage = this._onUnreadMessage;
			}
			remove
			{
				_onUnreadMessage -= value;
				_signalRClient.OnUnreadMessage = this._onUnreadMessage;
			}
		}

		private event OnReceiveMessagesEventHandler _onReceiveMessages;

		event OnReceiveMessagesEventHandler IWhatsAppNETAPI.OnReceiveMessages
		{
			add
			{
				_onReceiveMessages += value;
				_signalRClient.OnReceiveMessages = this._onReceiveMessages;
			}
			remove
			{
				_onReceiveMessages -= value;
				_signalRClient.OnReceiveMessages = this._onReceiveMessages;
			}
		}

		private event OnReceiveMessageStatusEventHandler _onReceiveMessageStatus;

		event OnReceiveMessageStatusEventHandler IWhatsAppNETAPI.OnReceiveMessageStatus
		{
			add
			{
				_onReceiveMessageStatus += value;
				_signalRClient.OnReceiveMessageStatus = this._onReceiveMessageStatus;
			}
			remove
			{
				_onReceiveMessageStatus -= value;
				_signalRClient.OnReceiveMessageStatus = this._onReceiveMessageStatus;
			}
		}

		private event OnReceiveContactEventHandler _onReceiveContacts;

		event OnReceiveContactEventHandler IWhatsAppNETAPI.OnReceiveContacts
		{
			add
			{
				_onReceiveContacts += value;
				_signalRClient.OnReceiveContacts = this._onReceiveContacts;
			}
			remove
			{
				_onReceiveContacts -= value;
				_signalRClient.OnReceiveContacts = this._onReceiveContacts;
			}
		}

		private event OnReceiveBusinessProfileEventHandler _onReceiveBusinessProfiles;

		event OnReceiveBusinessProfileEventHandler IWhatsAppNETAPI.OnReceiveBusinessProfiles
		{
			add
			{
				_onReceiveBusinessProfiles += value;
				_signalRClient.OnReceiveBusinessProfiles = this._onReceiveBusinessProfiles;
			}
			remove
			{
				_onReceiveBusinessProfiles -= value;
				_signalRClient.OnReceiveBusinessProfiles = this._onReceiveBusinessProfiles;
			}
		}

		private event OnReceiveGroupEventHandler _onReceiveGroups;

		event OnReceiveGroupEventHandler IWhatsAppNETAPI.OnReceiveGroups
		{
			add
			{
				_onReceiveGroups += value;
				_signalRClient.OnReceiveGroups = this._onReceiveGroups;
			}
			remove
			{
				_onReceiveGroups -= value;
				_signalRClient.OnReceiveGroups = this._onReceiveGroups;
			}
		}

		private event OnScanMeEventHandler _onScanMe;

		event OnScanMeEventHandler IWhatsAppNETAPI.OnScanMe
		{
			add
			{
				_onScanMe += value;
				_signalRClient.OnScanMe = this._onScanMe;
			}
			remove
			{
				_onScanMe -= value;
				_signalRClient.OnScanMe = this._onScanMe;
			}
		}

		private event OnStartupEventHandler _onStartup;

		event OnStartupEventHandler IWhatsAppNETAPI.OnStartup
		{
			add
			{
				_onStartup += value;
				_signalRClient.OnStartup = this._onStartup;
			}
			remove
			{
				_onStartup -= value;
				_signalRClient.OnStartup = this._onStartup;
			}
		}

		private event OnChangeStateEventHandler _onChangeState;

		event OnChangeStateEventHandler IWhatsAppNETAPI.OnChangeState
		{
			add
			{
				_onChangeState += value;
				_signalRClient.OnChangeState = this._onChangeState;
			}
			remove
			{
				_onChangeState -= value;
				_signalRClient.OnChangeState = this._onChangeState;
			}
		}

		private event OnChangeBatteryEventHandler _onChangeBattery;

		event OnChangeBatteryEventHandler IWhatsAppNETAPI.OnChangeBattery
		{
			add
			{
				_onChangeBattery += value;
				_signalRClient.OnChangeBattery = this._onChangeBattery;
			}
			remove
			{
				_onChangeBattery -= value;
				_signalRClient.OnChangeBattery = this._onChangeBattery;
			}
		}

		private event OnGroupJoinLeaveEventHandler _onGroupJoin;

		event OnGroupJoinLeaveEventHandler IWhatsAppNETAPI.OnGroupJoin
		{
			add
			{
				_onGroupJoin += value;
				_signalRClient.OnGroupJoin = this._onGroupJoin;
			}
			remove
			{
				_onGroupJoin -= value;
				_signalRClient.OnGroupJoin = this._onGroupJoin;
			}
		}

		private event OnGroupJoinLeaveEventHandler _onGroupLeave;

		event OnGroupJoinLeaveEventHandler IWhatsAppNETAPI.OnGroupLeave
		{
			add
			{
				_onGroupLeave += value;
				_signalRClient.OnGroupLeave = this._onGroupLeave;
			}
			remove
			{
				_onGroupLeave -= value;
				_signalRClient.OnGroupLeave = this._onGroupLeave;
			}
		}

		private event OnCreatedGroupStatusEventHandler _onCreatedGroupStatus;

		event OnCreatedGroupStatusEventHandler IWhatsAppNETAPI.OnCreatedGroupStatus
		{
			add
			{
				_onCreatedGroupStatus += value;
				_signalRClient.OnCreatedGroupStatus = this._onCreatedGroupStatus;
			}
			remove
			{
				_onCreatedGroupStatus -= value;
				_signalRClient.OnCreatedGroupStatus = this._onCreatedGroupStatus;
			}
		}

		private event OnMessageAckEventHandler _onMessageAck;

		event OnMessageAckEventHandler IWhatsAppNETAPI.OnMessageAck
		{
			add
			{
				_onMessageAck += value;
				_signalRClient.OnMessageAck = this._onMessageAck;
			}
			remove
			{
				_onMessageAck -= value;
				_signalRClient.OnMessageAck = this._onMessageAck;
			}
		}

		public WhatsAppNETAPI()
		{
			_signalRServer = new SignalRServer();
			_signalRServer.Connect();
			_signalRClient = new SignalRClient();
			_jsonSessionId = JsonConvert.SerializeObject(new
			{
				sessionId = _sessionId
			});
		}

		public WhatsAppNETAPI(string port, string sessionId,string proxy="")
		{
			_port = port;
			_sessionId = sessionId;
			_proxy = proxy;
			_signalRServer = new SignalRServer(port);
			_signalRServer.Connect();
			_signalRClient = new SignalRClient(_port, _sessionId);
			_jsonSessionId = JsonConvert.SerializeObject(new
			{
				sessionId = _sessionId
			});
		}

		public void Connect()
		{
			OnClientConnected -= OnClientConnectedEventHandler;
			OnClientConnected += OnClientConnectedEventHandler;
			OnMonitoringLog -= OnMonitoringLogEventHandler;
			OnMonitoringLog += OnMonitoringLogEventHandler;
			_signalRClient.OnClientConnected = this.OnClientConnected;
			_signalRClient.OnStartup = this._onStartup;
			_signalRClient.OnChangeState = this._onChangeState;
			_signalRClient.OnScanMe = this._onScanMe;
			_signalRClient.OnReceiveMessage = this._onReceiveMessage;
			_signalRClient.OnUnreadMessage = this._onUnreadMessage;
			_signalRClient.OnReceiveMessages = this._onReceiveMessages;
			_signalRClient.OnReceiveMessageStatus = this._onReceiveMessageStatus;
			_signalRClient.OnReceiveContacts = this._onReceiveContacts;
			_signalRClient.OnReceiveGroups = this._onReceiveGroups;
			_signalRClient.Connect();
			_isConnected = true;
		}

		private void UnSubscribeEvent()
		{
			_signalRClient.OnStartup = null;
			_signalRClient.OnChangeState = null;
			_signalRClient.OnScanMe = null;
			_signalRClient.OnReceiveMessage = null;
			_signalRClient.OnUnreadMessage = null;
			_signalRClient.OnReceiveMessages = null;
			_signalRClient.OnReceiveMessageStatus = null;
			_signalRClient.OnReceiveContacts = null;
			_signalRClient.OnReceiveGroups = null;
			_signalRClient.OnClientConnected = null;
			_signalRClient.OnGroupJoin = null;
			_signalRClient.OnGroupLeave = null;
		}

		public void Disconnect()
		{
			UnSubscribeEvent();
			_signalRClient.Invoke("Disconnect", _jsonSessionId);
			_signalRClient.Disconnect();
			_isConnected = false;
		}

		public void Logout()
		{
			UnSubscribeEvent();
			_signalRClient.Invoke("Logout", _jsonSessionId);
			_signalRClient.Disconnect();
			_isConnected = false;
		}

		public void SendMessage(MsgArgs message)
		{
			message.sessionId = _sessionId;
			message.send_to = FormatNoHP(message.send_to);
			if (message.mentions != null)
			{
				List<string> list = new List<string>();
				string[] mentions = message.mentions;
				foreach (string noHP in mentions)
				{
					list.Add(FormatNoHP(noHP));
				}
				message.mentions = list.ToArray();
			}
			string text = JsonConvert.SerializeObject(message);
			_signalRClient.Invoke("SendMessage", text);
		}

		public void SetStatus(StatusArgs status)
		{
			status.sessionId = _sessionId;
		}

		public void SetOnlineStatus(bool state)
		{
			string text = JsonConvert.SerializeObject(new
			{
				sessionId = _sessionId,
				state = state
			});
			_signalRClient.Invoke("SetOnlineStatus", text);
		}

		public void ReplyMessage(ReplyMsgArgs message)
		{
			message.sessionId = _sessionId;
			message.send_to = FormatNoHP(message.send_to);
			string text = JsonConvert.SerializeObject(message);
			_signalRClient.Invoke("SendMessage", text);
		}

		public void BroadcastMessage(IList<MsgArgs> messages, int delayInMillisecond = 500)
		{
			foreach (MsgArgs message in messages)
			{
				message.sessionId = _sessionId;
				message.send_to = FormatNoHP(message.send_to);
				if (message.mentions != null)
				{
					List<string> list = new List<string>();
					string[] mentions = message.mentions;
					foreach (string noHP in mentions)
					{
						list.Add(FormatNoHP(noHP));
					}
					message.mentions = list.ToArray();
				}
			}
			string text = JsonConvert.SerializeObject(messages);
			_signalRClient.Invoke("BroadcastMessage", text, delayInMillisecond);
		}

		public void CreateGroup(Group newGroup)
		{
			foreach (Contact member in newGroup.members)
			{
				member.id = FormatNoHP(member.id);
			}
			string text = JsonConvert.SerializeObject(new
			{
				sessionId = _sessionId,
				newGroup = newGroup
			});
			_signalRClient.Invoke("CreateGroup", text);
		}

		public void AddRemoveGroupMember(Group group, string action)
		{
			if (!group.id.EndsWith("@g.us"))
			{
				group.id += "@g.us";
			}
			foreach (Contact member in group.members)
			{
				member.id = FormatNoHP(member.id);
			}
			string text = JsonConvert.SerializeObject(new
			{
				sessionId = _sessionId,
				group = group,
				action = action
			});
			_signalRClient.Invoke("AddRemoveGroupMember", text);
		}

		public void VerifyWANumber(IList<string> contacts, bool checkProfile = false)
		{
			throw new NotImplementedException();
		}

		public void VerifyWANumber(IList<string> contacts)
		{
			List<VerifyContact> list = new List<VerifyContact>();
			foreach (string contact in contacts)
			{
				list.Add(new VerifyContact
				{
					phoneNumber = FormatNoHP(contact),
					sessionId = _sessionId
				});
			}
			string text = JsonConvert.SerializeObject(list);
			_signalRClient.Invoke("VerifyWANumber", text, false);
		}

		public void GetBusinessProfile(IList<string> contacts)
		{
			List<BusinessProfileContact> list = new List<BusinessProfileContact>();
			foreach (string contact in contacts)
			{
				list.Add(new BusinessProfileContact
				{
					phoneNumber = FormatNoHP(contact),
					sessionId = _sessionId
				});
			}
			string text = JsonConvert.SerializeObject(list);
			_signalRClient.Invoke("GetBusinessProfile", text);
		}

		public void GetContacts()
		{
			_signalRClient.Invoke("GrabContacts", _jsonSessionId);
		}

		public void GetGroups(bool includeMembers = true)
		{
			if (includeMembers)
			{
				_signalRClient.Invoke("GrabGroupAndMember", _jsonSessionId);
			}
			else
			{
				_signalRClient.Invoke("GrabGroups", _jsonSessionId);
			}
		}

		public void GetGroups(string groupId, bool includeMembers = true)
		{
			string text = JsonConvert.SerializeObject(new
			{
				sessionId = _sessionId,
				groupId = groupId
			});
			if (includeMembers)
			{
				_signalRClient.Invoke("GrabGroupAndMember", text);
			}
			else
			{
				_signalRClient.Invoke("GrabGroups", text);
			}
		}

		public void ArchiveChat()
		{
			_signalRClient.Invoke("ArchiveChat", _jsonSessionId);
		}

		public void ArchiveChat(string phoneNumber)
		{
			phoneNumber = FormatNoHP(phoneNumber);
			string text = JsonConvert.SerializeObject(new
			{
				sessionId = _sessionId,
				phoneNumber = phoneNumber
			});
			_signalRClient.Invoke("ArchiveChat", text);
		}

		public void MarkRead(string phoneNumber)
		{
			phoneNumber = FormatNoHP(phoneNumber);
			string text = JsonConvert.SerializeObject(new
			{
				sessionId = _sessionId,
				phoneNumber = phoneNumber
			});
			_signalRClient.Invoke("MarkRead", text);
		}

		public void DeleteMessage(string groupId, string participant)
		{
			participant = FormatNoHP(participant);
			string text = JsonConvert.SerializeObject(new
			{
				sessionId = _sessionId,
				groupId = groupId,
				participant = participant
			});
			_signalRClient.Invoke("DeleteMessage", text);
		}

		public void DeleteChat()
		{
			_signalRClient.Invoke("DeleteChat", _jsonSessionId);
		}

		public void DeleteChat(string phoneNumber)
		{
			phoneNumber = FormatNoHP(phoneNumber);
			string text = JsonConvert.SerializeObject(new
			{
				sessionId = _sessionId,
				phoneNumber = phoneNumber
			});
			_signalRClient.Invoke("DeleteChat", text);
		}

		public void GetBatteryStatus()
		{
			_signalRClient.Invoke("GetBatteryStatus", _jsonSessionId);
		}

		public void GetCurrentState()
		{
			_signalRClient.Invoke("GetCurrentState", _jsonSessionId);
		}

		public void GetUnreadMessage()
		{
		}

		public void GetAllMessage(string phoneNumber, int limit = 50)
		{
			phoneNumber = FormatNoHP(phoneNumber);
			string text = JsonConvert.SerializeObject(new
			{
				sessionId = _sessionId,
				phoneNumber = phoneNumber,
				limit = limit
			});
			_signalRClient.Invoke("GetAllMessage", text);
		}

		private string FormatNoHP(string noHP)
		{
			string result = noHP;
			try
			{
				if (noHP.EndsWith("@broadcast"))
				{
					return string.Empty;
				}
				if (!noHP.EndsWith("@g.us"))
				{
					if (string.IsNullOrEmpty(CountryCode))
					{
						CountryCode = "62";
					}
					noHP = ((noHP.Substring(0, 1) == "0") ? $"{CountryCode}{noHP.Substring(1, noHP.Length - 1)}@s.whatsapp.net" : ((!(noHP.Substring(0, CountryCode.Length + 1) == "+" + CountryCode)) ? $"{noHP}@s.whatsapp.net" : $"{noHP.Substring(1, noHP.Length - 1)}@s.whatsapp.net"));
				}
				result = noHP;
				return result;
			}
			catch
			{
				return result;
			}
		}

		private void OnMonitoringLogEventHandler(string level, string message, string sessionId)
		{
		}

		private void OnClientConnectedEventHandler(string sessionId)
		{
			try
			{
				if (WaNetApiNodeJsPath.Substring(WaNetApiNodeJsPath.Length - 1) != "\\")
				{
					WaNetApiNodeJsPath += "\\";
				}
				if (!Directory.Exists(WaNetApiNodeJsPath + "session"))
				{
					Directory.CreateDirectory(WaNetApiNodeJsPath + "session");
				}
				ProcessStartInfo processStartInfo = new ProcessStartInfo();
				processStartInfo.WorkingDirectory = WaNetApiNodeJsPath;
				processStartInfo.FileName = "cmd.exe";
				processStartInfo.Arguments = "/c node ./src/index.js";
				processStartInfo.CreateNoWindow = true;
				processStartInfo.RedirectStandardOutput = true;
				processStartInfo.RedirectStandardError = true;
				processStartInfo.UseShellExecute = false;
				if (!string.IsNullOrEmpty(ImageAndDocumentPath))
				{
					processStartInfo.EnvironmentVariables.Add("IMAGE_AND_DOCUMENT_PATH", ImageAndDocumentPath);
				}
				processStartInfo.EnvironmentVariables.Add("SESSION_ID", _sessionId);
				processStartInfo.EnvironmentVariables.Add("PORT", _port);
				if (BrowserInfo == null)
				{
					processStartInfo.EnvironmentVariables.Add("AGENT_NAME", "Google Chrome (Windows)");
					processStartInfo.EnvironmentVariables.Add("OS_NAME", "Windows");
				}
				else
				{
					processStartInfo.EnvironmentVariables.Add("AGENT_NAME", BrowserInfo.AgentName);
					processStartInfo.EnvironmentVariables.Add("OS_NAME", BrowserInfo.OS);
				}
				processStartInfo.EnvironmentVariables.Add("Agent", _proxy);
                Process process = new Process();
				process.StartInfo = processStartInfo;
				process.ErrorDataReceived -= ErrorDataReceivedHandler;
				process.ErrorDataReceived += ErrorDataReceivedHandler;
				process.OutputDataReceived -= OutputDataReceivedHandler;
				process.OutputDataReceived += OutputDataReceivedHandler;
				process.Start();
				process.BeginErrorReadLine();
				process.BeginOutputReadLine();
			}
			catch (Exception ex)
			{
				if (!string.IsNullOrEmpty(ex.Message))
				{
					if (this.OnMonitoringLog != null)
					{
						this.OnMonitoringLog("ERROR", ex.Message, _sessionId);
					}
					if (CreateLog)
					{
						LogWriter.LogWrite($"ERROR: {ex.Message}");
					}
				}
			}
		}

		private void OutputDataReceivedHandler(object sender, DataReceivedEventArgs e)
		{
			if (!string.IsNullOrEmpty(e.Data))
			{
				string text = "INFO";
				if (e.Data.IndexOf("Session closed") >= 0 || e.Data.IndexOf("puppeteer") >= 0 || e.Data.IndexOf(".js:") >= 0)
				{
					text = "ERROR";
				}
				if (this.OnMonitoringLog != null)
				{
					this.OnMonitoringLog(text, e.Data, _sessionId);
				}
				if (CreateLog)
				{
					LogWriter.LogWrite($"{text}: {e.Data}");
				}
			}
		}

		private void ErrorDataReceivedHandler(object sender, DataReceivedEventArgs e)
		{
			if (!string.IsNullOrEmpty(e.Data))
			{
				if (this.OnMonitoringLog != null)
				{
					this.OnMonitoringLog("ERROR", e.Data, _sessionId);
				}
				if (CreateLog)
				{
					LogWriter.LogWrite($"ERROR: {e.Data}");
				}
			}
		}

		public void Dispose()
		{
			Disconnect();
			_signalRServer.Disconnect();
		}
	}
}
