using System;
using System.Collections;
using System.Collections.Generic;
using Assets.SimpleLocalization.Scripts;
using UnityEngine;
using UnityEngine.UI;

public static class Langs
{
	public static string English = "English";
	public static string Russian = "Russian";
	public static string Tyrkey = "Turkey";
}

public class LanguageWindowController : MonoBehaviour
{
	[SerializeField] private Button close;
	[SerializeField] private Button ruButton;
	[SerializeField] private Button enButton;
	[SerializeField] private Button turkButton;
	[SerializeField] private GameObject settings;
	// Start is called before the first frame update
	void Start()
	{
		close.onClick.AddListener(ClosePressed);
		ruButton.onClick.AddListener(ruPressed);
		enButton.onClick.AddListener(enPressed);
		turkButton.onClick.AddListener(tuPressed);
	}

	private void OnDestroy()
	{
		close.onClick.RemoveListener(ClosePressed);
		ruButton.onClick.RemoveListener(ruPressed);
		enButton.onClick.RemoveListener(enPressed);
		turkButton.onClick.RemoveListener(tuPressed);
	}

	private void enPressed()
	{
		LocalizationManager.Language = Langs.English;
	}
	private void ruPressed()
	{
		LocalizationManager.Language = Langs.Russian;
	}
	private void tuPressed()
	{
		LocalizationManager.Language = Langs.Tyrkey;
	}

	private void ClosePressed()
	{
		settings.SetActive(false);
	}

	public void Show()
	{
		settings.SetActive(true);
	}
}
