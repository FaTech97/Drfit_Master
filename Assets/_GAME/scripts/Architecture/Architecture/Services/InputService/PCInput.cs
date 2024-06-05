using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _GAME.scripts.Architecture.Architecture.Services.InputService
{
    public class PCInput: IInputService
    {
        [SerializeField] private LongClickHandler leftMoveButton;
        [SerializeField] private LongClickHandler rightMoveButton;
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
            
            return Input.GetAxis("Horizontal");
        }
    }
}