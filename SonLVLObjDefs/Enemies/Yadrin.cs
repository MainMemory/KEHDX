using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S1ObjectDefinitions.Enemies
{
	class Yadrin : ObjectDefinition
	{
		private Sprite img;
		private PropertySpec[] properties;

		public override void Init(ObjectData data)
		{
			switch (LevelData.StageInfo.folder)
			{
				case "MZ":
				default:
					img = new Sprite(LevelData.GetSpriteSheet("MZ/Objects.gif").GetSection(1, 2, 40, 38), -20, -20);
					break;
				case "SYZ":
					img = new Sprite(LevelData.GetSpriteSheet("SYZ/Objects.gif").GetSection(1, 2, 40, 38), -20, -20);
					break;
				case "MBZ":
					img = new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(138, 2, 40, 38), -20, -20);
					break;
			}

			properties = new PropertySpec[1];
			
			properties[0] = new PropertySpec("Direction", typeof(int), "Extended",
				"Which way the Yadrin is facing.", null, new Dictionary<string, int>
				{
					{ "Left", 0 },
					{ "Right", 1 }
				},
				(obj) => obj.PropertyValue & 1,
				(obj, value) => obj.PropertyValue = ((byte)((int)value)));
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
			get { return img; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			Sprite sprite = new Sprite(img);
			sprite.Flip((subtype & 1) != 0, false);
			return sprite;
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return SubtypeImage(obj.PropertyValue);
		}
	}
}