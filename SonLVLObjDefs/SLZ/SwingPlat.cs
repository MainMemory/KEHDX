using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S1ObjectDefinitions.SLZ
{
	class SwingPlat : ObjectDefinition
	{
		private readonly Sprite[] sprites = new Sprite[3];
		private PropertySpec[] properties;
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}

		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("SLZ/Objects.gif");
			sprites[0] = new Sprite(sheet.GetSection(173, 141, 16, 16), -8, -8);
			sprites[1] = new Sprite(sheet.GetSection(173, 158, 16, 16), -8, -8);
			sprites[2] = new Sprite(sheet.GetSection(84, 141, 88, 46), -44, -16);
			
			properties = new PropertySpec[1];
			properties[0] = new PropertySpec("Size", typeof(int), "Extended",
				"How many chains the Platform should hang off of.", null,
				(obj) => obj.PropertyValue,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}
		
		public override byte DefaultSubtype
		{
			get { return 4; }
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
			for (int i = 0; i <= (obj.PropertyValue + 1); i++)
			{
				int frame = (i == 0) ? 0 : (i == (obj.PropertyValue + 1)) ? 2 : 1;
				Sprite sprite = new Sprite(sprites[frame]);
				sprite.Offset(0, (i * 16));
				sprs.Add(sprite);
			}
			return new Sprite(sprs.ToArray());
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			var overlay = new BitmapBits(2 * ((obj.PropertyValue * 16) + 24) + 1, (obj.PropertyValue * 16) + 25);
			overlay.DrawCircle(LevelData.ColorWhite, ((obj.PropertyValue * 16) + 24), 0, (obj.PropertyValue * 16) + 24);
			return new Sprite(overlay, -((obj.PropertyValue * 16) + 24), 0);
		}
	}
}