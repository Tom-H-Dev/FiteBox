using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerController : MonoBehaviour
{
    private float _speed;
    private PlayerInput _playerActions;
    private Rigidbody _rb;
    private Vector3 _moveInput;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        if (_rb is null)
            Debug.LogError("Rigidbody is " + _rb + "!");
        _playerActions = PlayerInputManager.instance.playerActions;
    }
    private void FixedUpdate()
    {
        _moveInput = _playerActions.PlayerMovement.Movement.ReadValue<Vector3>();
        Vector3 l_rotateInput = _playerActions.PlayerMovement.Movement.ReadValue<Vector3>();
        
        //Vector3 l_moveInput = new Vector3(_moveInput.x, , _moveInput.z);  //If use _rb.velocyity.y the player will move at mach 999 downwards and bread the floor
        _rb.velocity = _moveInput * _speed;
        transform.Rotate(new Vector3(0, 14 * l_rotateInput.x, 0) * Time.deltaTime * 4.5f, Space.Self);

    }
}
