using System;
using System.Linq;
using Shop;

namespace _GAME.scripts.Architecture.Architecture.Persistanse_Service
{
	public abstract class IPersistanseDataService
	{
		public event Action OnDataChanged;
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
			data.Player.PlayerHP = GameConfig.PlayerMaxHp;
			Data = data;
			SaveProgress(Data);
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
			// LoadProgress();
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

		public void SetItem(ItemId id)
		{
			GameData data = Data;
			var carList = data.Player.BuysItemsIDs.ToList();
			carList.Add(id);
			data.Player.BuysItemsIDs = carList.ToArray();
			data.Player.CurrectItemId = id;
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
