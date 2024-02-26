using UnityEngine;
using UnityEngine.Serialization;

namespace Shop
{
    [CreateAssetMenu(fileName = "ShopItem", menuName = "Qwino configs/Create shop item")]
    public class ShopItemConfig : ScriptableObject
    {
        public string id = "";
        public GameObject model;
        public int price = 10;
        public bool isOpen = false;
    }
}