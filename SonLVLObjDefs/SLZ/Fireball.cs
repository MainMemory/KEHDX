using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S1ObjectDefinitions.SLZ
{
	class Fireball : ObjectDefinition
	{
		private readonly Sprite[] sprites = new Sprite[2];
		private PropertySpec[] properties;

		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("SLZ/Objects.gif");
			sprites[0] = new Sprite(sheet.GetSection(1, 1, 15, 31), -7, -23);
			sprites[1] = new Sprite(sheet.GetSection(2, 34, 31, 15), -23, -8);
			
			properties = new PropertySpec[2];
			properties[0] = new PropertySpec("Pattern", typeof(int), "Extended",
				"The pattern this Fireball is to follow.", null, new Dictionary<string, int>
				{
					{ "Eject Up - Slowest", 0 },
					{ "Eject Up - Slow", 1 },
					{ "Eject Up - Fast", 2 },
					{ "Eject Up - Fastest", 3 },
					{ "Travel Up", 4 },
					{ "Travel Down", 5 },
					{ "Travel Left", 6 },
					{ "Travel Right", 7 }
				},
				(obj) => obj.PropertyValue & 7,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 248) | (byte)((int)value)));
			
			properties[1] = new PropertySpec("Interval", typeof(int), "Extended",
				"The timings this Fireball is to be based off.", null, new Dictionary<string, int>
				{
					{ "30 Frames", 0x00 },
					{ "60 Frames", 0x10 },
					{ "90 Frames", 0x20 },
					{ "120 Frames", 0x30 },
					{ "150 Frames", 0x40 },
					{ "180 Frames", 0x50 },
					{ "210 Frames", 0x60 },
					{ "240 Frames", 0x70 },
					{ "270 Frames", 0x80 },
					{ "300 Frames", 0x90 },
					{ "330 Frames", 0xa0 },
					{ "360 Frames", 0xb0 },
					{ "390 Frames", 0xc0 },
					{ "420 Frames", 0xd0 },
					{ "450 Frames", 0xe0 },
					{ "480 Frames", 0xf0 },
				},
				(obj) => obj.PropertyValue & 240,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 15) | (byte)((int)value)));
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
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			int temp = subtype & 7;
			Sprite sprite = new Sprite(sprites[(temp > 5) ? 1 : 0]);
			sprite.Flip((temp == 6), (temp < 5));
			return sprite;
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return SubtypeImage(obj.PropertyValue);
		}
	}
}