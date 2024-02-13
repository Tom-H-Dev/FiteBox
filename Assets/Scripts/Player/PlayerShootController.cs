using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootController : MonoBehaviourPunCallbacks
{
    private PlayerInput _playerActions;
    private Vector3 _rotateInput;
    [SerializeField] private GameObject _turret;
    private bool _hasShot = false;
    [SerializeField] private Transform _shootPos;
    [SerializeField] private GameObject Bullet;
    
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
            _hasShot = true;
            PhotonNetwork.Instantiate("Bullet", _shootPos.position, _shootPos.rotation, 0);
            Debug.Log("Shoot");
            StartCoroutine(ShootCooldown());
        }
    }

    private void InstatiateBulletOverNetwork()
    {

    }

    private IEnumerator ShootCooldown()
    {
        yield return new WaitForSeconds(2);
        _hasShot = false;
    }
}
