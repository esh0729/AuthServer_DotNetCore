using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCommon
{
	public class PDGameConfig : PacketData
	{
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Member variables

		public int startContinentId;
		public float startXPosition;
		public float startYPosition;
		public float startZPosition;
		public float startRadius;
		public int startYRotationType;
		public float startYRotation;
		public int heroCreationLimitCount;

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Member functions

		public override void Serialize(PacketWriter writer)
		{
			base.Serialize(writer);

			writer.Write(startContinentId);
			writer.Write(startXPosition);
			writer.Write(startYRotation);
			writer.Write(startZPosition);
			writer.Write(startRadius);
			writer.Write(startYRotationType);
			writer.Write(startYRotation);
			writer.Write(heroCreationLimitCount);
		}

		public override void Deserialize(PacketReader reader)
		{
			base.Deserialize(reader);

			startContinentId = reader.ReadInt32();
			startXPosition = reader.ReadSingle();
			startYPosition = reader.ReadSingle();
			startZPosition = reader.ReadSingle();
			startRadius = reader.ReadSingle();
			startYRotationType = reader.ReadInt32();
			startYRotation = reader.ReadSingle();
			heroCreationLimitCount = reader.ReadInt32();
		}
	}
}
