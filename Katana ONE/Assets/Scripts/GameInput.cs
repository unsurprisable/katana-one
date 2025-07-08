using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    private PlayerInputActions playerInputActions;

    public event EventHandler OnJumpPressed;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }


        playerInputActions = new PlayerInputActions();

        playerInputActions.Player.Enable();
        playerInputActions.Player.Jump.performed += (ctx) => OnJumpPressed?.Invoke(this, EventArgs.Empty);
    }

    public float GetHorizontalMovement()
    {
        return playerInputActions.Player.Move.ReadValue<Vector2>().x;
    }
    public float GetVerticalMovement()
    {
        return playerInputActions.Player.Move.ReadValue<Vector2>().y;
    }
}
