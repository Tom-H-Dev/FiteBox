using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class InLobbyManager : MonoBehaviourPunCallbacks
{

    [Header("User Interface")]
    [Tooltip("The TextMeshPro text element where the name of lobby is in.")]
    [SerializeField] private TMP_Text _lobbyName;

    [Tooltip("The TextMeshPro text element where all the names of the players are set in a row to see what players are in the list.")]
    [SerializeField] private TMP_Text _userNames;

    [Tooltip("The button that is in the screen to start the game.")]
    [SerializeField] private Button _startGameButton;

    [SerializeField] private bool _debugMode = false;

    private void Start()
    {
        _lobbyName.text = PhotonNetwork.CurrentRoom.MaxPlayers + " Player Lobby";
        SetNamesOfLobbyAndConnectedUsers();
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
    public override void OnLeftRoom()
    {
        //We have left the Room, return back to the GameLobby
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
    }

    private void Update()
    {
        SetNamesOfLobbyAndConnectedUsers();
        UpdateUI();
    }

    private void SetNamesOfLobbyAndConnectedUsers()
    {
        _userNames.text = string.Empty;

        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            //Show if this player is a Master Client. There can only be one Master Client per Room so use this to define the authoritative logic etc.)
            string isMasterClient = (PhotonNetwork.PlayerList[i].IsMasterClient ? ": Host" : "");
            if (PhotonNetwork.NickName == PhotonNetwork.PlayerList[i].NickName)
            {
                _userNames.text = _userNames.text + "- " + PhotonNetwork.PlayerList[i].NickName + isMasterClient + " (You)" + "\n";
            }
            else
            {
                _userNames.text = _userNames.text + "- " + PhotonNetwork.PlayerList[i].NickName + isMasterClient + "\n";
            }
        }
    }

    private void UpdateUI()
    {
        //The lobby will be closed then there are enough players in the lobby
        if (PhotonNetwork.IsMasterClient)
        {
            //The lobby will be closed then there are enough players in the lobby
            if (PhotonNetwork.IsMasterClient)
            {
                if (!_debugMode)
                {
                    if (PhotonNetwork.PlayerList.Length == 2)
                    {
                        PhotonNetwork.CurrentRoom.IsVisible = false;
                        PhotonNetwork.CurrentRoom.IsOpen = false;
                        _startGameButton.interactable = true;
                    }
                    else
                    {
                        PhotonNetwork.CurrentRoom.IsVisible = true;
                        PhotonNetwork.CurrentRoom.IsOpen = true;
                        _startGameButton.interactable = false;
                    }
                }
                else
                {
                    PhotonNetwork.CurrentRoom.IsVisible = false;
                    PhotonNetwork.CurrentRoom.IsOpen = false;
                    _startGameButton.interactable = true;
                }

            }
        }
    }

    public void StartGameButton()
    {
        PhotonNetwork.LoadLevel("Tanks");
    }
}
