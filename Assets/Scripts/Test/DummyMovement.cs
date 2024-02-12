using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyMovement : MonoBehaviour
{
    private PlayerInput _playerActions;
    private Vector3 _rotateInput;
    [SerializeField] private GameObject _turret;
    private void Start()
    {
        _playerActions = PlayerInputManager.instance.playerActions;
    }
    void Update()
    {
        HandleTurretRotation();
    }
    private void HandleTurretRotation()
    {
        _rotateInput = _playerActions.PlayerCombat.RotateTurret.ReadValue<Vector2>();

        _turret.transform.Rotate(new Vector3(0, _rotateInput.x * 14, 0) * Time.deltaTime * 6f, Space.Self);
    }
}
