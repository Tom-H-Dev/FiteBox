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
        //_playerActions = new Player();
        _rb = GetComponent<Rigidbody>();
        if (_rb is null)
            Debug.LogError("Rigidbody is NULL!");
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            _moveInput = _playerActions.PlayerMovement.Movement.ReadValue<Vector3>();
            _rb.velocity = _moveInput * _speed;
        }
    }



    #region input map
    private void OnEnable()
    {
        _playerActions.PlayerMovement.Enable();
    }
    private void OnDisable()
    {
        _playerActions.PlayerMovement.Disable();
    }
    #endregion
}
