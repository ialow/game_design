using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class AiNpc : AbstractEntity
{
    [Header("Movement parameters")]
    [Inject] private NavMeshAgent navMeshAgent;
    [Inject(Id = "Player")] private GameObject player;

    [SerializeField] private float movementSpeed = 6f;
    [SerializeField] private float changePositionTime = 5f;
    [SerializeField] private float moveDistance = 30f;
    [SerializeField] private float detectionRadius = 10f;

    private bool playerDetected = false;
    private Vector3 patrolPoint;

    private void Start()
    {
        InitializeNavMeshAgent();
        InitializePlayer();
        InvokeRepeating(nameof(MoveNpc), changePositionTime, changePositionTime);
    }

    private void InitializeNavMeshAgent()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = movementSpeed;
    }

    private void InitializePlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public override void GetDamage(float damage)
    {
        base.GetDamage(damage);
    }

    public override void OnDeath()
    {
        base.OnDeath();
    }

    public override void OnRevival()
    {
        base.OnRevival();
    }

    private Vector3 RandomNavSphere(float distance)
    {
        var randomDirection = Random.insideUnitSphere * distance;
        randomDirection += transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, distance, -1);
        return navHit.position;
    }


    private void MoveNpc()
    {
        if (CanSeePlayer()) { playerDetected = true; AttackPlayer(); }
        else if (playerDetected) { Patrol(); }
        else { navMeshAgent.SetDestination(RandomNavSphere(moveDistance)); }
    }

    private bool CanSeePlayer()
    {
        return player != null && Vector3.Distance(transform.position, player.transform.position) <= detectionRadius;
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