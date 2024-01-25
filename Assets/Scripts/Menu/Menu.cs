using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
}
