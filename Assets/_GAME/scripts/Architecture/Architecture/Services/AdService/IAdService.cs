using System;
using Unity.VisualScripting;

namespace _GAME.scripts.Architecture.Architecture.Services.AdService
{
	public abstract class IAdService
	{
		public abstract void ShawReward(int id, Action action);
	}
}
