using System.Collections;
using System.Collections.Generic;
using _GAME.scripts.Architecture.Architecture;
using _GAME.scripts.Architecture.Architecture.Services.ScenesService;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class WinWindow : WindowBase
{
    [SerializeField] private Button nextLevelButton;
    private LevelManager _levelManager;
    

    [Inject]
    private void Construct(LevelManager levelManager)
    {
        _levelManager = levelManager;
        nextLevelButton.interactable = levelManager.isNextLevelAvailable();
        nextLevelButton.onClick.AddListener(() =>
        {
            _levelManager.GoToNextLevel();
        });
    }
    
}
