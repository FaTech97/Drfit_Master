using System;
using System.Collections;
using System.Collections.Generic;
using _GAME.scripts.Architecture.Architecture.Persistanse_Service;
using _GAME.scripts.Architecture.Architecture.Services.ScenesService;
using _GAME.scripts.Architecture.Architecture.Services.SoundService;
using Assets.SimpleLocalization.Scripts;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class Bootstraper : MonoBehaviour
{
    [Scene] [SerializeField] private string firstScene;
    private IPersistanseDataService _persistanseDataService;
    private SceneLoader _sceneLoader;
    private LevelManager _levelManager;
    private SoundsService _soundsService;

    [Inject]
    private void Construct(IPersistanseDataService persistanseDataService, SceneLoader sceneLoader,
        LevelManager levelManager, SoundsService soundsService)
    {
        _soundsService = soundsService;
        _persistanseDataService = persistanseDataService;
        _sceneLoader = sceneLoader;
        _levelManager = levelManager;
    }

    private async void Start()
    {
        await UnityServices.InitializeAsync();
        AnalyticsService.Instance.StartDataCollection();
        _persistanseDataService.OnDataWasLoad += SetGameState;
        if (Application.platform == RuntimePlatform.Android || Application.isEditor)
        {
            SetGameState();
        }
    }

    private void SetGameState()
    {
        Debug.Log("SetGameState");
        SetCurrentLevel();
        Debug.Log("SetGameState1");
        SetLanguageFromBack();
        Debug.Log("SetGameState2");
        LoadMainMenu();
        SetMuteSettings();
    }

    private void SetMuteSettings()
    {
        _soundsService.SetMuteForAll(_persistanseDataService.Data.Settings.IsAudioMute);
    }

    private void SetCurrentLevel()
    {
        _levelManager.SetLevelIndex(_persistanseDataService.Data.Levels.CurrentLevelIndex);
    }

    private void LoadMainMenu()
    {
        Debug.Log("FFFFFffffffffffffffffffffffffff");
        _sceneLoader.Load(firstScene);
    }

    private void SetLanguageFromBack()
    {
        Langs lang = _persistanseDataService.Data.Settings.Language;
        LocalizationManager.Language = lang.ToString();
    }
}