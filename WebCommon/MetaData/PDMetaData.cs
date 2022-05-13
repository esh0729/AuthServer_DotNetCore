using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCommon
{
	public class PDMetaData : PacketData
	{
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Member variables

		public PDGameConfig gameConfig;
		public PDCharacter[] characters;
		public PDContinent[] continents;
		public PDCharacterAction[] characterActions;

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Member functions

		public override void Serialize(PacketWriter writer)
		{
			base.Serialize(writer);

			writer.Write(gameConfig);
			writer.Write(characters);
			writer.Write(continents);
			writer.Write(characterActions);
		}

		public override void Deserialize(PacketReader reader)
		{
			base.Deserialize(reader);

			gameConfig = reader.ReadPacketData<PDGameConfig>();
			characters = reader.ReadPacketDatas<PDCharacter>();
			continents = reader.ReadPacketDatas<PDContinent>();
			characterActions = reader.ReadPacketDatas<PDCharacterAction>();
		}
	}
}
