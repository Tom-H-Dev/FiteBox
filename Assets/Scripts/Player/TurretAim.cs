using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurretAim : MonoBehaviour
{
    private GameObject _playerTurret;
    private Vector2 mousePOs;
    [SerializeField] private Camera mainCam;

    private PlayerInput _playerActions;
    
    void Start()
    {
        _playerTurret = gameObject;
        _playerActions = PlayerInputManager.instance.playerActions;
    }

    void Update()
    {
        //mousePOs = PlayerInputManager.instance.playerActions.PlayerMovement.MousePostition.ReadValue<Vector2>();
        //Vector2 MousePosition = Camera.main.ScreenToWorldPoint(mousePOs); 

        //_playerTurret.transform.LookAt(PlayerInputManager.instance.playerActions.PlayerMovement.MousePostition.ReadValue<Vector2>());
        TurretRotaion();
    }
    float rotationFactorPerFrame = 15.0f;
    private void TurretRotaion()
    {
        //var mousePosition = _playerActions.PlayerMovement.MousePostition.ReadValue<Vector2>();
        //var mousePositionZ = mainCam.farClipPlane * .5f;

        //var mouseWorldPosition = mainCam.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, mousePositionZ)); // _camera.ScreenToViewportPoint(mousePosition);

        //// Get the angle between the points
        //// Use the x and z from the object/mouse, since we're looking along the y axis
        //var angle = AngleBetweenTwoPoints(new Vector2(transform.position.x, transform.position.z), new Vector2(mouseWorldPosition.x, mouseWorldPosition.z));

        //transform.rotation = Quaternion.Euler(new Vector3(0f, -angle, 0f));


        // We're getting a Vector2, whereas we will need a Vector3
        // Get a z value based on camera, and include it in a Vector3
        Vector2 mousePosition = _playerActions.PlayerMovement.MousePostition.ReadValue<Vector2>();

        var mousePositionZ = mainCam.farClipPlane * .5f;

        Vector3 mouseViewportPosition = mainCam.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, mainCam.transform.position.y));

        Vector3 positionToLookAt;

        positionToLookAt.x = mouseViewportPosition.x;
        positionToLookAt.y = 0.0f;
        //positionToLookAt.z = currentMovement.z;
        positionToLookAt.z = mouseViewportPosition.z;

        Quaternion currentRotation = transform.rotation;

        Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt - transform.position);
        transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
    }

    float AngleBetweenTwoPoints(Vector2 a, Vector2 b)
    {
        return Mathf.Atan2(b.y - a.y, b.x - a.x) * Mathf.Rad2Deg;
    }
}
