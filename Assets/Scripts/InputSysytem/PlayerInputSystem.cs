using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputSystem : MonoBehaviour
{
    [SerializeField, Range(0, 1f)] private float _directionThreshold = 0.9f;

    private PlayerInputActions _inputActions;

    public event Action RightDash;
    public event Action LeftDash;
    public event Action Jump;

    private Vector2 _primaryTocuhPos;
    private void Awake()
    {
        _inputActions = new PlayerInputActions();
        _inputActions.Player.Movement.performed += Movement_performed;
        _inputActions.Player.PrimaryContact.started += PrimaryContact_started;
        _inputActions.Player.PrimaryContact.canceled += PrimaryContact_canceled;
    }

    private void PrimaryContact_canceled(InputAction.CallbackContext context)
    {
        Vector2 endPosition = _inputActions.Player.PrimaryPosition.ReadValue<Vector2>();
        Vector2 direction = (endPosition - _primaryTocuhPos).normalized;
        Move(direction);
    }

    private void PrimaryContact_started(InputAction.CallbackContext context)
    {
        _primaryTocuhPos = _inputActions.Player.PrimaryPosition.ReadValue<Vector2>();
    }

    private void Movement_performed(InputAction.CallbackContext context)
    {
        Vector2 direction =  context.ReadValue<Vector2>();
        Move(direction);
    }

    private void Move(Vector2 direction)
    {
        if (Vector2.Dot(Vector2.up, direction) > _directionThreshold)
        {
            Jump?.Invoke();
        }
        else if (Vector2.Dot(Vector2.left, direction) > _directionThreshold)
        {
            LeftDash?.Invoke();
        }
        else if (Vector2.Dot(Vector2.right, direction) > _directionThreshold)
        {
            RightDash?.Invoke();
        }
    }

    private void OnEnable()
    {
        _inputActions?.Player.Enable();
    }

    private void OnDisable()
    {
        _inputActions?.Player.Disable();
    }
          
}
