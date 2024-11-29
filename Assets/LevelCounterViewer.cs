using System;
using System.Collections;
using System.Collections.Generic;
using _GAME.scripts.Architecture.Architecture.Persistanse_Service;
using _GAME.scripts.Architecture.Architecture.Services.ScenesService;
using Assets.SimpleLocalization.Scripts;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LevelCounterViewer : MonoBehaviour
{
    private Text LevelText;
    private IPersistanseDataService _persistanseDataService;
    private LevelManager _levelManager;

    [Inject]
    private void Construct(LevelManager levelManager)
    {
        _levelManager = levelManager;
    }

    private void Start()
    {
        LevelText = GetComponent<Text>();
        LevelText.text = LocalizationManager.Localize("Common.Level", _levelManager.GetLevelIndex() + 1);
    }
}
