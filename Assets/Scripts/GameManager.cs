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
    public List<GameObject> UnitsList;

    [Space(5)]

    [Header("Blue Player")]
    public BluePlayer bluePlayer;
    public List<Unit> blueTowers;
    public TextMeshProUGUI manaText;
    public Image manaImage;

    [Space(5)]

    [Header("Red Player")]
    public RedPlayer redPlayer;
    public List<Unit> redTowers;

    [Space(5)]

    [Header("GameEnd Panel")]
    public GameObject GameEndUI;
    public TextMeshProUGUI whoWonText;
    public Button restartButton;

    private void Start()
    {
        SetActivePanel(MenuUI.name);
        playButton.onClick.AddListener(OnClickPlayButton);
        restartButton.onClick.AddListener(RestartScene);
        remainingTime = gameTime;
    }


    public void SetTeams()
    {
        bluePlayer.gameObject.SetActive(true);
        bluePlayer.Towers = blueTowers;
        bluePlayer.CurrentMana = 5;
        bluePlayer.Units = UnitsList;

        redPlayer.gameObject.SetActive(true);
        redPlayer.Towers = redTowers;
        redPlayer.CurrentMana = 5;
        redPlayer.Units = UnitsList;
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
                CheckTowers();
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
        SetTeams();
        SetActivePanel(GameUI.name);
        isGameActive = true;
    }
    
    public void CheckTowers()
    {
        if (blueTowers.Count == redTowers.Count)
        {
            CheckTowersHealth();
            return;
        }
        if(blueTowers.Count > redTowers.Count)
        {
            GameOver(Team.Red); // Losing team
        }
        else
        {
            GameOver(Team.Blue);
        }
    }

    void CheckTowersHealth()
    {
        List<int> blueTowersHealth = new List<int>();
        List<int> redTowersHealth = new List<int>();

        foreach (var item in blueTowers)
        {
            if (item != null)
            {
                blueTowersHealth.Add(item.GetComponent<Unit>().hitPoints);
            }

        }
        foreach (var item in redTowers)
        {
            if (item != null)
            {
                redTowersHealth.Add(item.GetComponent<Unit>().hitPoints);
            }
        }
        blueTowersHealth.Sort();
        redTowersHealth.Sort();


        int minBlueHealth = blueTowersHealth[0];
        int minRedHealth = redTowersHealth[0];
        
        if (minBlueHealth > minRedHealth)
        {
            GameOver(Team.Red); // Losing team
        }
        else
        {
            GameOver(Team.Blue); // Losing team
        }
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

    public void GameOver(Team team)
    {
        if (team != Team.Blue)
        {
            whoWonText.text = "Blue Team Won";
        }
        else
        {
            whoWonText.text = "Red Team Won";
        }
        SetActivePanel(GameEndUI.name);
    }

    public void RestartScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
