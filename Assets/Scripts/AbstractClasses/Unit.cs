using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public string unitName;

    public List<UnitType> targets;

    public Team team;

    public UnitType unitType;

    public float attackSpeed;

    public float attackRange;

    public int hitPoints;

    public int damage;

    public Transform currentTarget;

    public float attackCooldown;


    public void Attack()
    {
        // Deal damage to the target
        if (currentTarget != null)
        {
            //Debug.Log(gameObject.name + " Attacking " + currentTarget.name);
            currentTarget.GetComponent<Unit>().TakeDamage(damage);
        }
    }

    public void TakeDamage(int takenDamage)
    {
        Debug.Log("Take Damage " + gameObject.name);
        hitPoints -= takenDamage;
        if (hitPoints <= 0)
        {
            Destroy(gameObject);
            return;
        }
    }
}

public enum UnitType
{
    Ground,
    Air,
    Tower
}

public enum Team
{
    Blue,
    Red,
}

