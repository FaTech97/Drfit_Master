using System;
using System.Collections;
using System.Collections.Generic;
using Assets.SimpleLocalization.Scripts;
using UnityEngine;
using UnityEngine.UI;

public enum Langs
{
	English,
	Russian,
	Turkey
}

public class LanguageWindowController : MonoBehaviour
{
	public event Action<Langs> OnLangChanged;
	[SerializeField] private Button close;
	[SerializeField] private Button ruButton;
	[SerializeField] private Button enButton;
	[SerializeField] private Button turkButton;
	[SerializeField] private GameObject settings;
	private Image _ruImage;
	private Image _enImage;
	private Image _tuImage;

	void Start()
	{
		close.onClick.AddListener(ClosePressed);
		ruButton.onClick.AddListener(ruPressed);
		enButton.onClick.AddListener(enPressed);
		turkButton.onClick.AddListener(tuPressed);
		_ruImage = ruButton.GetComponent<Image>();
		_enImage = enButton.GetComponent<Image>();
		_tuImage = turkButton.GetComponent<Image>();
		ChooseLang(Langs.English);
	}

	private void OnDestroy()
	{
		close.onClick.RemoveListener(ClosePressed);
		ruButton.onClick.RemoveListener(ruPressed);
		enButton.onClick.RemoveListener(enPressed);
		turkButton.onClick.RemoveListener(tuPressed);
	}


	public void ChooseLang(Langs lang)
	{
		LocalizationManager.Language = lang.ToString();
		ChangeChoosesByLang(lang, Langs.Russian, _ruImage);
		ChangeChoosesByLang(lang, Langs.English, _enImage);
		ChangeChoosesByLang(lang, Langs.Turkey, _tuImage);
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

	private void enPressed() => ChooseLang(Langs.English);
	private void ruPressed() => ChooseLang(Langs.Russian);
	private void tuPressed() => ChooseLang(Langs.Turkey);
	private void ClosePressed() => settings.SetActive(false);
}
