using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S1ObjectDefinitions.SLZ
{
	class RisingPlatform : ObjectDefinition
	{
		private Sprite img;
		private PropertySpec[] properties;

		public override void Init(ObjectData data)
		{
			img = new Sprite(LevelData.GetSpriteSheet("SLZ/Objects.gif").GetSection(84, 188, 80, 32), -40, -8);

			properties = new PropertySpec[1];
			properties[0] = new PropertySpec("Movement", typeof(int), "Extended",
				"The Platform's movement.", null, new Dictionary<string, int>
				{
					{ "Up (128px)", 0 },
					{ "Up (256px)", 1 },
					{ "Up (416px)", 2 },
					
					{ "Down (128px)", 3 },
					{ "Down (256px)", 4 },
					{ "Down (416px)", 5 },
					
					{ "Up (160px)", 6 },
					{ "Up (288px)", 7 },
					{ "Up (352px)", 8 },
					
					{ "Down (160px)", 9 },
					{ "Down (288px)", 10 },
					{ "Down (352px)", 11 },
					
					{ "Up-Right", 12 },
					{ "Down-Left", 13 }
				},
				(obj) => (obj.PropertyValue & 15),
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 240) | (byte)((int)value)));
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
			return img;
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return img;
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			// The default values from RisingPlatform_distanceTable ("table28") in the object's script
			// If you've modified those, you can simply copy them over.
			int[] RisingPlatform_distanceTable = new int[15] {0x400000, 0x800000, 0xD00000, 0x400000, 0x800000, 0xD00000, 0x500000, 0x900000, 0xB00000, 0x500000, 0x900000, 0xB00000, 0x800000, 0x800000, 0xC00000};
			var overlay = new BitmapBits(80, 32);
			overlay.DrawRectangle(LevelData.ColorWhite, 0, 0, 80 - 1, 32 - 1);
			if (obj.PropertyValue < 14)
			{
				if (obj.PropertyValue < 12)
				{
					int sign = (obj.PropertyValue % 6 < 3) ? -1 : 1;
					return new Sprite(overlay, -40, sign * (RisingPlatform_distanceTable[obj.PropertyValue] / 0x10000 * 2) - 8);
				}
				else
				{
					int sign = (obj.PropertyValue == 13) ? -1 : 1;
					return new Sprite(overlay, sign * (RisingPlatform_distanceTable[obj.PropertyValue] / 0x10000 * 2) - 40, -sign * (RisingPlatform_distanceTable[obj.PropertyValue] / 0x10000) - 8);
				}
			}
			return new Sprite(overlay, -40, -8);
		}
	}
}