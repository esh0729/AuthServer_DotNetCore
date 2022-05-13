using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using WebCommon;

namespace AuthServer
{
	[Table("r_Character")]
	public class Character
	{
		[Key]
		public int characterId { get; set; }
		[Required]
		public int moveSpeed { get; set; }

		//
		//
		//

		public PDCharacter ToPDCharacter()
		{
			PDCharacter character = new PDCharacter();
			character.characterId = characterId;
			character.moveSpeed = moveSpeed;

			return character;
		}
	}
}
