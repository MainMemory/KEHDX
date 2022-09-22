using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S1ObjectDefinitions.Enemies
{
	class Bomb : ObjectDefinition
	{
		private Sprite img;
		private PropertySpec[] properties;

		public override void Init(ObjectData data)
		{
			switch (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1])
			{
				case '5':
				default:
					img = new Sprite(LevelData.GetSpriteSheet("SLZ/Objects.gif").GetSection(53, 131, 20, 37), -10, -21);
					break;
				case '6':
					img = new Sprite(LevelData.GetSpriteSheet("SBZ/Objects.gif").GetSection(52, 40, 20, 37), -10, -21);
					break;
				case '7':
					img = new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(53, 328, 20, 37), -10, -21);
					break;
			}

			properties = new PropertySpec[2];
			properties[0] = new PropertySpec("Direction", typeof(int), "Extended",
				"Which way the Bomb is facing.", null, new Dictionary<string, int>
				{
					{ "Left", 0 },
					{ "Right", 1 }
				},
				(obj) => obj.PropertyValue & 1,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 254) | (byte)((int)value)));
			
			properties[1] = new PropertySpec("On Roof", typeof(int), "Extended",
				"If the Bomb is on a roof or not.", null, new Dictionary<string, int>
				{
					{ "False", 0 },
					{ "True", 2 }
				},
				(obj) => obj.PropertyValue & 2,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 253) | (byte)((int)value)));
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] { 0, 1, 2, 3 }); }
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
				case 0: return "Left";
				case 1: return "Right";
				case 2: return "Left - Roof";
				case 3: return "Right - Roof";
				default: return "Unknown";
			}
			return subtype + "";
		}

		public override Sprite Image
		{
			get { return img; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			Sprite sprite = new Sprite(img);
			sprite.Flip((subtype & 1) != 0, (subtype & 2) != 0); // Do pardon the odd syntax...
			return sprite;
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return SubtypeImage(obj.PropertyValue);
		}
	}
}