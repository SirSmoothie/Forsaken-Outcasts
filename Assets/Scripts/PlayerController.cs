using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public CharacterModel characterModel;
    public PlayerInput playerInput;
    public void Start()
    {
        //playerInput.actions.FindAction("Jump").performed += aContext => characterModel.Jump();
        //playerInput.actions.FindAction("Interact").performed += aContext => characterModel.Interact();
        
        playerInput.actions.FindAction("Movement").performed += OnMovementOnperformed;
        playerInput.actions.FindAction("Movement").canceled += OnMovementOnperformed;
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
}
