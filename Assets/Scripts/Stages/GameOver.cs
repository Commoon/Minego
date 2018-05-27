using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public string NextStage;

    CanvasGroup canvasGroup;
    bool active = false;
    bool loadingNextStage = false;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Start()
    {
        Hide();
    }

    void Update()
    {
        if (!active)
        {
            return;
        }
        if (!loadingNextStage && Input.GetButton("Submit"))
        {
            loadingNextStage = true;
            SceneManager.LoadScene(NextStage);
        }
    }

    public void Hide()
    {
        active = false;
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void Show()
    {
        active = true;
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
}