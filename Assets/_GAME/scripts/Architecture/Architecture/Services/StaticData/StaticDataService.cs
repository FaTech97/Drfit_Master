using _GAME.scripts.Architecture.Architecture.Services.SoundService.types;
using Shop;

namespace _GAME.scripts.Architecture.Architecture.Services.StaticData
{
	public class StaticDataService
	{
		public readonly StaticDataResource<WindowId, WindowConfig> Windows = new StaticDataResource<WindowId, WindowConfig>("Windows/");
		public readonly StaticDataResource<ItemId, ShopItemConfig> Items = new StaticDataResource<ItemId, ShopItemConfig>("Items/");
		public readonly StaticDataResource<SoundID, SoundConfig> Sounds = new StaticDataResource<SoundID, SoundConfig>("Sounds/");
	}
}
