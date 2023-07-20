using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitAgent : Unit
{
    private NavMeshAgent agent;
    public int mana;
    public int unitAmount;
    public float movementSpeed;
    public float sightRange;
    public GameObject rightPath;
    public GameObject leftPath;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        FindTarget();
        Move();
    }

    public void Move()
    {
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
                agent.SetDestination(closestPoint);
            }
            // If the target entered the attackRange, stop and attack.
            else
            {
                Debug.Log("ElSE");
                agent.isStopped = true;
                if (attackCooldown <= 0f)
                {
                    Attack();
                    Debug.Log(gameObject.name + " Attacking " + currentTarget.name);
                    attackCooldown = attackSpeed; // Saldýrýlar arasýndaki bekleme süresini hesapla
                }
                // Saldýrýlar arasýndaki bekleme süresini güncelle
                if (attackCooldown > 0f)
                {
                    attackCooldown -= Time.deltaTime;
                }


            }
        }
        // If current target is null, move Local forward
        else
        {
            agent.isStopped = false;

            // Determine the closest path (right or left) based on the x position
            GameObject closestPath = transform.position.x >= 0f ? rightPath : leftPath;
            Vector3 pathPosition;
            if (team == Team.Blue)
            {
                // Get the positions of the path
                pathPosition = closestPath.transform.GetChild(1).position;
            }
            else
            {
                // Get the positions of the path
                pathPosition = closestPath.transform.GetChild(0).position;
            }

            // Set target to gent
            agent.SetDestination(pathPosition);
        }
    }

    public void FindTarget()
    {
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
        //if (currentTarget != null)
        //{
        //    Debug.Log("Nearest enemy: " + currentTarget.name);
        //}
        //else
        //{
        //    Debug.Log("No enemy in sight.");
        //}
    }

private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sightRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}