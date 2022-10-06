using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.CNZ
{
	class Bumper : ObjectDefinition
	{
		// We're using the name "Bumper" here, as opposed to what Origins calls it ("OBJECT NAME")
		// "Bumper" rolls better off the tongue, I feel
		
		private Sprite img;

		public override void Init(ObjectData data)
		{
			switch (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1])
			{
				case '4':
					img = new Sprite(LevelData.GetSpriteSheet("CNZ/Objects.gif").GetSection(148, 100, 32, 32), -16, -16);
					break;
				case '9':
					img = new Sprite(LevelData.GetSpriteSheet("MPZ/Objects2.gif").GetSection(1, 1, 32, 32), -16, -16);
					break;
				case 'Z': // SYZ (This is a KEHDX addition)
					img = new Sprite(LevelData.GetSpriteSheet("SYZ/Objects.gif").GetSection(1, 131, 28, 28), -14, -14);
					break;
				case 'M':
				default:
					img = new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(511, 339, 32, 32), -16, -16);
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