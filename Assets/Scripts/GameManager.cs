using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
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

    [Header("Menu Panel")]
    public GameObject MenuUI;
    public Button playButton;
    public Button quitButton;

    [Space(5)]

    [Header("Countdown Panel")]
    public GameObject CountdownUI;
    public TextMeshProUGUI countdownText;
    public int countdownValue;

    [Space(5)]

    [Header("Game Panel")]
    public GameObject GameUI;
    public TextMeshProUGUI remainingTimeText;
    public bool isGameActive;
    public float gameTime = 75f;
    float remainingTime;


    [Space(5)]

    [Header("GameEnd Panel")]
    public GameObject GameEndUI;
    public Button restartButton;

    private void Start()
    {
        SetActivePanel(MenuUI.name);
        playButton.onClick.AddListener(OnClickPlayButton);
        restartButton.onClick.AddListener(RestartScene);
        remainingTime = gameTime;
    }

    private void Update()
    {
        if (isGameActive)
        {
            remainingTime -= Time.deltaTime;
            DisplayRemainingTime();

            if (remainingTime <= 0)
            {
                isGameActive = false;
                SetActivePanel(GameEndUI.name);
            }
        }
    }
    public void DisplayRemainingTime()
    {
        float min = remainingTime / 60;
        float second = remainingTime % 60;
        if (second >= 10)
        {
            remainingTimeText.text = ((int)min).ToString() + ":" + ((int)second).ToString();
        }
        else
        {
            remainingTimeText.text = ((int)min).ToString() + ":0" + ((int)second).ToString();
        }

    }
    IEnumerator CountDownDisplay()
    {
        for (int i = countdownValue; i > 0; i--)
        {
            CountDownAnimation();
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        CountDownAnimation();
        countdownText.text = "GO!";
        yield return new WaitForSeconds(1f);
        SetActivePanel(GameUI.name);
        isGameActive = true;
    }

    public void CountDownAnimation()
    {
        countdownText.transform.DOScale(3f, 0.5f).SetEase(Ease.OutBounce)
            .OnComplete(() =>
            {
                countdownText.transform.DOScale(0f, 0.5f).SetEase(Ease.OutQuad);
            });
    }

    public void SetActivePanel(string activePanel)
    {
        MenuUI.SetActive(activePanel.Equals(MenuUI.name));
        CountdownUI.SetActive(activePanel.Equals(CountdownUI.name));
        GameUI.SetActive(activePanel.Equals(GameUI.name));
        GameEndUI.SetActive(activePanel.Equals(GameEndUI.name));
    }

    public void OnClickPlayButton()
    {
        SetActivePanel(CountdownUI.name);
        StartCoroutine(CountDownDisplay());
    }


    public void RestartScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
