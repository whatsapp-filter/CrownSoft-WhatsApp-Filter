using System;
using System.Collections.Generic;

namespace WhatsAppNETAPI
{
	public interface IWhatsAppNETAPI : IDisposable
	{
		bool CreateLog { get; set; }

		[Obsolete("Property ini sudah tidak digunakan, sebaiknya dihapus saja.", true)]
		string ChromePath { get; set; }

		string CountryCode { get; set; }

		string WaNetApiNodeJsPath { get; set; }

		string ImageAndDocumentPath { get; set; }

		string GetCurrentNumber { get; }

		bool IsWaNetApiNodeJsPathExists { get; }

		bool IsConnected { get; }

		BrowserInfo BrowserInfo { get; set; }

		[Obsolete("Property ini sudah tidak digunakan, sebaiknya dihapus saja.", true)]
		bool IsMultiDevice { get; set; }

		[Obsolete("Property ini sudah tidak digunakan, sebaiknya dihapus saja.", true)]
		bool Headless { get; set; }

		event OnStartupEventHandler OnStartup;

		event OnChangeStateEventHandler OnChangeState;

		[Obsolete("Event ini sudah tidak digunakan, sebaiknya dihapus saja.", true)]
		event OnChangeBatteryEventHandler OnChangeBattery;

		event OnScanMeEventHandler OnScanMe;

		event OnConnectedEventHandler OnClientConnected;

		event OnMonitoringLogEventHandler OnMonitoringLog;

		event OnReceiveMessageEventHandler OnReceiveMessage;

		event OnReceiveMessageEventHandler OnUnreadMessage;

		event OnReceiveMessagesEventHandler OnReceiveMessages;

		event OnReceiveMessageStatusEventHandler OnReceiveMessageStatus;

		event OnReceiveContactEventHandler OnReceiveContacts;

		event OnReceiveBusinessProfileEventHandler OnReceiveBusinessProfiles;

		event OnReceiveGroupEventHandler OnReceiveGroups;

		event OnGroupJoinLeaveEventHandler OnGroupJoin;

		event OnGroupJoinLeaveEventHandler OnGroupLeave;

		event OnCreatedGroupStatusEventHandler OnCreatedGroupStatus;

		event OnMessageAckEventHandler OnMessageAck;

		void Connect();

		void Disconnect();

		void Logout();

		void ArchiveChat();

		void ArchiveChat(string phoneNumber);

		void MarkRead(string phoneNumber);

		void DeleteMessage(string groupId, string participant);

		void DeleteChat();

		void DeleteChat(string phoneNumber);

		[Obsolete("Method ini sudah tidak disupport, silahkan dihapus saja.", true)]
		void SetStatus(StatusArgs status);

		void SetOnlineStatus(bool state);

		void SendMessage(MsgArgs message);

		void ReplyMessage(ReplyMsgArgs message);

		void BroadcastMessage(IList<MsgArgs> messages, int delayInMillisecond = 500);

		void CreateGroup(Group newGroup);

		void AddRemoveGroupMember(Group group, string action);

		[Obsolete("Method ini sudah tidak digunakan silahkan dihapus saja.", true)]
		void GetUnreadMessage();

		void GetAllMessage(string phoneNumber, int limit = 50);

		[Obsolete("Method ini sudah tidak digunakan silahkan menggunakan method VerifyWANumber dengan parameter contacts.", true)]
		void VerifyWANumber(IList<string> contacts, bool checkProfile = false);

		void VerifyWANumber(IList<string> contacts);

		void GetBusinessProfile(IList<string> contacts);

		void GetContacts();

		void GetGroups(bool includeMembers = true);

		void GetGroups(string groupId, bool includeMembers = true);

		[Obsolete("Method ini sudah tidak disupport, silahkan dihapus saja.", true)]
		void GetBatteryStatus();

		[Obsolete("Method ini sudah tidak digunakan silahkan dihapus saja.", true)]
		void GetCurrentState();
	}
}
