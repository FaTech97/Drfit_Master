using UnityEngine;

namespace _GAME.scripts.Architecture.types
{
    [CreateAssetMenu(fileName = "WindowConfig", menuName = "Create static data/Window")]
    public class WindowConfig: ScriptableObject
    {
        public WindowId id;
        public GameObject prefab;
    }
}