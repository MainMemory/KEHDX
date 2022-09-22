using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S1ObjectDefinitions.Enemies
{
	class CrabmeatShot : ObjectDefinition
	{
		private Sprite img;

		public override void Init(ObjectData data)
		{
			switch (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1])
			{
				case '1':
				default:
					img = new Sprite(LevelData.GetSpriteSheet("GHZ/Objects.gif").GetSection(179, 127, 12, 12), -6, -6);
					break;
				case '3':
					img = new Sprite(LevelData.GetSpriteSheet("SYZ/Objects.gif").GetSection(227, 1, 12, 12), -6, -6);
					break;
				case '7':
					img = new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(47, 1, 12, 12), -6, -6);
					break;
			}
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}

		public override bool Hidden
		{
			get { return true; }
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