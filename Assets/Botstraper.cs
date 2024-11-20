using System;
using System.Collections;
using System.Collections.Generic;
using _GAME.scripts.Architecture.Architecture.Persistanse_Service;
using _GAME.scripts.Architecture.Architecture.Services.ScenesService;
using Assets.SimpleLocalization.Scripts;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class Bootstraper : MonoBehaviour
{
    [Scene] [SerializeField] private string firstScene;
    private IPersistanseDataService _persistanseDataService;
    private SceneLoader _sceneLoader;
    private LevelManager _levelManager;

    [Inject]
    private void Construct(IPersistanseDataService persistanseDataService, SceneLoader sceneLoader, LevelManager levelManager)
    {
        _persistanseDataService = persistanseDataService;
        _sceneLoader = sceneLoader;
        _levelManager = levelManager;
        _persistanseDataService.OnDataWasLoad += SetGameState;

    }

    private void SetGameState()
    {
        SetCurrentLevel();
        SetLanguageFromBack();
        LoadMainMenu();
    }

    private void SetCurrentLevel()
    {
        _levelManager.SetLevelIndex(_persistanseDataService.Data.Levels.CurrentLevelIndex);
    }

    private void LoadMainMenu()
    {
        _sceneLoader.Load(firstScene);
    }

    private void SetLanguageFromBack()
    {
        Langs lang = _persistanseDataService.Data.Settings.Language;
        LocalizationManager.Language = lang.ToString();
    }
}
