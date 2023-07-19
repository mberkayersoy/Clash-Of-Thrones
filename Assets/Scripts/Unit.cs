using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Unit", menuName = "Unit/Create Unit", order = 0)]
public class Unit : ScriptableObject
{
    public string unitName;
    public int mana;
    public int damage;
    public int hitPoints;
    public float attackSpeed;
    public float attackRange;
    public float sightRange;
    public float movementSpeed;
    public float unitAmount;
    public List<UnitType> targets;
    public Team team;
    public UnitType pos;
}

public enum UnitType
{
    Ground,
    Air,
    Tower
}

public enum Team
{
    Player,
    Enemy,
}
