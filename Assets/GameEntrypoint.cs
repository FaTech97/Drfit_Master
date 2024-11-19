using System.Collections;
using System.Collections.Generic;
using _GAME.scripts.Architecture.Architecture.Services.ScenesService;
using Assets.SimpleLocalization.Scripts;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameEntrypoint : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Text playText;
    private LevelManager _sceneLoader;

    [Inject]
    private void Construct(LevelManager sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }
    
    void Start()
    {
        playButton.onClick.AddListener(OnPlayClick);
        playText.text = LocalizationManager.Localize("Common.Play", _sceneLoader.GetLevelIndex() + 1);
    }

    private void OnPlayClick()
    {
        _sceneLoader.RestartCurrentLevel();
    }
}
