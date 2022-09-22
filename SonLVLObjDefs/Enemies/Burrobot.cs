using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S1ObjectDefinitions.Enemies
{
	class Burrobot : ObjectDefinition
	{
		private Sprite img;

		public override void Init(ObjectData data)
		{
			switch (LevelData.StageInfo.folder)
			{
				case "LZ":
				default:
					img = new Sprite(LevelData.GetSpriteSheet("LZ/Objects.gif").GetSection(92, 68, 24, 46), -12, -24);
					break;
				case "MBZ":
					img = new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(88, 217, 24, 46), -12, -24);
					break;
			}
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}
		
		public override byte DefaultSubtype
		{
			get { return 0; }
		}

		public override string SubtypeName(byte subtype)
		{
			return subtype + "";
		}

		public override Sprite Image
		{
			get { return img; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return img;
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return img;
		}
	}
}