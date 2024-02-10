using UnityEngine;
using Zenject;

namespace _GAME.scripts.Architecture
{
    public class UIInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            BindUiFactory();
            BindUiService();
        }

        private void BindUiService()
        {
            Container.BindInterfacesAndSelfTo<WindowService>().AsSingle();
        }

        private void BindUiFactory()
        {
            Container.Bind<UIFactory>().AsSingle();
        }
    }
}