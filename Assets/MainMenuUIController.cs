using System.Collections;
using System.Collections.Generic;
using _GAME.scripts.Architecture.Architecture.Persistanse_Service;
using _GAME.scripts.Architecture.Architecture.Services.ScenesService;
using _GAME.scripts.Architecture.Architecture.Services.SoundService;
using _GAME.scripts.Architecture.Architecture.Services.SoundService.types;
using Assets.SimpleLocalization.Scripts;
using DG.Tweening;
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
    private SoundsService _soundsService;

    [Inject]
    private void Construct(LevelManager sceneLoader, IPersistanseDataService persistanseDataService, SoundsService soundsService)
    {
        _soundsService = soundsService;
        _sceneLoader = sceneLoader;
        _persistanseDataService = persistanseDataService;
    }
    
    void Start()
    {
        // mainMusic = FindObjectOfType<MainMusicController>();
        _soundsService.Play(SoundID.Main);
        Langs lang = _persistanseDataService.Data.Settings.Language;
        LocalizationManager.Language = lang.ToString();
        playButton.onClick.AddListener(OnPlayClick);
        playText.text = LocalizationManager.Localize("Common.Play", _sceneLoader.GetLevelIndex() + 1);
    }

    private void OnPlayClick()
    {
        _soundsService.Stop(SoundID.Main);
        _sceneLoader.RestartCurrentLevel();
    }
}
