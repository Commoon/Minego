using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Minego;

public class GameManager : MonoBehaviour
{
    public UnityEngine.UI.Button ButtonStart;
    public UnityEngine.UI.Button ButtonAbout;

    private void Start()
    {
        SetUI(ButtonStart, 0f, 0f, 300f, 100f, 50);
        SetUI(ButtonAbout, 0f, -139f, 300f, 100f, 42);
    }

    private void SetUI(UnityEngine.UI.Button button, float posX, float posY, float width, float height, int fontSize)
    {
        button.targetGraphic.rectTransform.anchoredPosition = new Vector2(posX / 1280 * Screen.width, posY / 720 * Screen.height);
        button.targetGraphic.rectTransform.sizeDelta = new Vector2(width / 1280 * Screen.width, height / 720 * Screen.height);
        button.GetComponentInChildren<Text>().fontSize = fontSize * Screen.height / 720;
    }

    public void GameStart()
    {
        GlobalState.Restart();
        SceneManager.LoadScene("Stage 1");
    }

    public void AboutUs()
    {
        Application.OpenURL("https://vilja.itch.io/Minego");
    }
}
