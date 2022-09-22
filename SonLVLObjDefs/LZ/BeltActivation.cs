using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S1ObjectDefinitions.LZ
{
	class BeltActivation : ObjectDefinition
	{
		private PropertySpec[] properties;
		private Sprite img;
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}

		public override void Init(ObjectData data)
		{
			img = new Sprite(LevelData.GetSpriteSheet("Global/Display.gif").GetSection(239, 239, 16, 16), -8, -8);
			
			properties = new PropertySpec[1];
			properties[0] = new PropertySpec("Activate Count", typeof(int), "Extended",
				"How many of the following objects should be activated by this Activator.", null,
				(obj) => obj.PropertyValue,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}
		
		public override byte DefaultSubtype
		{
			get { return 8; }
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
			if (obj.PropertyValue == 0)
				return null;
			
			int xmin = obj.X;
			int ymin = obj.Y;
			int xmax = obj.X;
			int ymax = obj.Y;
			
			for (int i = 1; i < obj.PropertyValue + 1; i++)
			{
				xmin = Math.Min(xmin, LevelData.Objects[LevelData.Objects.IndexOf(obj) + i].X);
				ymin = Math.Min(ymin, LevelData.Objects[LevelData.Objects.IndexOf(obj) + i].Y);
				xmax = Math.Max(xmax, LevelData.Objects[LevelData.Objects.IndexOf(obj) + i].X);
				ymax = Math.Max(ymax, LevelData.Objects[LevelData.Objects.IndexOf(obj) + i].Y);
			}
			
			BitmapBits bmp = new BitmapBits(xmax - xmin + 1, ymax - ymin + 1);
			
			for (int i = 1; i < obj.PropertyValue + 1; i++)
				bmp.DrawLine(LevelData.ColorWhite, obj.X - xmin, obj.Y - ymin, LevelData.Objects[LevelData.Objects.IndexOf(obj) + i].X - xmin, LevelData.Objects[LevelData.Objects.IndexOf(obj) + i].Y - ymin);
			
			return new Sprite(bmp, xmin - obj.X, ymin - obj.Y);
		}
	}
}