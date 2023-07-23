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

    int maxHealth;

    public int damage;

    public Transform currentTarget;

    public float attackCooldown;

    public HealthBar healthBar;

    private void Awake()
    {
        maxHealth = hitPoints;
    }

    public void Attack(int takenDamage)
    {
        // Deal damage to the target
        if (currentTarget != null)
        {
            //Debug.Log(gameObject.name + " Attacking " + currentTarget.name);
            currentTarget.GetComponent<Unit>().TakeDamage(takenDamage);
        }
    }

    public void TakeDamage(int takenDamage)
    {
        hitPoints += takenDamage;

        if (hitPoints > maxHealth)
        {
            hitPoints = maxHealth;
        }
        healthBar.UpdateHealthBar(takenDamage);

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

