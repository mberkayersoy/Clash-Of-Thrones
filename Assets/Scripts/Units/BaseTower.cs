using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTower : SideTower
{
    //private void Update()
    //{
    //    FindTarget();

    //    if (currentTarget != null)
    //    {

    //        if (attackCooldown <= 0f)
    //        {
    //            attackCooldown = attackSpeed;
    //            Attack(damage);
    //        }
    //        // Update attack cooldown
    //        if (attackCooldown > 0f)
    //        {
    //            attackCooldown -= Time.deltaTime;
    //        }
    //    }
    //}
    //public void FindTarget()
    //{
    //    if (currentTarget != null) return;
    //    Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange);
    //    foreach (Collider collider in colliders)
    //    {
    //        if (collider.TryGetComponent(out Unit enemy))
    //        {

    //            // If the target's team is different from agent's team and 
    //            // the agent can attack the target.
    //            if (team != enemy.team && targets.Contains(enemy.unitType))
    //            {
    //                currentTarget = collider.transform;
    //                //Debug.Log(currentTarget.name);
    //                return;
    //            }
    //        }

    //    }
    //    // If the target is not found set currentTarget null.
    //    currentTarget = null;
    //}

    private void OnDestroy()
    {
        GameManager.Instance.GameOver(team);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
