using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurretAim : MonoBehaviourPunCallbacks
{
    private GameObject _playerTurret;
    private Vector2 mousePOs;
    [SerializeField] private Camera mainCam;

    private PlayerInput _playerActions;
    float rotationFactorPerFrame = 15.0f;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject _shootPosition;

    private bool hasShot = false;

    void Start()
    {
        _playerTurret = gameObject;
        _playerActions = PlayerInputManager.instance.playerActions;
        _playerActions.PlayerCombat.Shoot.performed += ctx => ShootTurret();
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            TurretRotaion();
        }
    }

    private void TurretRotaion()
    {
        //TODO: Check if gamepad rotation or if keyboard is used and swap UI and the way of aiming so if looking with mouse and keyboard or turn with controller

        Vector2 mousePosition = _playerActions.PlayerMovement.MousePostition.ReadValue<Vector2>();
        Vector3 mouseViewportPosition = mainCam.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, mainCam.transform.position.y));
        Vector3 positionToLookAt;

        positionToLookAt.x = mouseViewportPosition.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = mouseViewportPosition.z;

        Quaternion currentRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt - transform.position);
        transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
    }

    private void ShootTurret()
    {
        if (!hasShot && photonView.IsMine)
        {
            GameObject l_bullet = Instantiate(_bullet, _shootPosition.transform.position, _shootPosition.transform.rotation);
            hasShot = true;
            StartCoroutine(ShotTimer());
        }
    }

    private IEnumerator ShotTimer()
    {
        yield return new WaitForSeconds(0.5f);
        hasShot = false;
    }
}
