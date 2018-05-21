using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Minego;

public class GameManager : MonoBehaviour {
    private void Start()
    {
        //Screen.SetResolution(1280, 720, Screen.fullScreen);
    }
    public void GameStart()
    {
        GlobalState.Restart();
        SceneManager.LoadScene("Stage 1");
    }
}
