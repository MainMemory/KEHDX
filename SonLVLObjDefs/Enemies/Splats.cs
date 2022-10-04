using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S1ObjectDefinitions.Enemies
{
	class Splats : ObjectDefinition
	{
		private Sprite img;
		private PropertySpec[] properties;

		public override void Init(ObjectData data)
		{
			switch (LevelData.StageInfo.folder)
			{
				case "GHZ":
				default:
					img = new Sprite(LevelData.GetSpriteSheet("GHZ/Objects.gif").GetSection(214, 211, 21, 40), -11, -15);
					break;
				case "MBZ":
					img = new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(220, 254, 21, 40), -11, -15);
					break;
			}

			properties = new PropertySpec[2];
			properties[0] = new PropertySpec("Direction", typeof(int), "Extended",
				"Which way the Splats will be facing intially.", null, new Dictionary<string, int>
				{
					{ "Left", 0 },
					{ "Right", 1 }
				},
				(obj) => obj.PropertyValue & 1,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 254) | (byte)((int)value)));
			
			properties[1] = new PropertySpec("Behaviour", typeof(int), "Extended",
				"How the Splats will act.", null, new Dictionary<string, int>
				{
					{ "Advance", 0 },
					{ "Hover", 2 }
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