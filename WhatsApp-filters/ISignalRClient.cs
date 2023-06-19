using System.Threading.Tasks;

namespace WhatsAppNETAPI
{
	public interface ISignalRClient
	{
		OnStartupEventHandler OnStartup { get; set; }

		OnChangeStateEventHandler OnChangeState { get; set; }

		OnChangeBatteryEventHandler OnChangeBattery { get; set; }

		OnScanMeEventHandler OnScanMe { get; set; }

		OnConnectedEventHandler OnClientConnected { get; set; }

		OnReceiveMessageEventHandler OnReceiveMessage { get; set; }

		OnReceiveMessageEventHandler OnUnreadMessage { get; set; }

		OnReceiveMessagesEventHandler OnReceiveMessages { get; set; }

		OnReceiveContactEventHandler OnReceiveContacts { get; set; }

		OnReceiveBusinessProfileEventHandler OnReceiveBusinessProfiles { get; set; }

		OnReceiveGroupEventHandler OnReceiveGroups { get; set; }

		OnReceiveMessageStatusEventHandler OnReceiveMessageStatus { get; set; }

		OnGroupJoinLeaveEventHandler OnGroupJoin { get; set; }

		OnGroupJoinLeaveEventHandler OnGroupLeave { get; set; }

		OnCreatedGroupStatusEventHandler OnCreatedGroupStatus { get; set; }

		OnMessageAckEventHandler OnMessageAck { get; set; }

		string CurrentNumber { get; }

		void Connect();

		void Disconnect();

		Task Invoke(string method, params object[] args);
	}
}
