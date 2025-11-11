using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class SausageInput
{
    public Vector2 MoveInput { get; private set; }

    public void SetInput(CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }
}