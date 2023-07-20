using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //[Header("Menu Panel")]
    //public GameObject MenuUI;
    //public Button playButton;
    //public Button quitButton;

    //[Space(5)]

    //[Header("Countdown Panel")]
    //public GameObject CountdownUI;
    //public TextMeshProUGUI countdownText;
    //public int countdownValue;

    //[Space(5)]

    //[Header("Game Panel")]
    //public GameObject GameUI;
    //public Button pauseButton;
    //public TextMeshProUGUI remainingTimeText;
    //float gameTime = 75f;
    //float remainingTime;


    //[Space(5)]

    //[Header("GameEnd Panel")]
    //public GameObject GameEndUI;
    //public Button restartButton;

    //private void Start()
    //{
    //    gameManager = GameManager.Instance;
    //    DOTween.Init();
    //    remainingTime = gameTime;
    //    SetActivePanel(MenuUI.name);
    //    playButton.onClick.AddListener(OnClickPlayButton);
    //    restartButton.onClick.AddListener(RestartScene);
    //    gameTime = gameManager.gameTime;
    //    remainingTime = gameTime;
    //}


    //public void DisplayRemainingTime()
    //{
    //    float min = remainingTime / 60;
    //    float second = remainingTime % 60;
    //    if (second >= 10)
    //    {
    //        remainingTimeText.text = ((int)min).ToString() + ":" + ((int)second).ToString();
    //    }
    //    else
    //    {
    //        remainingTimeText.text = ((int)min).ToString() + ":0" + ((int)second).ToString();
    //    }

    //}
    //IEnumerator CountDownDisplay()
    //{
    //    for (int i = countdownValue; i > 0; i--)
    //    {
    //        CountDownAnimation();
    //        countdownText.text = i.ToString();
    //        yield return new WaitForSeconds(1f);
    //    }
    //    CountDownAnimation();
    //    countdownText.text = "GO!";
    //    yield return new WaitForSeconds(1f);
    //    gameManager.isGameActive = true;
    //    SetActivePanel(GameUI.name);
    //}

    //public void CountDownAnimation()
    //{
    //    countdownText.transform.DOScale(3f, 0.5f).SetEase(Ease.OutBounce)
    //        .OnComplete(() =>
    //        {
    //            countdownText.transform.DOScale(0f, 0.5f).SetEase(Ease.OutQuad);
    //        });
    //}

    //public void SetActivePanel(string activePanel)
    //{
    //    MenuUI.SetActive(activePanel.Equals(MenuUI.name));
    //    CountdownUI.SetActive(activePanel.Equals(CountdownUI.name));
    //    GameUI.SetActive(activePanel.Equals(GameUI.name));
    //    GameEndUI.SetActive(activePanel.Equals(GameEndUI.name));
    //}

    //public void OnClickPlayButton()
    //{
    //    SetActivePanel(CountdownUI.name);
    //    StartCoroutine(CountDownDisplay());
    //}


    //public void RestartScene()
    //{
    //    Time.timeScale = 1;
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //}
}
