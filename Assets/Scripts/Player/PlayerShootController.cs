using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootController : MonoBehaviour
{
    private PlayerInput _playerActions;
    private Vector3 _rotateInput;
    [SerializeField] private GameObject _turret;
    private bool _hasShot = false;
    
    private void Start()
    {
        _playerActions = PlayerInputManager.instance.playerActions;
        _playerActions.PlayerCombat.Shoot.performed += ctx => HandleShootSystem();
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

    private void HandleShootSystem()
    {
        if (!_hasShot)
        {
            Debug.Log("Shoot");
            _hasShot = true;
            StartCoroutine(ShootCooldown());
        }
    }

    private IEnumerator ShootCooldown()
    {
        yield return new WaitForSeconds(2);
        _hasShot = false;
    }
}
