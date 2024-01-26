using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurretAim : MonoBehaviourPunCallbacks, IPunObservable
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
        mainCam = FindObjectOfType<Camera>();
        _playerTurret = gameObject;
        _playerActions = PlayerInputManager.instance.playerActions;
        _playerActions.PlayerCombat.Shoot.performed += ctx => ShootTurret();
    }

    void Update()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }
        TurretRotaion();
        
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
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }
        if (!hasShot)
        {
            GameObject l_bullet = Instantiate(_bullet, _shootPosition.transform.position, transform.rotation);
            hasShot = true;
            StartCoroutine(ShotTimer());
        }
    }

    private IEnumerator ShotTimer()
    {
        yield return new WaitForSeconds(0.5f);
        hasShot = false;
    }

    #region IPunObservable implementation

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(hasShot);
        }
        else
        {
            // Network player, receive data
            this.hasShot = (bool)stream.ReceiveNext();
        }
    }

    #endregion
}
