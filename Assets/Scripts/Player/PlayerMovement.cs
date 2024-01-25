using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    private PlayerInput _playerActions;
    private Rigidbody _rb;
    private Vector3 _moveInput;
    public bool canMove = true;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        if (_rb is null)
            Debug.LogError("Rigidbody is NULL!");
    }

    private void Start()
    {
        _playerActions = PlayerInputManager.instance.playerActions;
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            _moveInput = _playerActions.PlayerMovement.Movement.ReadValue<Vector3>();
            _rb.velocity = _moveInput * _speed;
        }
    }
}
