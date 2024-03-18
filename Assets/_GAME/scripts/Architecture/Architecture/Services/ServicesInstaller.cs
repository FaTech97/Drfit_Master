using System.Collections.Generic;
using _GAME.scripts.Architecture.Architecture.Persistanse_Service;
using _GAME.scripts.Architecture.Architecture.Persistanse_Service.PrefsPersistanseDataService;
using _GAME.scripts.Architecture.Architecture.Services.AdService;
using _GAME.scripts.Architecture.Architecture.Services.ScenesService;
using _GAME.scripts.Architecture.Architecture.Services.StaticData;
using UnityEngine;
using YG;
using Zenject;

namespace _GAME.scripts.Architecture.Architecture.Services
{
    public class ServicesInstaller: MonoInstaller
    {
        [SerializeField] private GameObject YaObject;
        [SerializeField] private SceneLoader sceneLoader;
        [SerializeField] private List<Level> levelsList;
    
        public override void InstallBindings()
        {
            BindUiService();
            BindSceneService();
            BindPersistanseService();
            BindStaticDataServoce();
            BindAdService();
        }

        private void BindStaticDataServoce()
        {
            Container.Bind<StaticDataService>().AsSingle().NonLazy();
        }

        private void BindAdService()
        {
            Container.Bind<YandexGame>().FromComponentInNewPrefab(YaObject).AsSingle().NonLazy();
            Container.Bind<IAdService>().To<YandexAdService>().AsSingle();
        }

        private void BindSceneService()
        {
            Container.Bind<SceneLoader>().FromComponentInNewPrefab(sceneLoader).AsSingle().NonLazy();
            // LevelManager levelManager = new LevelManager(levelsList);
            Container.Bind<LevelManager>().AsSingle().WithArguments(levelsList);
        }

        private void BindPersistanseService()
        {
            Container.Bind<IPersistanseDataService>().To<YandexPersistentDataService>().AsSingle();
        }

        private void BindUiService()
        {
            Container.Bind<UIFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<WindowService>().AsSingle();
            
        }
    }
}