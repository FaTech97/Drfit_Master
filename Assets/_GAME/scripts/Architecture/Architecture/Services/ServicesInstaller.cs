using System.Collections.Generic;
using _GAME.scripts.Architecture.Architecture.Persistanse_Service;
using _GAME.scripts.Architecture.Architecture.Persistanse_Service.PrefsPersistanseDataService;
using _GAME.scripts.Architecture.Architecture.Services.ScenesService;
using UnityEngine;
using Zenject;

namespace _GAME.scripts.Architecture.Architecture.Services
{
    public class ServicesInstaller: MonoInstaller
    {
        [SerializeField] private SceneLoader sceneLoader;
        [SerializeField] private List<Level> levelsList;
    
        public override void InstallBindings()
        {
            BindUiService();
            BindSceneService();
            BindPersistanseService();
        }

        private void BindSceneService()
        {
            Container.Bind<SceneLoader>().FromComponentInNewPrefab(sceneLoader).AsSingle().NonLazy();
            // LevelManager levelManager = new LevelManager(levelsList);
            Container.Bind<LevelManager>().AsSingle().WithArguments(levelsList);
        }

        private void BindPersistanseService()
        {
            Container.Bind<IPersistanseDataService>().To<PrefsPersistanseDataService>().AsSingle();
        }

        private void BindUiService()
        {
            Container.Bind<UIFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<WindowService>().AsSingle();
            
        }
    }
}