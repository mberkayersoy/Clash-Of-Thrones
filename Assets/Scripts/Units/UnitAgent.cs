using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitAgent : Unit
{
    protected NavMeshAgent agent;
    protected Animator animator;
    public int mana;
    public int unitAmount;
    public float movementSpeed;
    public float sightRange;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!GameManager.Instance.isGameActive) return;
        FindTarget();
        Move();
    }

    private void Move()
    {
        if (!agent.isOnNavMesh)
        {
            return;
        }

        // If there is a target
        if (currentTarget != null)
        {
            Vector3 closestPoint = currentTarget.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
            float distanceToTarget = Vector3.Distance(transform.position, closestPoint);
    
            // If target is not in attackRange move towards target.
            if (distanceToTarget > attackRange)
            {
                Vector3 targetDirection = closestPoint - transform.position;
                targetDirection.y = 0f;
                targetDirection.Normalize();
                agent.isStopped = false;
                animator.SetTrigger("Move");
                agent.SetDestination(closestPoint);
            }
            // If the target entered the attackRange, stop and attack.
            else
            {
                agent.isStopped = true;
                if (attackCooldown <= 0f)
                {
                    attackCooldown = attackSpeed; 
                    Attack(damage);
                    animator.SetTrigger("Attack");
                }
                // Update cooldown between attacks
                if (attackCooldown > 0f)
                {
                    attackCooldown -= Time.deltaTime;
                }


            }
        }
        // If current target is null, move Local forward
        else
        {
            animator.SetTrigger("Move");
            FindTower();
            agent.isStopped = false;
            if (currentTarget == null) return;
            Vector3 closestPoint = currentTarget.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
            
            Vector3 targetDirection = closestPoint - transform.position;
            targetDirection.y = 0f;
            targetDirection.Normalize();
            agent.SetDestination(closestPoint);
        }
    }

    public void FindTarget()
    {
        //if (currentTarget != null) return;
        Collider[] colliders = Physics.OverlapSphere(transform.position, sightRange);

        Transform nearestEnemy = null;
        float nearestDistance = float.MaxValue;

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Unit enemy))
            {
                // If the target's team is different from agent's team and 
                // the agent can attack the target.
                if (team != enemy.team && targets.Contains(enemy.unitType))
                {
                    float distanceToEnemy = Vector3.Distance(transform.position, collider.transform.position);
                    if (distanceToEnemy < nearestDistance)
                    {
                        nearestDistance = distanceToEnemy;
                        nearestEnemy = collider.transform;
                    }
                }
            }
        }

        currentTarget = nearestEnemy;
    }

    public void FindTower()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 500f);

        Transform nearestTower = null;
        float nearestDistance = float.MaxValue;

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Unit enemy))
            {
                // If the target's team is different from agent's team and 
                // the agent can attack the target.
                if (team != enemy.team && enemy.unitType == UnitType.Tower)
                {
                    float distanceToEnemy = Vector3.Distance(transform.position, collider.transform.position);
                    if (distanceToEnemy < nearestDistance)
                    {
                        nearestDistance = distanceToEnemy;
                        nearestTower = collider.transform;
                    }
                }
            }
        }

        currentTarget = nearestTower;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sightRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}