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
    [SerializeField] private Text winText;
    private LevelManager _levelManager;
    

    [Inject]
    private void Construct(LevelManager levelManager)
    {
        _levelManager = levelManager;
        winText.text = "УРОВЕНЬ " + (_levelManager.GetLevelIndex() + 1) + "\nПРОЙДЕН!";
        nextLevelButton.interactable = levelManager.isNextLevelAvailable();
        nextLevelButton.onClick.AddListener(() =>
        {
            _levelManager.GoToNextLevel();
        });
    }
    
}
