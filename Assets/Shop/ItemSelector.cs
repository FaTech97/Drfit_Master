using System.Collections.Generic;
using System.Linq;
using _GAME.scripts.Architecture.Architecture.Persistanse_Service;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Shop
{
    public class ItemSelector : MonoBehaviour
    {
        [SerializeField] private Button prevButton;
        [SerializeField] private Button nextButton;
        [SerializeField] private Button BuyButton;
        [SerializeField] private string prefix;
        [SerializeField] private Text _priceText;
        [SerializeField] private Text buttonText;
        [SerializeField] private string postfix;
        private int currentItemIndex;
        private List<ShopItemConfig> _items;
        private IPersistanseDataService _persistanseDataService;

        [Inject]
        private void Contruct(IPersistanseDataService persistanseDataService)
        {
            _persistanseDataService = persistanseDataService;
        }
    
        private void Awake()
        {
            prevButton.onClick.AddListener(() => ChangeItem(-1));
            nextButton.onClick.AddListener(() => ChangeItem(1));
            BuyButton.onClick.AddListener(Buy);
            LoadAllConfigs();
            InstantiateAll();
            SelectItem(0);
        }

        private void Buy()
        {
            _persistanseDataService.SpendMoney(_items[currentItemIndex].price); 
            _persistanseDataService.AddCar(_items[currentItemIndex].id);
            _items[currentItemIndex].isOpen = true;
            MakeButtonIsOpen();
        }
    
        private void InstantiateAll()
        {
            foreach (ShopItemConfig itemConfig in _items)
            {
                var a = Instantiate(itemConfig.model, transform.position, Quaternion.identity, transform);
                a.AddComponent<ItemAnimation>();
            }
        }

        private void LoadAllConfigs() => 
            _items = Resources
                .LoadAll<ShopItemConfig>("").ToList();

        private void SelectItem(int _index)
        {
            prevButton.interactable = (_index != 0);
            nextButton.interactable = (_index != transform.childCount - 1);
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(i == _index);
                if (_items[currentItemIndex].isOpen)
                {
                    MakeButtonIsOpen();
                }
                else
                {
                    _priceText.text = prefix + _items[_index].price + postfix;
                    buttonText.text = "Купить";
                }
            }
        }

        private void MakeButtonIsOpen()
        {
            _priceText.text = "";
            buttonText.text = "Выбрать";
            BuyButton.onClick.RemoveListener(Buy);
            BuyButton.onClick.RemoveListener(SetItem);
            
        }

        private void SetItem()
        {
            
        }

        public void ChangeItem(int _change)
        {
            currentItemIndex += _change;
            SelectItem(currentItemIndex);
        }
    }
}