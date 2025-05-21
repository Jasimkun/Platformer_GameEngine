using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManger : MonoBehaviour
{
     public TMP_InputField inputField;
     public Button gameStartButton;
    // Start is called before the first frame update
    void Start()
    {
        gameStartButton.onClick.AddListener(OnGameStartButtonCliked);
    
    }

    private void OnGameStartButtonCliked()
    {
        string playerName = inputField.text;
        if (string.IsNullOrEmpty(playerName))
        {
            Debug.Log("플레이어 이름이 없어!");
            return;
        }
        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.Save();
        Debug.Log("플레이어 이름 저장 됨 : " + playerName);
        SceneManager.LoadScene("Level_1");
    }
}
