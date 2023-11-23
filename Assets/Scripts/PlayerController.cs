using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : NetworkBehaviour
{
    public CharacterModel characterModel;
    public PlayerInput playerInput;
    public void Start()
    {
        if (!IsOwner) return;
        //playerInput.actions.FindAction("Jump").performed += aContext => characterModel.Jump();
        //playerInput.actions.FindAction("Interact").performed += aContext => characterModel.Interact();
        
        playerInput.actions.FindAction("Movement").performed += OnMovementOnperformed;
        playerInput.actions.FindAction("Movement").canceled += OnMovementOnperformed;

        playerInput.actions.FindAction("CameraMovement").performed += OnCamMovementOnperformed;
        playerInput.actions.FindAction("CameraMovement").canceled += OnCamMovementOnperformed;
        
        playerInput.actions.FindAction("Jump").performed += aContext => characterModel.Jump();
    }

    private void OnMovementOnperformed(InputAction.CallbackContext aContext)
    {
        if (aContext.phase == InputActionPhase.Performed)
        {
            characterModel.MoveDirection(aContext.ReadValue<Vector2>());
        }
        if (aContext.phase == InputActionPhase.Canceled)
        {
            characterModel.MoveDirection(Vector2.zero);
        }
    }
    
    private void OnCamMovementOnperformed(InputAction.CallbackContext aContext)
    {
        if (aContext.phase == InputActionPhase.Performed)
        {
            characterModel.CamDirection(aContext.ReadValue<Vector2>());
        }
        if (aContext.phase == InputActionPhase.Canceled)
        {
            characterModel.CamDirection(Vector2.zero);
        }
    }
}
