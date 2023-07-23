using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPlayer : Player
{
    readonly Team team = Team.Red;
    public List<GameObject> units = new List<GameObject>();
    public List<GameObject> Units { get => units; set => units = value; }

    void Update()
    {
        UpdateMana();
        InstantiateUnit();

    }
    public override void UpdateMana()
    {
        if (!GameManager.Instance.isGameActive) return;

        if (currentMana <= maxMana)
        {
            currentMana += Time.deltaTime;
        }
    }

    public void InstantiateUnit()
    {
        int randomindex = Random.Range(0, 4);
        if(currentMana >= units[randomindex].GetComponent<UnitAgent>().mana)
        {
            DecreaseMana(units[randomindex].GetComponent<UnitAgent>().mana);
            GameObject soldier = Instantiate(units[randomindex], new Vector3(Random.Range(-6,7),0,20), Quaternion.identity);
            soldier.GetComponent<Unit>().team = team;
            MoveIndexToEnd(units, randomindex);
        }
    }

}
