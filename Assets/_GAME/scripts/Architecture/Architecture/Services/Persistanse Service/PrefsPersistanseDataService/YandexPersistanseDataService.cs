using System;
using _GAME.scripts.Architecture.Architecture.Persistanse_Service;
using Assets.SimpleLocalization.Scripts;
using UnityEngine;
using YG;



public class YandexPersistentDataService : IPersistanseDataService
{
	const string RU_CODE = "ru";
	const string EN_CODE = "en";
	const string TU_CODE = "tr";
	const string GE_CODE = "de";
	const string FR_CODE = "fr";
	const string SP_CODE = "es";
	protected override void Initialization()
	{
		YandexGame.GetDataEvent += GetLoad;
	}

	private void GetLoad()
	{
		SetData(YandexGame.savesData.data);
		Debug.Log("langCode: " + YandexGame.EnvironmentData.language);
		if (YandexGame.savesData.data.Settings.Language == Langs.Unset)
		{
			switch (YandexGame.EnvironmentData.language)
			{
				case RU_CODE:
					YandexGame.savesData.data.Settings.Language = Langs.Russian;
					break;
				case EN_CODE:
					YandexGame.savesData.data.Settings.Language = Langs.English;
					break;
				case TU_CODE:
					YandexGame.savesData.data.Settings.Language = Langs.Turkish;
					break;
				case GE_CODE:
					YandexGame.savesData.data.Settings.Language = Langs.German;
					break;
				case FR_CODE:
					YandexGame.savesData.data.Settings.Language = Langs.French;
					break;
				case SP_CODE:
					YandexGame.savesData.data.Settings.Language = Langs.Spanish;
					break;
				default:
					YandexGame.savesData.data.Settings.Language = Langs.English;
					break;
			}
			Debug.Log("DEFAULT LANG SET: " + YandexGame.savesData.data.Settings.Language.ToString());
		}

		InvokeDate();
	}

	public override GameData LoadProgress()
	{
		if (YandexGame.Instance)
		{
			YandexGame.LoadProgress();
		}

		return Data;
	}

	public override void SaveProgress(GameData gameData)
	{
		SetData(gameData);
		YandexGame.SaveProgress();
	}

	public override void SetData(GameData newValue)
	{
		YandexGame.savesData.data = newValue;
	}

	public override GameData GetData()
	{
		return YandexGame.savesData.data;
	}
}
