using System;
using _GAME.scripts.Architecture.Architecture.Persistanse_Service;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace _GAME.scripts
{
    public class MoneyView: MonoBehaviour
    {
        [SerializeField] private Text counterText;
        private IPersistanseDataService _persistanseDataService;

        [Inject]
        private void Construct(IPersistanseDataService persistanseDataService)
        {
            _persistanseDataService = persistanseDataService;
            RefreshCounterText();
        }

        private void Start()
        {
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
}