using UnityEngine;
using UnityEngine.AI;

public class AiNpc : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private GameObject player;

    [SerializeField] private float movementSpeed = 6f;
    [SerializeField] private float changePositionTime = 5f;
    [SerializeField] private float moveDistance = 30f;
    [SerializeField] private float detectionRadius = 10f;

    private bool playerDetected = false;
    private Vector3 patrolPoint;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = movementSpeed;
        player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating(nameof(MoveNpc), changePositionTime, changePositionTime);
    }

    private Vector3 RandomNavSphere(float distance)
    {
        var randomDirection = UnityEngine.Random.insideUnitSphere * distance;
        randomDirection += transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, distance, -1);
        return navHit.position;
    }

    private void MoveNpc()
    {
        if (CanSeePlayer())
        {
            playerDetected = true;
            AttackPlayer();
        }
        else if (playerDetected)
        {
            Patrol();
        }
        else
        {
            patrolPoint = RandomNavSphere(moveDistance);
            navMeshAgent.SetDestination(patrolPoint);
        }
    }

    private bool CanSeePlayer()
    {
        if (player != null)
        {
            return Vector3.Distance(transform.position, player.transform.position) <= detectionRadius;
        }
        else
        {
            return false;
        }
    }

    private void AttackPlayer()
    {
        navMeshAgent.SetDestination(player.transform.position);
    }

    private void Patrol()
    {
        navMeshAgent.SetDestination(patrolPoint);
    }
}