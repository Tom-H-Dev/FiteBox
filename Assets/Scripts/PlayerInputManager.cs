using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager instance;
    public PlayerInput playerActions;

    private void Awake()
    {
        if (instance != null )
            Destroy( instance );
        else instance = this;

        playerActions = new PlayerInput();
    }

    #region input map
    private void OnEnable()
    {
        playerActions.PlayerMovement.Enable();
    }
    private void OnDisable()
    {
        playerActions.PlayerMovement.Disable();
    }
    #endregion
}
