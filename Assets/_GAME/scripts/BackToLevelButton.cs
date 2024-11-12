using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _GAME.scripts;
using _GAME.scripts.Architecture.Architecture.Persistanse_Service;
using _GAME.scripts.Architecture.Architecture.Services.ScenesService;
using Shop;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BackToLevelButton : MonoBehaviour
{
        [SerializeField] private Button closeButton;
        private LevelManager _levelManager;
        
        [Inject]
        private void Contruct( LevelManager levelManager)
        {
            _levelManager = levelManager;
        }
    
        private void Awake()
        {
            closeButton = GetComponent<Button>();
            closeButton.onClick.AddListener(Close);
        }

        private void OnDestroy()
        {
            closeButton.onClick.RemoveListener(Close);
        }

        private void Close()
        {
            this._levelManager.RestartCurrentLevel();
        }
}
