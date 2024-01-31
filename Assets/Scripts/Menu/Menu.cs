using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public PlayerInfo playerInfo;
    public GameObject chooseName;
    public TMP_InputField nameInputField;
    public TextMeshProUGUI errorText;

    private void Start()
    {
        errorText.text = string.Empty;
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void SceneTransition(string l_sceneName)
    {
        SceneManager.LoadScene(l_sceneName);
    }

    public void ChoosePlayerName()
    {
        if (nameInputField.text == "")
        {
            errorText.text = "Please Enter A Name.";
        }
        else
        {
            playerInfo.playerName = nameInputField.text;
            playerInfo.playerWins = 0;
            playerInfo.playerKills = 0;
            playerInfo.playerDeaths = 0;
            playerInfo.playerKDRatio = 0;
            errorText.text = string.Empty;
            chooseName.SetActive(false);
        }
    }

    [Header("Join The Lobby By ID UI")]
    [SerializeField] private TMP_InputField _lobbyCodeInputField;
    [SerializeField] private Button _lobbyJoinButton;

    public void LobbyIDChange()
    {
        if (_lobbyCodeInputField.text != "")
            _lobbyJoinButton.interactable = true;
        else _lobbyJoinButton.interactable = false;
    }

    public void ControllerMenuNAvigate(GameObject l_gameObject)
    {
        //clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //Set a new selected object
        EventSystem.current.SetSelectedGameObject(l_gameObject);
    }
}
