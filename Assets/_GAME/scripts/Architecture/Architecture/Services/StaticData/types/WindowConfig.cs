using UnityEngine;

namespace _GAME.scripts.Architecture.Architecture.Services.StaticData.types
{
    [CreateAssetMenu(fileName = "WindowConfig", menuName = "Create static data/Window")]
    public class WindowConfig: IConfig<WindowId>
    {
        public GameObject prefab;
    }
}