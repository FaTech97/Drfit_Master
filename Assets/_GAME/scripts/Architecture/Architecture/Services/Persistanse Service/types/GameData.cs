using System;
using System.Collections.Generic;
using Shop;
using UnityEngine.Serialization;

namespace _GAME.scripts.Architecture.Architecture.Persistanse_Service
{
	[Serializable]
	public class GameData
	{
		public SettingsData Settings = new SettingsData();
		public LevelsData Levels = new LevelsData();
		public PlayerData Player = new PlayerData();
	}

	[Serializable]
	public class SettingsData
	{
		public bool IsAudioMute = false;
		public Langs Language;
	}

	[Serializable]
	public class LevelsData
	{
		public int CurrentLevelIndex = 0;
	}

	[Serializable]
	public class PlayerData
	{
		public int Coins = 10000;
		public int PlayerHP = 3;
		public int MaxHp = 3;
		public int RepairPrice = 25;
		public ItemId[] BuysItemsIDs = new ItemId[0];
		public ItemId CurrectItemId = ItemId.DefaultItem;
	}

}
