using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    private PlayerInputActions playerInputActions;
    public event EventHandler OnInteractAction;

    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;

    public enum Binding
    {
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight,
        Interact,
        InteractAlternate,
        Pause
    }



    private void Awake()
    {
        Instance = this;

        playerInputActions = new PlayerInputActions();
        if (PlayerPrefs.HasKey("moveBindings"))
        {
            playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString("moveBindings"));
        }

        playerInputActions.Enable();



        playerInputActions.Player.Interact.performed += InteractOnPerformed;

        playerInputActions.Player.InteractAlternate.performed += InteractAlternateOnPerformed;

        playerInputActions.Player.Pause.performed += PausePerformed;
    }

    private void OnDestroy()
    {
        playerInputActions.Player.Interact.performed -= InteractOnPerformed;
        playerInputActions.Player.InteractAlternate.performed -= InteractAlternateOnPerformed;
        playerInputActions.Player.Pause.performed -= PausePerformed;

        playerInputActions.Dispose();
    }

    private void PausePerformed(InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternateOnPerformed(InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractOnPerformed(InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementNormalized()
    {
        Vector2 input = playerInputActions.Player.Move.ReadValue<Vector2>();

        return input.normalized;
    }

    public string GetBindingText(Binding binding)
    {
        switch (binding)
        {
            default:
            case Binding.Interact:
                return playerInputActions.Player.Interact.bindings[0].ToDisplayString();
            case Binding.InteractAlternate:
                return playerInputActions.Player.InteractAlternate.bindings[0].ToDisplayString();
            case Binding.Pause:
                return playerInputActions.Player.Pause.bindings[0].ToDisplayString();
            case Binding.MoveUp:
                return playerInputActions.Player.Move.bindings[1].ToDisplayString();
            case Binding.MoveDown:
                return playerInputActions.Player.Move.bindings[2].ToDisplayString();
            case Binding.MoveLeft:
                return playerInputActions.Player.Move.bindings[3].ToDisplayString();
            case Binding.MoveRight:
                return playerInputActions.Player.Move.bindings[4].ToDisplayString();
        }
    }

    public void RebindBinding(Binding binding, Action OnActionRebound)
    {
        playerInputActions.Player.Disable();

        InputAction action;
        int bindingIndex;

        switch (binding)
        {
            default:
            case Binding.MoveUp:
                action = playerInputActions.Player.Move;
                bindingIndex = 1;
                break;
            case Binding.MoveDown:
                action = playerInputActions.Player.Move;
                bindingIndex = 2;
                break;
            case Binding.MoveLeft:
                action = playerInputActions.Player.Move;
                bindingIndex = 3;
                break;
            case Binding.MoveRight:
                action = playerInputActions.Player.Move;
                bindingIndex = 4;
                break;
            case Binding.Interact:
                action = playerInputActions.Player.Interact;
                bindingIndex = 0;
                break;
            case Binding.InteractAlternate:
                action = playerInputActions.Player.Interact;
                bindingIndex = 0;
                break;
            case Binding.Pause:
                action = playerInputActions.Player.Pause;
                bindingIndex = 0;
                break;

        }

        action.PerformInteractiveRebinding(bindingIndex).OnComplete(callback =>
        {
            callback.Dispose();
            playerInputActions.Player.Enable();
            OnActionRebound();

            PlayerPrefs.SetString("moveBindings", playerInputActions.SaveBindingOverridesAsJson());
            PlayerPrefs.Save();
        }).Start();
    }

}
