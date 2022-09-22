using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S1ObjectDefinitions.SLZ
{
	class RotatingStair : ObjectDefinition
	{
		private Sprite img;

		public override void Init(ObjectData data)
		{
			img = new Sprite(LevelData.GetSpriteSheet("SLZ/Objects.gif").GetSection(67, 26, 32, 32), -16, -16);
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}
		
		public override byte DefaultSubtype
		{
			get { return 0; }
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
			List<Sprite> sprs = new List<Sprite>();
			for (int i = 0; i < 8; i++)
			{
				Sprite tmp = new Sprite(img);
				tmp.Offset(-112 + (i * 32), -112 + (i * 32));
				sprs.Add(tmp);
			}
			return new Sprite(sprs.ToArray());
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			var bitmap = new BitmapBits(257, 257);
			for (int i = 0; i < 8; i++)
			{
				bitmap.DrawRectangle(LevelData.ColorWhite, (i * 32), (i * 32), 32, 32);
			}
			bitmap.Flip(true, false);
			return new Sprite(bitmap, -128, -128);
		}
	}
}