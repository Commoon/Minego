using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Minego;

public class FinalManager : MonoBehaviour
{
    public GameObject Player1;
    public GameObject Player2;
    public float ShowDelay = 1f;
    public Text TextScore1;
    public Text TextScore2;
    public UnityEngine.UI.Button ButtonRestart;
    public Text TextMore;

    private void SetUI(Text text, float posX, float posY, float width, float height, int fontSize)
    {
        text.rectTransform.anchoredPosition = new Vector2(posX / 1280 * Screen.width, posY / 720 * Screen.height);
        text.rectTransform.sizeDelta = new Vector2(width / 1280 * Screen.width, height / 720 * Screen.height);
        text.fontSize = fontSize * Screen.height / 720;
    }

    private void SetUI(UnityEngine.UI.Button button, float posX, float posY, float width, float height, int fontSize)
    {
        button.targetGraphic.rectTransform.anchoredPosition = new Vector2(posX / 1280 * Screen.width, posY / 720 * Screen.height);
        button.targetGraphic.rectTransform.sizeDelta = new Vector2(width / 1280 * Screen.width, height / 720 * Screen.height);
        button.GetComponentInChildren<Text>().fontSize = fontSize * Screen.height / 720;
    }

    private void Start()
    {
        Invoke("ShowLoser", ShowDelay);
        SetUI(TextScore1, -287.64f, 84f, 300f, 200f, 50);
        SetUI(TextScore2, 287.64f, 84f, 300f, 200f, 50);
        SetUI(ButtonRestart, 0f, -38f, 300f, 100f, 50);
        SetUI(TextMore, 0f, 258f, 800f, 50f, 42);
        TextScore1.text = string.Format("1P\n{0:0000}", GlobalState.Score1);
        TextScore2.text = string.Format("2P\n{0:0000}", GlobalState.Score2);
    }

    private void ShowLoser()
    {
        var loser = GlobalState.Score1 > GlobalState.Score2 ? Player2 : GlobalState.Score1 < GlobalState.Score2 ? Player1 : null;
        if (loser != null)
        {
            var anim = loser.GetComponent<Animator>();
            anim.SetTrigger("Die");
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Menu");
    }
}