using _GAME.scripts.Architecture.Architecture.Services.StaticData;
using UnityEngine;
using UnityEngine.Serialization;

namespace Shop
{
	[CreateAssetMenu(fileName = "ShopItem", menuName = "Qwino configs/Create shop item")]
	public class ShopItemConfig : IConfig<ItemId>
	{
		public GameObject model;
		public int price = 10;
		public bool isOpen = false;
		[Range(3, 10)] public int HP = 3;
		[Range(25, 300)] public int RepairPrice = 25;
	}
}
