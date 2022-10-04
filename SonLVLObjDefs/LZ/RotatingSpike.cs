using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S1ObjectDefinitions.LZ
{
	class RotatingSpike : ObjectDefinition
	{
		private readonly Sprite[] sprites = new Sprite[3];
		private PropertySpec[] properties;
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}

		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("LZ/Objects.gif");
			sprites[0] = new Sprite(sheet.GetSection(84, 173, 16, 16), -8, -8);
			sprites[1] = new Sprite(sheet.GetSection(101, 173, 16, 16), -8, -8);
			sprites[2] = new Sprite(sheet.GetSection(84, 190, 32, 32), -16, -16);
			
			properties = new PropertySpec[3];
			properties[0] = new PropertySpec("Spind Speed", typeof(int), "Extended",
				"How fast the Spikes will spin.", null,
				(obj) => obj.PropertyValue >> 4,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 15) | (byte)((int)value) << 4));
			
			properties[1] = new PropertySpec("Length", typeof(int), "Extended",
				"How long this Spike's chain will be.", null,
				(obj) => obj.PropertyValue & 15,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 240) | (byte)((int)value)));
			
			properties[2] = new PropertySpec("Direction", typeof(int), "Extended",
				"Which direction this Spike's movement should be in.", null, new Dictionary<string, int>
				{
					{ "Clockwise", 0 },
					{ "Counter-Clockwise", 1 }
				},
				(obj) => (((V4ObjectEntry)obj).Direction.HasFlag(RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipX) ? 1 : 0),
				(obj, value) => ((V4ObjectEntry)obj).Direction = (RSDKv3_4.Tiles128x128.Block.Tile.Directions)value);
		}
		
		public override byte DefaultSubtype
		{
			get { return 0x44; }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			return null;
		}

		public override Sprite Image
		{
			get { return sprites[2]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[1];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			List<Sprite> sprs = new List<Sprite>();
			for (int i = 0; i <= ((obj.PropertyValue & 15) + 1); i++)
			{
				int frame = (i == 0) ? 0 : (i == ((obj.PropertyValue & 15) + 1)) ? 2 : 1;
				Sprite sprite = new Sprite(sprites[frame]);
				sprite.Offset(0, (i * 16));
				sprs.Add(sprite);
			}
			return new Sprite(sprs.ToArray());
		}
	}
}