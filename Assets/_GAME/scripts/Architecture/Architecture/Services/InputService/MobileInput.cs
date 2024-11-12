using UnityEngine;
using UnityEngine.UI;

namespace _GAME.scripts.Architecture.Architecture.Services.InputService
{
	public class MobileInput : IInputService
	{
		[SerializeField] private LongClickHandler leftMoveButton;
		[SerializeField] private LongClickHandler rightMoveButton;
		[SerializeField] private Button pauseButton;
		public override float MoveDirection => getDirection();

		private float getDirection()
		{
			if (leftMoveButton.pressing)
			{
				return -1;
			}

			if (rightMoveButton.pressing)
			{
				return 1;
			}

			return 0;
		}

		private void Start()
		{
			pauseButton.onClick.AddListener(() => PausePresed?.Invoke());
		}

		private void OnDestroy()
		{
			pauseButton.onClick.RemoveAllListeners();
		}
	}
}
