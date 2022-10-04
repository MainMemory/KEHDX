using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S1ObjectDefinitions.Enemies
{
	class NewtronShoot : ObjectDefinition
	{
		private readonly Sprite[] sprites = new Sprite[2];
		private PropertySpec[] properties;

		public override void Init(ObjectData data)
		{
			switch (LevelData.StageInfo.folder)
			{
				case "GHZ":
				default:
					BitmapBits sheet = LevelData.GetSpriteSheet("GHZ/Objects2.gif");
					sprites[0] = new Sprite(sheet.GetSection(1, 1, 39, 39), -20, -20);
					sprites[1] = new Sprite(sheet.GetSection(81, 1, 39, 39), -19, -20);
					break;
				case "MBZ":
					sheet = LevelData.GetSpriteSheet("MBZ/Objects.gif");
					sprites[0] = new Sprite(sheet.GetSection(1, 164, 39, 39), -20, -20);
					sprites[1] = new Sprite(sheet.GetSection(81, 164, 39, 39), -19, -20);
					break;
			}
			
			// Originally prop val was unused, but Origins uses it (+ val1) for Missions
			// Fire In Dir needs to be true in order for the second one to have any effect
			// However, since they're displayed alphabetically, they're the other way around instead :(
			// At least there's descriptions, though
			
			properties = new PropertySpec[2];
			properties[0] = new PropertySpec("Fire In Dir", typeof(int), "Extended",
				"If the Newtroon should only shoot in a specific direction, direction decided by Fire Direction. Only has effect in Origins's Mission Mode.", null, new Dictionary<string, int>
				{
					{ "False", 0 },
					{ "True", 1 }
				},
				(obj) => ((V4ObjectEntry)obj).Value1 & 1,
				(obj, value) => ((V4ObjectEntry)obj).Value1 = (byte)((int)value));
			
			properties[1] = new PropertySpec("Fire Direction", typeof(int), "Extended",
				"Which way the Newtron should fire. Only takes effect if Fire In Dir is active too, similarly only has effect in Mission Mode.", null, new Dictionary<string, int>
				{
					{ "Right", 0 },
					{ "Left", 1 }
				},
				(obj) => obj.PropertyValue & 1,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
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
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[subtype & 1];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			// Probably could just flip frame 1, but may as well use the dedicated flip sprite anyways
			return sprites[obj.PropertyValue & 1];
		}
	}
}