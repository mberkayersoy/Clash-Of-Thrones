using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Healer : UnitAgent
{
    public List<UnitType> friendTargets;
    public int heal;
    public float healCooldown;
    public float healSpeed;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!GameManager.Instance.isGameActive) return;

        FindTarget();

        if (healCooldown <= 0f)
        {
            FindFriend();
            healCooldown = healSpeed;
        }
        // Update cooldown between attacks
        if (healCooldown > 0f)
        {
            healCooldown -= Time.deltaTime;
        }
        Move();
    }

    public void Move()
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
                    Debug.Log(gameObject.name + " Attacking " + currentTarget.name);
                    attackCooldown = attackSpeed; // Saldýrýlar arasýndaki bekleme süresini hesapla
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

    public void FindFriend()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange);

        List<Unit> nearestFriends = new List<Unit>();

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Unit friend))
            {
                // If the target's team is different from agent's team and 
                // the agent can attack the target.
                if (team == friend.team && friendTargets.Contains(friend.unitType))
                {
                    nearestFriends.Add(friend);
                }
            }
        }
        if (nearestFriends != null)
        {
            HealFrinds(nearestFriends);
        }
    }

    private void HealFrinds(List<Unit> friends)
    {
        foreach (Unit friend in friends)
        {
            friend.TakeDamage(heal);
        }
    }

}
