using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AuthServer
{
	[Table("s_GameServer")]
	public class GameServer
	{
		[Key]
		public int gameServerId { get; set; }
		[Required]
		public string connectionAddress { get; set; } = string.Empty;
		[Required]
		public string heroNameRegex { get; set; } = string.Empty;
	}
}
