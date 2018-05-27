using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Minego;

public enum StageStatus
{
    Pending,
    Started,
    Over
}

public class StageManager : MonoBehaviour
{
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
    public float StartDelay = 0f;
    public int WinPoints = 5;

    private StageStatus _status = StageStatus.Pending;
    [HideInInspector]
    public StageStatus Status
    {
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

    void Start()
    {
        TextStageName.text = string.Format("Stage {0}", StageName);
        SetUI(TextScore1, 20f, 0f, 140f, 30f, 26);
        SetUI(TextScore2, -20f, 0f, 140f, 30f, 26);
        SetUI(TextStageName, -124f, 0f, 240f, 30f, 26);
        SetUI(TextElapsedTime, 124f, 0f, 160f, 30f, 26);
        SetUI(TextWinMessage, 0f, 40f, 500f, 100f, 48);
        SetUI(TextContinue, 0f, -40f, 260f, 30f, 24);
        UpdateScore(Player1, GlobalState.Score1);
        UpdateScore(Player2, GlobalState.Score2);
        Status = StageStatus.Started;
        Player1.IsDead = Player2.IsDead = true;
        Invoke("StartStage", StartDelay);
    }

    public void StartStage()
    {
        Player1.IsDead = Player2.IsDead = false;
    }

    void Update()
    {
        var time = Mathf.FloorToInt(Time.timeSinceLevelLoad);
        TextElapsedTime.text = string.Format("{0:00}:{1:00}", time / 60, time % 60);
    }

    public void GetPoint(Player player, int point)
    {
        UpdateScore(player, player.Score + point);
    }

    void UpdateScore(Player player, int score)
    {
        var textScore = player.IsPlayer1 ? TextScore1 : TextScore2;
        player.Score = score;
        textScore.text = string.Format(player.IsPlayer1 ? "1P: {0:0000}" : "2P: {0:0000}", player.Score);
    }

    public void Win(Player player)
    {
        Status = StageStatus.Over;
        TextWinMessage.text = string.Format("Player {0} wins!", player.IsPlayer1 ? 1 : 2);
        GetPoint(player, WinPoints);
        GlobalState.Score1 = Player1.Score;
        GlobalState.Score2 = Player2.Score;
        UIGameOver.Show();
    }
}
