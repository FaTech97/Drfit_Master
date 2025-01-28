using System;
using System.Linq;
using Shop;
using UnityEngine;

namespace _GAME.scripts.Architecture.Architecture.Persistanse_Service
{
	public abstract class IPersistanseDataService
	{
		public event Action OnDataChanged;
		public event Action OnDataWasLoad;
		public GameConfig GameConfig = new GameConfig();

		public GameData Data
		{
			get { return GetData(); }
			set
			{
				SetData(value);
				OnDataChanged?.Invoke();
			}
		}

		protected void InvokeDate()
		{
			Debug.Log("INVOKED DATA");
			OnDataWasLoad?.Invoke();
		}

		// TechDept решить проблему такого костыля с записью в дату
		public void ChangeLanguage(Langs lang)
		{
			GameData data = Data;
			data.Settings.Language = lang;
			Data = data;
			SaveProgress(Data);
		}

		public void SubscructHP()
		{
			GameData data = Data;
			data.Player.PlayerHP--;
			Data = data;
			SaveProgress(Data);
		}

		public void RefreshHP()
		{
			GameData data = Data;
			data.Player.PlayerHP = Data.Player.MaxHp;
			Data = data;
			SaveProgress(Data);
		}

		public void ResetProgress()
		{
			GameData gameData = new GameData();
			SaveProgress(gameData);
			OnDataChanged?.Invoke();
		}

		public void AddMoney(int coins)
		{
			GameData data = Data;
			data.Player.Coins += coins;
			Data = data;
		}

		public void ChangeLevel(int currentLevel)
		{
			GameData data = Data;
			data.Levels.CurrentLevelIndex = currentLevel;
			Data = data;
			SaveProgress(Data);
		}

		public void SpendMoney(int count)
		{
			GameData data = Data;
			data.Player.Coins -= count;
			Data = data;
			SaveProgress(Data);
		}

		public IPersistanseDataService()
		{
			Initialization();
		}

		public void DeleteProgress()
		{
			Data = new GameData();
			SaveProgress(Data);
		}

		public void AddCar(ItemId id)
		{
			GameData data = Data;
			var carList = data.Player.BuysItemsIDs.ToList();
			carList.Add(id);
			data.Player.BuysItemsIDs = carList.ToArray();
			Data = data;
			SaveProgress(Data);
		}

		public void SetItem(ShopItemConfig item)
		{
			GameData data = Data;
			var carList = data.Player.BuysItemsIDs.ToList();
			carList.Add(item.id);
			data.Player.BuysItemsIDs = carList.ToArray();
			data.Player.CurrectItemId = item.id;
			data.Player.MaxHp = item.HP;
			data.Player.RepairPrice = item.RepairPrice;
			Data = data;
			SaveProgress(Data);
		}

		public void ChangeIsMute(bool isMute)
		{
			GameData data = Data;
			data.Settings.IsAudioMute = isMute;
			Data = data;
			SaveProgress(Data);
		}

		protected abstract void Initialization();

		public abstract GameData LoadProgress();

		public abstract void SaveProgress(GameData gameData);

		public abstract void SetData(GameData newValue);

		public abstract GameData GetData();
	}
}
