using System;
using UnityEngine;

namespace _GAME.scripts.Architecture.Architecture.Services.InputService
{
    public abstract class IInputService: MonoBehaviour
    {
        public Action PausePresed;
        public virtual float MoveDirection { get; }
    }
}