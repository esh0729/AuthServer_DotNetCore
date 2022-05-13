using WebCommon;

namespace AuthServer
{
	public class MakeMetaData
	{
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Static member variables

		public static byte[] CreateMetaData(UserDbContext context)
		{
			//
			//
			//

			PDMetaData metaData = new PDMetaData();

			//
			// 게임설정
			//
			List<GameConfig> dbGameConfigs = context.gameConfigs.ToList();
			if (dbGameConfigs.Count() > 0)
			{
				metaData.gameConfig = dbGameConfigs.First().ToPDGameConfig();
			}
			dbGameConfigs.Clear();

			//
			// 대륙
			//
			List<Continent> dbContinents = context.continents.OrderBy(r => r.continentId).ToList();
			metaData.continents = new PDContinent[dbContinents.Count];
			for (int i = 0; i < dbContinents.Count; i++)
			{
				metaData.continents[i] = dbContinents[i].ToPDContinent();
			}	
			dbContinents.Clear();

			//
			// 캐릭터
			//
			List<Character> dbCharacters = context.characters.OrderBy(r => r.characterId).ToList();
			metaData.characters = new PDCharacter[dbCharacters.Count];
			for (int i = 0; i < dbCharacters.Count; i++)
			{
				metaData.characters[i] = dbCharacters[i].ToPDCharacter();
			}
			dbCharacters.Clear();

			//
			// 캐릭터행동
			//
			List<CharacterAction> dbCharacterActions = context.characterActions.OrderBy(r => r.characterId).OrderBy(r => r.actionId).ToList();
			metaData.characterActions = new PDCharacterAction[dbCharacterActions.Count];
			for (int i = 0; i < dbCharacterActions.Count; i++)
			{
				metaData.characterActions[i] = dbCharacterActions[i].ToPDCharacterAction();
			}
			dbCharacterActions.Clear();

			return metaData.SerializeRaw();
		}
	}
}
