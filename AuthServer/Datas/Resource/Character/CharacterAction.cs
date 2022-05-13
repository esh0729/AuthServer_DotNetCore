using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using WebCommon;

namespace AuthServer
{
	[Table("r_CharacterAction")]
	public class CharacterAction
	{
		[Key, Column(Order = 0)]
		public int characterId { get; set; }
		[Key, Column(Order = 1)]
		public int actionId { get; set; }
		
		//
		//
		//

		public PDCharacterAction ToPDCharacterAction()
		{
			PDCharacterAction characterAction = new PDCharacterAction();
			characterAction.characterId = characterId;
			characterAction.actionId = actionId;

			return characterAction;
		}
	}
}
