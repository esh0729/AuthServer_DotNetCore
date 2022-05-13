namespace AuthServer
{
	public class ResponseTemplate
	{
		public int returnCode { get; set; }
		public string error { get; set; } = string.Empty;
		public string response { get; set; } = string.Empty;
	}
}
