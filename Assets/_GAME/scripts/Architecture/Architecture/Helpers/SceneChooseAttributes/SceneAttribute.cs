using System;
using UnityEngine;

namespace _GAME.scripts.Architecture.Architecture.Helpers.SceneChooseAttributes
{
        [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
        public class SceneAttribute :  PropertyAttribute
        {
        }
}