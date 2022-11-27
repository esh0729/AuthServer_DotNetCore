using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AuthServer
{
	[Keyless]
	[Table("s_Systemsetting")]
	public class SystemSetting
	{
		[Required]
		public int metaDataVersion { get; set; }
	}
}
