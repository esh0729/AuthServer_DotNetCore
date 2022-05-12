using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AuthServer
{
	[Table("t_GuestUser")]
	public class GuestUser
	{
		[Key]
		public Guid guestId { get; set; }
		[Required]
		public Guid userId { get; set; }
	}
}
