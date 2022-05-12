namespace AuthServer
{
	public class ResponseTemplate
	{
		public bool isSuccess { get; set; }
		public string message { get; set; } = string.Empty;
		public string response { get; set; } = string.Empty;
	}
}
