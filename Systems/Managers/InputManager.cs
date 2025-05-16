using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputManager : PersistentSignleton<InputManager> {

    [NonSerialized] public UnityEvent PlayerInteractPressedEvent = new();

    public InputSystem_Actions _playerActions;
    
    // Interact
    [NonSerialized] public bool PlayerInteractWasPressed = false;
    [NonSerialized] public bool PlayerInteractWasReleased = false;
    [NonSerialized] public bool PlayerInteractIsHeld = false;
    // Dash
    [NonSerialized] public bool PlayerDashWasPressed = false;
    [NonSerialized] public bool PlayerDashtWasReleased = false;
    [NonSerialized] public bool PlayerDashIsHeld = false;


    // internal
    private InputAction _interactAction;
    private InputAction _moveAction;
    private InputAction _dashAction;



    private void Start() {
        // separating into its smaller chunks first
        _playerActions = new();
        _playerActions.Enable();

        _interactAction = _playerActions.Player.Interact;
        _moveAction = _playerActions.Player.Move;
        _dashAction = _playerActions.Player.Dash;
    }

    private void Update() {
        PlayerInteractWasPressed = _interactAction.WasPressedThisFrame();
        PlayerInteractWasReleased = _interactAction.WasReleasedThisFrame();
        PlayerInteractIsHeld = _interactAction.IsPressed();

        PlayerDashWasPressed = _dashAction.WasPressedThisFrame();
        PlayerDashtWasReleased = _dashAction.WasReleasedThisFrame();
        PlayerDashIsHeld = _dashAction.IsPressed();


        if (PlayerInteractWasPressed == true) {
            PlayerInteractPressedEvent.Invoke();
        }
    }

    /// <summary>
    /// Returns the current input axis corresponding to the player controls - i.e. returns Vector2.right if user is pressing right/D
    /// </summary>
    /// <returns></returns>
    public Vector2 GetPlayerMovement() {
        Vector2 input;

        // get initial value
        input = _moveAction.ReadValue<Vector2>();

        return input;
    }

}
