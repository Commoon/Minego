using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum StageStatus
{
    Pending,
    Started,
    Over
}

public class StageManager : MonoBehaviour {

    public string StageName;
    public Player Player1;
    public Player Player2;
    public Text TextScore1;
    public Text TextScore2;
    public Text TextStageName;
    public Text TextElapsedTime;
    public Text TextWinMessage;
    public Text TextContinue;
    public GameOver UIGameOver;

    private StageStatus _status = StageStatus.Pending;
    [HideInInspector] public StageStatus Status {
        get { return _status; }
        private set
        {
            if (value == StageStatus.Over)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
            _status = value;
        }
    }

    private void SetUI(Text text, float posX, float posY, float width, float height, int fontSize)
    {
        text.rectTransform.anchoredPosition = new Vector2(posX / 1280 * Screen.width, posY / 720 * Screen.height);
        text.rectTransform.sizeDelta = new Vector2(width / 1280 * Screen.width, height / 720 * Screen.height);
        text.fontSize = fontSize * Screen.height / 720;
    }

    void Start () {
        TextStageName.text = string.Format("Stage {0}", StageName);
        SetUI(TextScore1, 20f, 0f, 140f, 30f, 26);
        SetUI(TextScore2, -20f, 0f, 140f, 30f, 26);
        SetUI(TextStageName, -124f, 0f, 240f, 30f, 26);
        SetUI(TextElapsedTime, 124f, 0f, 160f, 30f, 26);
        SetUI(TextWinMessage, 0f, 40f, 500f, 100f, 48);
        SetUI(TextContinue, 0f, -40f, 260f, 30f, 24);
        this.Status = StageStatus.Started;
    }

    void Update () {
        var time = Mathf.FloorToInt(Time.timeSinceLevelLoad);
        TextElapsedTime.text = string.Format("{0:00}:{1:00}", time / 60, time % 60);
	}

    public void GetPoint(bool isPlayer1, int point)
    {
        var player = isPlayer1 ? Player1 : Player2;
        var textScore = isPlayer1 ? TextScore1 : TextScore2;
        player.Score += point;
        textScore.text = string.Format(isPlayer1 ? "1P: {0:0000}" : "2P: {0:0000}", player.Score);
    }

    public void Win(bool isPlayer1)
    {
        this.Status = StageStatus.Over;
        TextWinMessage.text = string.Format("Player {0} wins!", isPlayer1 ? 1 : 2);
        UIGameOver.Show();
    }
}
