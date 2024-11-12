using _GAME.scripts.Architecture.Architecture.Services.StaticData.types;
using _GAME.scripts.Architecture.types;
using Shop;

namespace _GAME.scripts.Architecture.Architecture.Services.StaticData
{
	public class StaticDataService
	{
		public StaticDataResource<WindowId, WindowConfig> Windows = new StaticDataResource<WindowId, WindowConfig>("Windows/");
		public StaticDataResource<ItemId, ShopItemConfig> Items = new StaticDataResource<ItemId, ShopItemConfig>("Items/");
	}
}
