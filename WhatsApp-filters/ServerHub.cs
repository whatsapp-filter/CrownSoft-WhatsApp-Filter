using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace WhatsAppNETAPI
{
	public class ServerHub : Hub
	{
		public void Startup(string json)
		{
			base.Clients.All.OnStartup(json);
		}

		public void ChangeState(string json)
		{
			base.Clients.All.OnChangeState(json);
		}

		public void ChangeBattery(string json)
		{
			base.Clients.All.OnChangeBattery(json);
		}

		public void MessageAck(string json)
		{
			base.Clients.All.OnMessageAck(json);
		}

		public void ScanMe(string json)
		{
			base.Clients.All.OnScanMe(json);
		}

		public void GroupJoin(string json)
		{
			base.Clients.All.OnGroupJoin(json);
		}

		public void GroupLeave(string json)
		{
			base.Clients.All.OnGroupLeave(json);
		}

		public void CreatedGroupStatus(string json)
		{
			base.Clients.All.OnCreatedGroupStatus(json);
		}

		public void ReceiveMessage(string json)
		{
			base.Clients.All.OnReceiveMessage(json);
		}

		public void UnreadMessage(string json)
		{
			base.Clients.All.OnUnreadMessage(json);
		}

		public void SendMessage(string json)
		{
			base.Clients.All.OnSendMessage(json);
		}

		public void SetStatus(string json)
		{
			base.Clients.All.OnSetStatus(json);
		}

		public void SetOnlineStatus(string json)
		{
			base.Clients.All.OnSetOnlineStatus(json);
		}

		public void BroadcastMessage(string json, int delayInMillisecond = 500)
		{
			base.Clients.All.OnBroadcastMessage(json, delayInMillisecond);
		}

		public void CreateGroup(string json)
		{
			base.Clients.All.OnGroupCreate(json);
		}

		public void AddRemoveGroupMember(string json)
		{
			base.Clients.All.OnAddRemoveGroupMember(json);
		}

		public void VerifyWANumber(string json, bool checkProfile = false)
		{
			base.Clients.All.OnVerifyWANumber(json, checkProfile);
		}

		public void GetBusinessProfile(string json)
		{
			base.Clients.All.OnGetBusinessProfile(json);
		}

		public void SendMessageStatus(string json)
		{
			base.Clients.All.OnReceiveMessageStatus(json);
		}

		public void GrabContacts(string json)
		{
			base.Clients.All.OnGrabContact(json);
		}

		public void GrabGroups(string json)
		{
			base.Clients.All.OnGrabGroup(json);
		}

		public void GrabGroupAndMember(string json)
		{
			base.Clients.All.OnGrabGroupAndMember(json);
		}

		public void ReceiveContacts(string json)
		{
			base.Clients.All.OnReceiveContacts(json);
		}

		public void ReceiveBusinessProfiles(string json)
		{
			base.Clients.All.OnReceiveBusinessProfiles(json);
		}

		public void ReceiveGroups(string json)
		{
			base.Clients.All.OnReceiveGroups(json);
		}

		public void ArchiveChat(string json)
		{
			base.Clients.All.OnArchiveChat(json);
		}

		public void MarkRead(string json)
		{
			base.Clients.All.OnMarkRead(json);
		}

		public void DeleteMessage(string json)
		{
			base.Clients.All.OnDeleteMessage(json);
		}

		public void DeleteChat(string json)
		{
			base.Clients.All.OnDeleteChat(json);
		}

		public void GetBatteryStatus(string json)
		{
			base.Clients.All.OnGetBatteryStatus(json);
		}

		public void GetCurrentState(string json)
		{
			base.Clients.All.OnGetCurrentState(json);
		}

		public void GetUnreadMessage(string json)
		{
			base.Clients.All.OnGetUnreadMessage(json);
		}

		public void GetAllMessage(string json)
		{
			base.Clients.All.OnGetAllMessage(json);
		}

		public void ReceiveMessages(string json)
		{
			base.Clients.All.OnReceiveMessages(json);
		}

		public void Disconnect(string json)
		{
			base.Clients.All.OnDisconnect(json);
		}

		public void Logout(string json)
		{
			base.Clients.All.OnLogout(json);
		}

		public override Task OnConnected()
		{
			return base.OnConnected();
		}

		public override Task OnDisconnected(bool stopCalled)
		{
			return base.OnDisconnected(stopCalled);
		}
	}
}
