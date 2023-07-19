using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    public Tower tower;
    public Transform currentTarget;
    void Start()
    {
        
    }

    void Update()
    {
        FindTarget();
    }

    void FindTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, tower.range);
        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out AgentMover enemy))
            {
                Unit enemyUnit = enemy.GetComponent<AgentMover>().unit;

                // If the target's team is different from player's own team.
                if (tower.team != enemyUnit.team)
                {
                    currentTarget = collider.transform;
                    return;
                }
            }
        }
        // If the target is not found set currentTarget null.
        currentTarget = null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, tower.range);
    }
}
