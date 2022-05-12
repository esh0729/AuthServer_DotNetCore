using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AuthServer
{
	[Table("s_Systemsetting")]
	public class SystemSetting
	{
		[Required]
		public int metadataVersion { get; set; }
	}
}
