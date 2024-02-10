using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _GAME.scripts.Architecture.Architecture
{
    public class LevelStartInstaller: MonoInstaller
    {
        private LevelEvents _events;
        private WindowService _windowService;

        [Inject]
        private void Construct(WindowService windowService)
        {
            _windowService = windowService;
            InstantiateCanvas();
        }
   
        public override void InstallBindings()
        {
            Container.Bind<LevelEvents>().AsSingle().NonLazy();
        }
   
        private void InstantiateCanvas()
        {
            GameObject go = Instantiate(new GameObject("UI Root"));
            Canvas canvas = go.AddComponent<Canvas>();
            go.AddComponent<GraphicRaycaster>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            RectTransform rectTransform = go.GetComponent<RectTransform>();
            rectTransform.localPosition = Vector3.zero;
            _windowService.SetRootObject(go.transform);
        }
    }
}