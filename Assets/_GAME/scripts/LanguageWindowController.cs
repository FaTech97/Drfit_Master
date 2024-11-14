using System;
using System.Collections;
using System.Collections.Generic;
using Assets.SimpleLocalization.Scripts;
using Assets.SimpleLocalization.Scripts;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public enum Langs
{
	English,
	Russian,
	Turkish
}

public class LanguageWindowController : MonoBehaviour
{
	public event Action<Langs> OnLangChanged;
	[SerializeField] private Button close;
	[SerializeField] private Button ruButton;
	[SerializeField] private Button enButton;
	[SerializeField] private Button turkButton;
	[SerializeField] private GameObject settings;
	[SerializeField] private Image ruImage;
	[SerializeField] private Image enImage;
	[SerializeField] private Image tuImage;

	void Start()
	{
		close.onClick.AddListener(ClosePressed);
		ruButton.onClick.AddListener(RuPressed);
		enButton.onClick.AddListener(EnPressed);
		turkButton.onClick.AddListener(TuPressed);
	}

	private void OnDestroy()
	{
		close.onClick.RemoveListener(ClosePressed);
		ruButton.onClick.RemoveListener(RuPressed);
		enButton.onClick.RemoveListener(EnPressed);
		turkButton.onClick.RemoveListener(TuPressed);
	}


	public void ChooseLang(Langs lang)
	{
		LocalizationManager.Language = lang.ToString();
		OnLangChanged?.Invoke(lang);
		ChangeChoosesByLang(lang, Langs.Russian, ruImage);
		ChangeChoosesByLang(lang, Langs.English, enImage);
		ChangeChoosesByLang(lang, Langs.Turkish, tuImage);
	}

	public void ShowLangPage()
	{
		settings.SetActive(true);
	}

	private void ChangeChoosesByLang(Langs currentLang, Langs buttonLang, Image image)
	{
		var color = image.color;
		var transparency = currentLang == buttonLang ? 0.45f : 0.2f;
		image.color = new Color(color.r, color.g, color.b, transparency);
	}

	private void EnPressed() => ChooseLang(Langs.English);
	private void RuPressed() => ChooseLang(Langs.Russian);
	private void TuPressed() => ChooseLang(Langs.Turkish);
	private void ClosePressed() => settings.SetActive(false);
}
