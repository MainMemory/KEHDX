using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S1ObjectDefinitions.Enemies
{
	class NewtronFly : ObjectDefinition
	{
		private Sprite img;

		public override void Init(ObjectData data)
		{
			switch (LevelData.StageInfo.folder)
			{
				case "GHZ":
				default:
					img = new Sprite(LevelData.GetSpriteSheet("GHZ/Objects2.gif").GetSection(161, 1, 39, 39), -20, -20);
					break;
				case "MBZ":
					img = new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(1, 124, 39, 39), -20, -20);
					break;
			}
			
			// Despite them being set in the scene, prop val & val1 are unused
			// It may just be an oversight and they were intended to be used, as Newtron Shoots have *do* use it
			// It seemginly would've had both x and y checks too, in contrast to Shoots who only have x
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
			return img;
		}
	}
}