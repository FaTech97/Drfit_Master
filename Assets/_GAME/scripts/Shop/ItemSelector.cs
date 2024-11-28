using System;
using System.Collections.Generic;
using System.Linq;
using _GAME.scripts;
using _GAME.scripts.Architecture.Architecture.Persistanse_Service;
using _GAME.scripts.Architecture.Architecture.Services.ScenesService;
using Assets.SimpleLocalization.Scripts;
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
		[SerializeField] private Text _priceText;
		[SerializeField] private Text buttonText;
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
			LoadAllConfigs();
			prevButton.onClick.AddListener(() => ChangeItem(-1));
			nextButton.onClick.AddListener(() => ChangeItem(1));
			InstantiateAll();
			SelectItem(0);
		}

		private void Buy()
		{
			_persistanseDataService.SpendMoney(_items[currentItemIndex].price);
			_persistanseDataService.AddCar(_items[currentItemIndex].id);
			_items[currentItemIndex].isOpen = true;
			MakeButtonIsOpen(_items[currentItemIndex]);
		}

		private void InstantiateAll()
		{
			_items = _items.OrderBy(item => item.price).ToList();
			foreach (ShopItemConfig itemConfig in _items)
			{
				itemConfig.isOpen = (itemConfig.id == ItemId.DefaultItem) ||
				                    _persistanseDataService.Data.Player.BuysItemsIDs.Contains(itemConfig.id);
				GameObject shopItemInstance =
					Instantiate(itemConfig.model, transform.position, Quaternion.identity, transform);
				shopItemInstance.AddComponent<ItemAnimation>();
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
				.LoadAll<ShopItemConfig>("Items").ToList();

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
				MakeButtonIsOpen(_items[currentItemIndex]);
			}
			else
			{
				_priceText.gameObject.SetActive(true);
				_priceText.text = _items[_index].price.ToString();
				buttonText.text = LocalizationManager.Localize("SHOP.Buy");
				BuyButton.onClick.RemoveAllListeners();
				BuyButton.onClick.AddListener(Buy);
				BuyButton.interactable = _persistanseDataService.Data.Player.Coins > _items[_index].price;
			}
		}

		private void MakeButtonIsOpen(ShopItemConfig item)
		{
			if (item.id == _persistanseDataService.Data.Player.CurrectItemId)
			{
				buttonText.text = LocalizationManager.Localize("SHOP.Selected");
			}
			else
			{
				buttonText.text = LocalizationManager.Localize("SHOP.Choose");
			}

			_priceText.gameObject.SetActive(false);
			BuyButton.onClick.RemoveAllListeners();
			BuyButton.onClick.AddListener(SetItem);
		}

		private void SetItem()
		{
			_persistanseDataService.SetItem(_items[currentItemIndex].id);
			_priceText.gameObject.SetActive(false);
			buttonText.text = LocalizationManager.Localize("SHOP.Selected");
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
