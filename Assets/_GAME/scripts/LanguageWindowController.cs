using System;
using Assets.SimpleLocalization.Scripts;
using UnityEngine;
using UnityEngine.UI;

public enum Langs
{
	Unset,
	English,
	Russian,
	Turkish,
	German,
	French,
	Spanish
}

public class LanguageWindowController : MonoBehaviour
{
	public event Action<Langs> OnLangChanged;
	[SerializeField] private Button close;
	[SerializeField] private Button ruButton;
	[SerializeField] private Image ruImage;
	[SerializeField] private Button enButton;
	[SerializeField] private Image enImage;
	[SerializeField] private Button turkButton;
	[SerializeField] private Image tuImage;
	[SerializeField] private Button germButton;
	[SerializeField] private Image germImage;
	[SerializeField] private Button frButton;
	[SerializeField] private Image frImage;
	[SerializeField] private Button spButton;
	[SerializeField] private Image spImage;
	[SerializeField] private GameObject settings;

	void Start()
	{
		close.onClick.AddListener(ClosePressed);
		ruButton.onClick.AddListener(RuPressed);
		enButton.onClick.AddListener(EnPressed);
		germButton.onClick.AddListener(GermPressed);
		frButton.onClick.AddListener(FrPressed);
		spButton.onClick.AddListener(SpPressed);
		turkButton.onClick.AddListener(TuPressed);
	}



	private void OnDestroy()
	{
		close.onClick.RemoveListener(ClosePressed);
		ruButton.onClick.RemoveListener(RuPressed);
		enButton.onClick.RemoveListener(EnPressed);
		turkButton.onClick.RemoveListener(TuPressed);
		frButton.onClick.RemoveListener(FrPressed);
		spButton.onClick.RemoveListener(SpPressed);
		turkButton.onClick.RemoveListener(TuPressed);
	}


	public void ChooseLang(Langs lang)
	{
		LocalizationManager.Language = lang.ToString();
		OnLangChanged?.Invoke(lang);
		ChangeChoosesByLang(lang, Langs.Russian, ruImage);
		ChangeChoosesByLang(lang, Langs.English, enImage);
		ChangeChoosesByLang(lang, Langs.Turkish, tuImage);
		ChangeChoosesByLang(lang, Langs.French, frImage);
		ChangeChoosesByLang(lang, Langs.German, germImage);
		ChangeChoosesByLang(lang, Langs.Spanish, spImage);
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

	private void SpPressed() => ChooseLang(Langs.Spanish);

	private void FrPressed() => ChooseLang(Langs.French);

	private void GermPressed() => ChooseLang(Langs.German);
	private void ClosePressed() => settings.SetActive(false);
}
