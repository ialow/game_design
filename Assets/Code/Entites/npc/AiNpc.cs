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

    private bool hasDied = false;
    private bool playerDetected = false;
    private Vector3 patrolPoint;

    [Header("GameObject")]
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject NPC;
    [SerializeField] private GameObject gearPrefab;
    [SerializeField] private MeshRenderer npcMaterial;

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

    private void Update()
    {
        if (!hasDied && Vector3.Distance(NPC.transform.position, player.transform.position) <= 1.7f)
        {
            player.GetComponent<IDamagable>().GetDamage(50);
            GetDamage(50);
            OnDeath();
            hasDied = true;
        }
    }

    private void MoveNpc()
    {
        if (CanSeePlayer()) { AttackPlayer(); }
        else { Patrol(); }
    }

    private void AttackPlayer()
    {
        playerDetected = true;
        navMeshAgent.SetDestination(player.transform.position);
    }

    private bool CanSeePlayer()
        => player != null && Vector3.Distance(transform.position, player.transform.position) <= detectionRadius;

    private void Patrol()
    {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
            navMeshAgent.SetDestination(RandomNavSphere(moveDistance));
    }

    private IEnumerator EnableExplosion(float duration)
    {
        navMeshAgent.speed = 0;
        var newExplosion = Instantiate(explosion, NPC.transform.position, Quaternion.identity);
        var gear = Instantiate(gearPrefab, NPC.transform.position, Quaternion.identity);
        npcMaterial.enabled = false;
        yield return new WaitForSeconds(duration);
        Destroy(newExplosion);
        base.OnDeath();
    }
}