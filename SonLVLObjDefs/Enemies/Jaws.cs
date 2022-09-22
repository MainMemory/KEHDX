using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S1ObjectDefinitions.Enemies
{
	class Jaws : ObjectDefinition
	{
		private Sprite img;
		private PropertySpec[] properties;

		public override void Init(ObjectData data)
		{
			switch (LevelData.StageInfo.folder)
			{
				case "LZ":
				default:
					img = new Sprite(LevelData.GetSpriteSheet("LZ/Objects.gif").GetSection(1, 105, 48, 24), -16, -12);
					break;
				case "MBZ":
					img = new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(1, 264, 48, 24), -16, -12);
					break;
			}

			properties = new PropertySpec[2];
			
			properties[0] = new PropertySpec("Swim Time", typeof(int), "Extended",
				"How long the Jaws will swim in a direction at a time, to be multiplied by 64 frames in-game.", null,
				(obj) => obj.PropertyValue & 127,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 128) | (byte)((int)value) & 127));

			properties[1] = new PropertySpec("Direction", typeof(int), "Extended",
				"Which way the Jaws will be facing initially.", null, new Dictionary<string, int>
				{
					{ "Left", 0 },
					{ "Right", 128 }
				},
				(obj) => obj.PropertyValue & 128,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 127) | (byte)((int)value)));
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
			get { return img; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			Sprite sprite = new Sprite(img);
			sprite.Flip((subtype & 128) != 0, false);
			return sprite;
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return SubtypeImage(obj.PropertyValue);
		}
	}
}