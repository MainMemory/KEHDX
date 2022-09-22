using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S1ObjectDefinitions.Enemies
{
	class Orbinaut : ObjectDefinition
	{
		private readonly Sprite[] sprites = new Sprite[2];
		private PropertySpec[] properties;

		public override void Init(ObjectData data)
		{
			switch (LevelData.StageInfo.folder)
			{
				case "LZ":
				default:
					BitmapBits sheet = LevelData.GetSpriteSheet("LZ/Objects.gif");
					sprites[0] = new Sprite(sheet.GetSection(50, 105, 20, 20), -10, -10);
					sprites[1] = new Sprite(sheet.GetSection(107, 1, 16, 16), -8, -8);
					break;
				case "SLZ":
					sheet = LevelData.GetSpriteSheet("SLZ/Objects.gif");
					sprites[0] = new Sprite(sheet.GetSection(51, 1, 20, 20), -10, -10);
					sprites[1] = new Sprite(sheet.GetSection(114, 1, 16, 16), -8, -8);
					break;
				case "SBZ":
					sheet = LevelData.GetSpriteSheet("SBZ/Objects.gif");
					sprites[0] = new Sprite(sheet.GetSection(1, 138, 20, 20), -10, -10);
					sprites[1] = new Sprite(sheet.GetSection(64, 142, 16, 16), -8, -8);
					break;
				case "MBZ":
					sheet = LevelData.GetSpriteSheet("MBZ/Objects.gif");
					sprites[0] = new Sprite(sheet.GetSection(119, 114, 20, 20), -10, -10);
					sprites[1] = new Sprite(sheet.GetSection(140, 135, 16, 16), -8, -8);
					break;
			}

			properties = new PropertySpec[2];
			properties[0] = new PropertySpec("Direction", typeof(int), "Extended",
				"Which way the Orbinaut is facing.", null, new Dictionary<string, int>
				{
					{ "Left", 0 },
					{ "Right", 1 }
				},
				(obj) => obj.PropertyValue & 1,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 254) | (byte)((int)value)));
			
			// Don't mind this, just a little something
			properties[1] = new PropertySpec("Fire Orbs", typeof(int), "Extended",
				"If the Orbinaut should fire its orbs or not.", null, new Dictionary<string, int>
				{
					{ "True", 0 },
					{ "False", 2 }
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
			
			Sprite sprite = new Sprite(sprites[0]);
			sprite.Flip((subtype & 1) != 0, false);
			sprs.Add(sprite);

			int[] posoffsets = {-18, 0, 0, -18, 18, 0, 0, 18 };
			
			for (int i = 0; i < 8; i += 2)
			{
				Sprite tmp = new Sprite(sprites[1]);
				tmp.Offset(posoffsets[i], posoffsets[i+1]);
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