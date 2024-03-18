using System;
using UnityEngine;

namespace _GAME.scripts.Architecture.Architecture.Services.StaticData
{
    public abstract class IConfig<T>: ScriptableObject where T: Enum 
    {
        public T id;
    }
}