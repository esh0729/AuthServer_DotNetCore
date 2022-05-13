using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

using WebCommon;

namespace AuthServer
{
	[Table("r_Continent")]
	public class Continent
	{
		[Key]
		public int continentId { get; set; }
		[Required]
		public string sceneName { get; set; } = string.Empty;
		[Required]
		public double x { get; set; }
		[Required]
		public double y { get; set; }
		[Required]
		public double z { get; set; }
		[Required]
		public double xSize { get; set; }
		[Required]
		public double ySize { get; set; }
		[Required]
		public double zSize { get; set; }

		//
		//
		//

		public PDContinent ToPDContinent()
		{
			PDContinent continent = new PDContinent();
			continent.continentId = continentId;
			continent.sceneName = sceneName;
			continent.x = Convert.ToSingle(x);
			continent.y = Convert.ToSingle(y);
			continent.z = Convert.ToSingle(z);
			continent.xSize = Convert.ToSingle(xSize);
			continent.ySize = Convert.ToSingle(ySize);
			continent.zSize = Convert.ToSingle(zSize);

			return continent;
		}
	}
}
