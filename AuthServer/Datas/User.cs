using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AuthServer
{
	[Table("t_User")]
	public class User
	{
		[Key]
		public Guid userId { get; set; }
		[Required]
		public string accessSecret { get; set; }
		public int lastJoinedServer { get; set; }
		public string lastJoinedIPAddress { get; set; }
		public DateTimeOffset regTime { get; set; }
	}
}
