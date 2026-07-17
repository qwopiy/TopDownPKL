using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader", menuName = "ScriptableObjects/InputReader")]
public class InputReader : ScriptableObject, InputSystem_Actions.IPlayerActions
{
    private InputSystem_Actions gameInput;

    public event Action<Vector2> MovementChanged;


    void OnEnable()
    {
        if (gameInput == null)
        {
            gameInput = new InputSystem_Actions();
            gameInput.Player.SetCallbacks(this);
        }
        setPlayer();
    }
    void OnDisable()
    {
        gameInput.Player.Disable();
        //gameInput.UI.Disable();
        gameInput.Dispose();
    }

    public void setUI()
    {
        gameInput.Player.Disable();
        //gameInput.UI.Enable();
    }

    public void setPlayer()
    {
        gameInput.Player.Enable();
        //gameInput.UI.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementChanged?.Invoke(context.ReadValue<Vector2>());
    }
}
