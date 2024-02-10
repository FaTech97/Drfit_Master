using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _GAME.scripts.Architecture.types
{
    public class CloseWindowWhenClick: MonoBehaviour
    {
        public WindowId id;
        private Button _button;
        private WindowService windowService;

        [Inject]
        public void Construct(WindowService windowService)
        {
            this.windowService = windowService;
        }

        // Start is called before the first frame update
        void Start()
        {
            _button = GetComponent<Button>();
            if (_button != null)
                _button.onClick.AddListener(Close);
        }

        private void OnDestroy()
        {
            if (_button != null)
                _button.onClick.RemoveListener(Close);
        }

        public void Close()
        {
            windowService.Close(id);
        }
    }
}