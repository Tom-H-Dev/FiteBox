using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    private PlayerInput _playerActions;
    private Vector3 _moveInput;
    private void Start()
    {
        _playerActions = PlayerInputManager.instance.playerActions;
    }
    void Update()
    {
        HandleMovement();
    }
    private void HandleMovement()
    {
        _moveInput = _playerActions.PlayerMovement.Movement.ReadValue<Vector3>();

        //Move Front/Back
        transform.Translate(transform.forward * _moveInput.z * Time.deltaTime * 20f, Space.World);

        //Rotate Left/Right
        transform.Rotate(new Vector3(0, _moveInput.x * 14, 0) * Time.deltaTime * 6f, Space.Self);
    }
}
