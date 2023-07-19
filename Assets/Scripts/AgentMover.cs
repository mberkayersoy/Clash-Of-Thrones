using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentMover : MonoBehaviour
{
    private NavMeshAgent agent;
    public Unit unit;
    public Transform currentTarget;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        FindTarget();
        MoveTarget();
    }
    void MoveTarget()
    {
        // If there is a target
        if (currentTarget != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, currentTarget.position);

            // If target is not in attackRange move towards target.
            if (distanceToTarget > unit.attackRange)
            {

            }
            // If the target entered the attackRange, stop and attack.
            else
            {
                Debug.Log("Atacked: " + currentTarget.name);
                // AttackTarget();
            }
        }
        // If current target is null, move forward
        else
        {
            Debug.Log("MoveForward");
            Vector3 forwardDirection = transform.position + transform.forward * unit.movementSpeed;
            agent.SetDestination(forwardDirection);
        }
    }

    void AttackTarget()
    {

    }

    void FindTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, unit.sightRange);
        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out AgentMover enemy))
            {
                Unit enemyUnit = enemy.GetComponent<AgentMover>().unit;

                // If the target's team is different from agent's team and 
                // the agent can attack the target.
                if (unit.team != enemyUnit.team && unit.targets.Contains(enemyUnit.pos))
                {
                    currentTarget = collider.transform;
                    Debug.Log(currentTarget.name);
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
        Gizmos.DrawWireSphere(transform.position, unit.sightRange);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, unit.attackRange);

    }
}
