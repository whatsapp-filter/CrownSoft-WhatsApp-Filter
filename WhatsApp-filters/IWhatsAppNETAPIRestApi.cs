namespace WhatsAppNETAPI
{
	public interface IWhatsAppNETAPIRestApi
	{
		void Start(string url, string port = "8005");

		void Stop();
	}
}
