using System;
using System.Collections.Generic;
using System.Linq;
using _GAME.scripts;
using _GAME.scripts.Architecture.Architecture.Persistanse_Service;
using _GAME.scripts.Architecture.Architecture.Services.ScenesService;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Shop
{
    public class ItemSelector : MonoBehaviour
    {
        [SerializeField] private Button prevButton;
        [SerializeField] private Button nextButton;
        [SerializeField] private Button closeButton;
        [SerializeField] private Button BuyButton;
        [SerializeField] private string prefix;
        [SerializeField] private Text _priceText;
        [SerializeField] private Text buttonText;
        [SerializeField] private string postfix;
        [SerializeField] private BoxesCounterViewer sizeViewer;
        [SerializeField] private BoxesCounterViewer speedViewer;
        private int currentItemIndex;
        private List<ShopItemConfig> _items;
        private IPersistanseDataService _persistanseDataService;
        private LevelManager _levelManager;
        private SizeAligner _sizeAligner;

        [Inject]
        private void Contruct(IPersistanseDataService persistanseDataService, LevelManager levelManager)
        {
            _persistanseDataService = persistanseDataService;
            _levelManager = levelManager;
        }
    
        private void Awake()
        {
            _sizeAligner = GetComponent<SizeAligner>();
            prevButton.onClick.AddListener(() => ChangeItem(-1));
            nextButton.onClick.AddListener(() => ChangeItem(1));
            closeButton.onClick.AddListener(() => Close());
            BuyButton.onClick.AddListener(Buy);
            LoadAllConfigs();
            InstantiateAll();
            SelectItem(0);
        }

        private void Close()
        {
            this._levelManager.RestartCurrentLevel();
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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                ChangeItem(-1);
            }
            
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                ChangeItem(1);
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
            }
            speedViewer.SetCount(_items[currentItemIndex].speed);
            sizeViewer.SetCount(_items[currentItemIndex].size);
            _sizeAligner.AlignObjectSizeToColliderSize(transform.GetChild(currentItemIndex).gameObject);
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

        private void MakeButtonIsOpen()
        {
            _priceText.text = "";
            buttonText.text = "Выбрать";
            BuyButton.onClick.RemoveListener(Buy);
            BuyButton.onClick.AddListener(SetItem);
            
        }

        private void SetItem()
        {
            _persistanseDataService.SetItem(_items[currentItemIndex].id);
            _levelManager.RestartCurrentLevel();
        }

        public void ChangeItem(int _change)
        {
            if ((currentItemIndex + _change > (_items.Count - 1)) || (currentItemIndex + _change < 0))
            {
                return;
            }
            currentItemIndex += _change;
            SelectItem(currentItemIndex);
        }
    }
}