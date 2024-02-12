using _GAME.scripts.Architecture.Architecture.Persistanse_Service;
using _GAME.scripts.Architecture.Architecture.Services.ScenesService;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _GAME.scripts
{
    public class PlayerInfoView: MonoBehaviour
    {
        [SerializeField] private Text counterText;
        [SerializeField] private Text LevelText;
        private IPersistanseDataService _persistanseDataService;
        private LevelManager _levelManager;

        [Inject]
        private void Construct(LevelManager levelManager, IPersistanseDataService persistanseDataService)
        {
            _persistanseDataService = persistanseDataService;
            _levelManager = levelManager;
            LevelText.text = $"Уровень {_levelManager.GetLevelIndex() + 1}";
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