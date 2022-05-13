using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

using WebCommon;

namespace AuthServer
{
	[Keyless]
	[Table("r_GameConfig")]
	public class GameConfig
	{
		[Required]
		public int startContinentId { get; set; }
		[Required]
		public double startXPosition { get; set; }
		[Required]
		public double startYPosition { get; set; }
		[Required]
		public double startZPosition { get; set; }
		[Required]
		public double startRadius { get; set; }
		[Required]
		public int startYRotationType { get; set; }
		[Required]
		public double startYRotation { get; set; }
		[Required]
		public int heroCreationLimitCount { get; set; }

		//
		//
		//

		public PDGameConfig ToPDGameConfig()
		{
			PDGameConfig gameConfig = new PDGameConfig();
			gameConfig.startContinentId = startContinentId;
			gameConfig.startXPosition = Convert.ToSingle(startXPosition);
			gameConfig.startYPosition = Convert.ToSingle(startYPosition);
			gameConfig.startZPosition = Convert.ToSingle(startZPosition);
			gameConfig.startRadius = Convert.ToSingle(startRadius);
			gameConfig.startYRotationType = startYRotationType;
			gameConfig.startYRotation = Convert.ToSingle(startYRotation);
			gameConfig.heroCreationLimitCount = heroCreationLimitCount;

			return gameConfig;
		}
	}
}
