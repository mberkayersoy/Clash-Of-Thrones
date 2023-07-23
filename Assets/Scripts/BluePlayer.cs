using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePlayer : Player
{
    public static BluePlayer Instance;
    readonly Team team = Team.Blue;

    public List<GameObject> units = new List<GameObject>();
    public List<GameObject> Units { get => units; set => units = value; }

    private List<Card> deck = new List<Card>();
    public List<Card> Deck { get => deck; set => deck = value; }

    public List<Transform> cardParents;
    public Transform nextCardParent;
    public GameObject cardPrefab;

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

    private void Start()
    {
        InstantiateInitialDeck();
    }

    void InstantiateInitialDeck()
    {
        for (int i = 0; i < cardParents.Count; i++)
        {
            GameObject card = Instantiate(cardPrefab, cardParents[i]);

            card.GetComponent<Card>().unitPrefab = units[i];
            card.GetComponent<Card>().SetCardData();
            card.GetComponent<Card>().index = i;
        }
    }

    void Update()
    {
        UpdateMana();
    }
    public override void UpdateMana()
    {
        if (!GameManager.Instance.isGameActive) return;

        if (currentMana <= maxMana)
        {
            currentMana += Time.deltaTime;
            UpdateManaBar();
        }
    }

    public void UpdateManaBar()
    {
        GameManager.Instance.manaText.text = ((int)currentMana).ToString();
        currentMana = Mathf.Clamp(currentMana, 0f, maxMana);
        float fillAmount = currentMana / maxMana;
        GameManager.Instance.manaImage.fillAmount = fillAmount;
    }
    public void InstantiateUnit(Vector3 cardPos, int index, GameObject unitPrefab)
    {
        DecreaseMana(units[index].GetComponent<UnitAgent>().mana);
        GameObject soldier = Instantiate(unitPrefab, cardPos, Quaternion.identity);
        soldier.GetComponent<Unit>().team = team;
        MoveIndexToEnd(units, index);
        CreateNewCard(index);
    }

    public void CreateNewCard(int i)
    {
        GameObject card = Instantiate(cardPrefab, cardParents[i]);

        card.GetComponent<Card>().unitPrefab = units[3];
        card.GetComponent<Card>().SetCardData();
        card.GetComponent<Card>().index = i;
    }
}
