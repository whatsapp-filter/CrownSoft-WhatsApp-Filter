using System.Text;

namespace WhatsAppNETAPI
{
	public class VCard
	{
		public string version { get; set; }

		public string n { get; set; }

		public string fn { get; set; }

		public string org { get; set; }

		public string title { get; set; }

		public string waId { get; set; }

		public string telWork { get; set; }

		public string telHome { get; set; }

		public string mobile { get; set; }

		public string emailWork { get; set; }

		public string emailHome { get; set; }

		public string adrWork { get; set; }

		public string adrHome { get; set; }

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("BEGIN:VCARD");
			stringBuilder.AppendLine("VERSION:3.0");
			if (!string.IsNullOrEmpty(fn))
			{
				stringBuilder.Append("FN:").AppendLine(fn);
			}
			if (!string.IsNullOrEmpty(org))
			{
				stringBuilder.Append("ORG:").AppendLine(org);
			}
			if (!string.IsNullOrEmpty(title))
			{
				stringBuilder.Append("TITLE:").AppendLine(title);
			}
			if (!string.IsNullOrEmpty(telWork))
			{
				stringBuilder.Append("TEL;WORK;VOICE:").AppendLine(telWork);
			}
			if (!string.IsNullOrEmpty(mobile))
			{
				stringBuilder.AppendLine(string.Format("TEL;type=CELL;type=VOICE;waid={0}:+{0}", mobile));
			}
			stringBuilder.AppendLine("END:VCARD");
			return stringBuilder.ToString();
		}
	}
}
