using UnityEngine;

namespace StarterAssets
{
    public class UICanvasControllerInput : MonoBehaviour
    {

        [Header("Output")]
        public StarterAssetsInputs starterAssetsInputs;

        public void VirtualMoveInput(Vector2 virtualMoveDirection)
        {
            starterAssetsInputs.MoveInput(virtualMoveDirection);
        }

        public void VirtualLookInput(Vector2 virtualLookDirection)
        {
            starterAssetsInputs.LookInput(virtualLookDirection);
        }

        public void VirtualInteractInput(bool virtualJumpState)
        {
            starterAssetsInputs.InteractInput(virtualJumpState);
        }

        public void VirtualFlashInput(bool virtualSprintState)
        {
            starterAssetsInputs.FlashInput(virtualSprintState);
        }
        
    }

}
