using System.Collections;
using System.Collections.Generic;
using _GAME.scripts.Architecture.Architecture.Persistanse_Service;
using _GAME.scripts.Architecture.Architecture.Services.ScenesService;
using Assets.SimpleLocalization.Scripts;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MainMenuUIController : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Text playText;
    private LevelManager _sceneLoader;
    private IPersistanseDataService _persistanseDataService;
    private MainMusicController mainMusic;

    [Inject]
    private void Construct(LevelManager sceneLoader, IPersistanseDataService persistanseDataService)
    {
        _sceneLoader = sceneLoader;
        _persistanseDataService = persistanseDataService;
    }
    
    void Start()
    {
        mainMusic = FindObjectOfType<MainMusicController>();
        mainMusic.Play();
        Langs lang = _persistanseDataService.Data.Settings.Language;
        LocalizationManager.Language = lang.ToString();
        playButton.onClick.AddListener(OnPlayClick);
        playText.text = LocalizationManager.Localize("Common.Play", _sceneLoader.GetLevelIndex() + 1);
    }

    private void OnPlayClick()
    {
        mainMusic.Stop();
        _sceneLoader.RestartCurrentLevel();
    }
}
