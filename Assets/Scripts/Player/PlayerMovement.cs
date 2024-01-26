using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPun
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
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }
        if (canMove)
        {
            _moveInput = _playerActions.PlayerMovement.Movement.ReadValue<Vector3>();
            //Vector3 l_moveInput = new Vector3(_moveInput.x, , _moveInput.z);  //If use _rb.velocyity.y the player will move at mach 999 downwards and bread the floor
            _rb.velocity = _moveInput * _speed;
        }
    }
}
