using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Zenject.SpaceFighter;

public class AiNpc : AbstractEntity
{
    [Header("Movement parameters")]
    [Inject] private NavMeshAgent navMeshAgent;
    [Inject(Id = "Player")] private GameObject player;

    [field: SerializeField] private float movementSpeed = 6f;
    [SerializeField] private float changePositionTime = 5f;
    [SerializeField] private float moveDistance = 30f;
    [SerializeField] private float detectionRadius = 10f;

    private bool playerDetected = false;
    private Vector3 patrolPoint;

    [Header("GameObject")]
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject NPC;

    private void Start()
    {
        explosion.SetActive(false);
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
        StartCoroutine(EnableExplosion(1));
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

    private IEnumerator EnableExplosion(float duration)
    {
        movementSpeed = 0f;
        explosion.SetActive(true);
        NPC.SetActive(false);
        yield return new WaitForSeconds(duration);
        base.OnDeath();
        explosion.SetActive(false);
    }
}