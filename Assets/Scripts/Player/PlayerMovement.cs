using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPunCallbacks
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
            Debug.LogError("Rigidbody is " + _rb  + "!");
    }

    private void Start()
    {
        _playerActions = PlayerInputManager.instance.playerActions;
    }

    private void FixedUpdate()
    {
        if (canMove && photonView.IsMine)
        {
            _moveInput = _playerActions.PlayerMovement.Movement.ReadValue<Vector3>();
            _rb.velocity = _moveInput * _speed;
        }
    }
}
