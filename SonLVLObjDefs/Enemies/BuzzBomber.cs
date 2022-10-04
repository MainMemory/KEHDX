using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S1ObjectDefinitions.Enemies
{
	class BuzzBomber : ObjectDefinition
	{
		private readonly Sprite[] sprites = new Sprite[2];
		private PropertySpec[] properties;

		public override void Init(ObjectData data)
		{
			switch (LevelData.StageInfo.folder)
			{
				case "GHZ":
				default:
					BitmapBits sheet = LevelData.GetSpriteSheet("GHZ/Objects.gif");
					sprites[0] = new Sprite(sheet.GetSection(98, 74, 45, 19), -23, -9);
					sprites[1] = new Sprite(sheet.GetSection(144, 79, 35, 8), -17, -15);
					break;
				case "MZ":
					sheet = LevelData.GetSpriteSheet("MZ/Objects.gif");
					sprites[0] = new Sprite(sheet.GetSection(1, 127, 45, 19), -23, -9);
					sprites[1] = new Sprite(sheet.GetSection(38, 147, 35, 8), -17, -15);
					break;
				case "SYZ":
					sheet = LevelData.GetSpriteSheet("SYZ/Objects.gif");
					sprites[0] = new Sprite(sheet.GetSection(1, 81, 45, 19), -23, -9);
					sprites[1] = new Sprite(sheet.GetSection(38, 101, 35, 8), -17, -15);
					break;
				case "MBZ":
					sheet = LevelData.GetSpriteSheet("MBZ/Objects.gif");
					sprites[0] = new Sprite(sheet.GetSection(1, 1, 45, 19), -23, -9);
					sprites[1] = new Sprite(sheet.GetSection(38, 21, 35, 8), -17, -15);
					break;
			}

			properties = new PropertySpec[2];
			properties[0] = new PropertySpec("Direction", typeof(int), "Extended",
				"Which way the Buzz Bomber is facing.", null, new Dictionary<string, int>
				{
					{ "Left", 0 },
					{ "Right", 1 }
				},
				(obj) => obj.PropertyValue & 1,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 254) | (byte)((int)value)));
			
			properties[1] = new PropertySpec("Range", typeof(int), "Extended",
				"The range of the Buzz Bomber's activation trigger.", null, new Dictionary<string, int>
				{
					{ "Large", 0 },
					{ "Small", 2 }
				},
				(obj) => obj.PropertyValue & 2,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 253) | (byte)((int)value)));
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
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
			return subtype + "";
		}

		public override Sprite Image
		{
			get { return SubtypeImage(0); }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			List<Sprite> sprs = new List<Sprite>();
			
			for (int i = 0; i < 2; i++)
			{
				Sprite tmp = new Sprite(sprites[i]);
				tmp.Flip((subtype & 1) != 0, false);
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