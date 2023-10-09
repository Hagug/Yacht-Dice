using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start4PGame : MonoBehaviour
{
    public void OnClickButton()
    {
        SceneManager.LoadScene("GameScene");
        TurnScript.gameMode = 4;
    }
}
