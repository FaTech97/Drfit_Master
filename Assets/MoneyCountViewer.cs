using System.Collections;
using System.Collections.Generic;
using _GAME.scripts.Architecture.Architecture.Persistanse_Service;
using _GAME.scripts.Architecture.Architecture.Services.ScenesService;
using Assets.SimpleLocalization.Scripts;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MoneyCountViewer : MonoBehaviour
{
    private Text counterText;
    private IPersistanseDataService _persistanseDataService;

    [Inject]
    private void Construct(IPersistanseDataService persistanseDataService)
    {
        _persistanseDataService = persistanseDataService;
    }

    private void Start()
    {
        counterText = GetComponentInChildren<Text>();
        RefreshCounterText();
        _persistanseDataService.OnDataChanged += RefreshCounterText;
    }

    private void OnDestroy()
    {
        _persistanseDataService.OnDataChanged -= RefreshCounterText;
    }

    private void RefreshCounterText()
    {
        counterText.text = _persistanseDataService.Data.Player.Coins.ToString();
    }
}
