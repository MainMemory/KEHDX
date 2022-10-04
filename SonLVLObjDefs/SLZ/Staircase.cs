using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S1ObjectDefinitions.SLZ
{
	class Staircase : ObjectDefinition
	{
		private Sprite img;
		private PropertySpec[] properties;

		public override void Init(ObjectData data)
		{
			img = new Sprite(LevelData.GetSpriteSheet("SLZ/Objects.gif").GetSection(67, 26, 128, 32), -16, -16);
			
			properties = new PropertySpec[2];
			properties[0] = new PropertySpec("Activate From", typeof(int), "Extended",
				"Where the Staircase should be activated from.", null, new Dictionary<string, int>
				{
					{ "Top", 0 },
					{ "Bottom", 2 }
				},
				(obj) => obj.PropertyValue & 3,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 252) | (byte)((int)value)));
			
			properties[1] = new PropertySpec("Direction", typeof(int), "Extended",
				"Which way the Staircase will expand.", null, new Dictionary<string, int>
				{
					{ "Left", 0 },
					{ "Right", 1 }
				},
				(obj) => (((V4ObjectEntry)obj).Direction.HasFlag(RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipX) ? 1 : 0),
				(obj, value) => ((V4ObjectEntry)obj).Direction = (RSDKv3_4.Tiles128x128.Block.Tile.Directions)value);
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
			Sprite sprite = new Sprite(img);
			sprite.Flip(false, (((V4ObjectEntry)obj).Direction.HasFlag(RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipY)));
			return sprite;
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			var bitmap = new BitmapBits(129, 129);
			for (int i = 0; i < 97; i += 32)
			{
				bitmap.DrawRectangle(LevelData.ColorWhite, i, i, 32, 32);
			}
			bitmap.Flip(!(((V4ObjectEntry)obj).Direction.HasFlag(RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipX)), (((V4ObjectEntry)obj).Direction.HasFlag(RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipY)));
			return new Sprite(bitmap, -16 - ((!(((V4ObjectEntry)obj).Direction.HasFlag(RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipX))) ? 1 : 0), 16);
		}
	}
}