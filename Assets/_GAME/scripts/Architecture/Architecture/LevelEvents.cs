using System;

namespace _GAME.scripts.Architecture.Architecture
{
    class LevelEvents
    {
        // For example events
        public event Action OnAllItemsGets;
        public void AllItemsWasGet()
        {
            OnAllItemsGets?.Invoke();
        }
    }
}