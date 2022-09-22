using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S1ObjectDefinitions.Enemies
{
	class Caterkiller : ObjectDefinition
	{
		private readonly Sprite[] sprites = new Sprite[3];
		private PropertySpec[] properties;

		public override void Init(ObjectData data)
		{
			switch (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1])
			{
				case '2':
				default:
					BitmapBits sheet = LevelData.GetSpriteSheet("MZ/Objects.gif");
					sprites[0] = new Sprite(sheet.GetSection(18, 81, 16, 22), -8, -14);
					sprites[1] = new Sprite(sheet.GetSection(1, 81, 16, 24), -8, -14);
					sprites[2] = new Sprite(sheet.GetSection(35, 81, 16, 16), -8, -8);
					break;
				case '3':
					sheet = LevelData.GetSpriteSheet("SYZ/Objects.gif");
					sprites[0] = new Sprite(sheet.GetSection(98, 98, 16, 22), -8, -14);
					sprites[1] = new Sprite(sheet.GetSection(81, 98, 16, 24), -8, -14);
					sprites[2] = new Sprite(sheet.GetSection(98, 121, 16, 16), -8, -8);
					break;
				case '6':
					sheet = LevelData.GetSpriteSheet("SBZ/Objects.gif");
					sprites[0] = new Sprite(sheet.GetSection(75, 26, 16, 22), -8, -14);
					sprites[1] = new Sprite(sheet.GetSection(75, 1, 16, 24), -8, -14);
					sprites[2] = new Sprite(sheet.GetSection(75, 49, 16, 16), -8, -8);
					break;
				case '7':
					sheet = LevelData.GetSpriteSheet("MBZ/Objects.gif");
					sprites[0] = new Sprite(sheet.GetSection(1, 68, 16, 22), -8, -14);
					sprites[1] = new Sprite(sheet.GetSection(18, 68, 16, 24), -8, -14);
					sprites[2] = new Sprite(sheet.GetSection(35, 68, 16, 16), -8, -8);
					break;
			}

			properties = new PropertySpec[1];
			properties[0] = new PropertySpec("Direction", typeof(int), "Extended",
				"Which way the Caterkiller is facing.", null, new Dictionary<string, int>
				{
					{ "Left", 0 },
					{ "Right", 1 }
				},
				(obj) => obj.PropertyValue & 1,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 254) | (byte)((int)value)));
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] { 0, 1 }); }
		}
		
		public override byte DefaultSubtype
		{
			get { return 0; }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			switch (subtype)
			{
				case 0:
					return "Facing Left";
				case 1:
					return "Facing Right";
				default:
					return "Unknown";
			}
		}

		public override Sprite Image
		{
			get { return SubtypeImage(0); }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			List<Sprite> sprs = new List<Sprite>();
			
			bool flip = (subtype & 1) > 0;
			
			for (int i = 4; i > 0; i--)
			{
				Sprite tmp = new Sprite(sprites[(i == 1) ? 0 : 2]);
				tmp.Flip(flip, false);
				tmp.Offset(((i - 1) * 12) * (flip ? (-1) : (1)), 0);
				sprs.Add(tmp);
			}
			
			return new Sprite(sprs.ToArray());
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return SubtypeImage(obj.PropertyValue);
		}
	}
}