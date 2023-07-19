using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tower", menuName = "Tower/Create Tower", order = 0)]
public class Tower : ScriptableObject
{
    public string unitName;
    public int damage;
    public int hitPoints;
    public float hitSpeed;
    public float range;
    public UnitType targets;
    public Team team;
    public UnitType pos;
}
