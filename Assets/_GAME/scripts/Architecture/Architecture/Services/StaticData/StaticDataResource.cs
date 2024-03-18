using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _GAME.scripts.Architecture.Architecture.Services.StaticData
{
    public class StaticDataResource<T,K> where T: Enum where K : IConfig<T>
    {
        private string path;
        private Dictionary<T, K> _windowConfigs;

        public StaticDataResource(string path)
        {
            this.path = path;
            LoadResources();
            Debug.Log($"[QWINO] We found: {_windowConfigs.Count} on Resources/{this.path}");
        }
        
        public K Get(T id) =>
            _windowConfigs.TryGetValue(id, out K staticData)
                ? staticData
                : null;

        private void LoadResources()
        {
                _windowConfigs = Resources
                    .LoadAll<K>(path)
                    .ToDictionary(x => x.id, x => x);
        }
    }
}