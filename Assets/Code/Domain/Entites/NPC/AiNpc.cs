using Ddd.Application;
using Ddd.Infrastructure;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Ddd.Domain
{
    public class AiNpc : AbstractEntity
    {
        public static Action<int> DeathEvent;

        [Header("Movement parameters")]
        [SerializeField] private float standardMovementSpeed = 2.5f;
        [SerializeField] private float acceleratedMovementSpeed = 4f;
        [SerializeField] private float changePositionTime = 0.1f;
        [SerializeField] private float moveDistance = 27f;
        [SerializeField] private float detectionRadius = 9f;

        private bool hasDied = false;
        private bool isMovingToPlayer = false;
        private bool isRunningCoroutine = false;

        [Header("GameObject")]
        [SerializeField] private GameObject explosion;
        [SerializeField] private GameObject runNPC;
        [SerializeField] private GameObject NPC;
        [SerializeField] private GameObject gearPrefab;
        [SerializeField] private MeshRenderer npcMaterial;
        private NavMeshAgent navMeshAgent;
        private GameObject player;
        private DiContainer container;


        private void Start()
        {
            InitializeNavMeshAgent();
            InitializePlayer();
            InvokeRepeating(nameof(MoveNpc), changePositionTime, changePositionTime);
        }

        private void InitializeNavMeshAgent()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            navMeshAgent.speed = standardMovementSpeed;
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
            if (!hasDied)
            {
                StartCoroutine(EnableExplosion(0.61f));
                hasDied = true;
            }
        }

        public override void OnRevival()
        {
            base.OnRevival();
        }

        private Vector3 RandomNavSphere(float distance)
        {
            var randomDirection = UnityEngine.Random.insideUnitSphere * distance;
            randomDirection += transform.position;
            NavMeshHit navHit;
            NavMesh.SamplePosition(randomDirection, out navHit, distance, -1);
            return navHit.position;
        }

        private void Update()
        {
            if (isMovingToPlayer && !isRunningCoroutine)
            {
                StartCoroutine(EnableRun(0.6f));
            }
            if (!hasDied && Vector3.Distance(NPC.transform.position, player.transform.position) <= 1.7f)
            {
                player.GetComponent<IDamagable>().GetDamage(33);
                GetDamage(50);
                OnDeath();
                hasDied = true;
            }
        }

        private void MoveNpc()
        {
            if (CanSeePlayer())
            {
                AttackPlayer();
            }
            else if (!isMovingToPlayer)
            {
                Patrol();
            }
        }

        private void AttackPlayer()
        {
            navMeshAgent.SetDestination(player.transform.position);
            isMovingToPlayer = true;
            navMeshAgent.speed = acceleratedMovementSpeed;
        }

        private bool CanSeePlayer()
            => player != null && Vector3.Distance(transform.position, player.transform.position) <= detectionRadius;

        private void Patrol()
        {
            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
            {
                navMeshAgent.SetDestination(RandomNavSphere(moveDistance));
                navMeshAgent.speed = standardMovementSpeed;
            }
        }

        private IEnumerator EnableExplosion(float duration)
        {
            var newExplosion = Instantiate(explosion, NPC.transform.position, Quaternion.identity);
            var gear = Instantiate(gearPrefab, NPC.transform.position, Quaternion.identity);
            npcMaterial.enabled = false;
            yield return new WaitForSeconds(duration);
            base.OnDeath();
            Destroy(newExplosion);
            DeathEvent?.Invoke(UnityEngine.Random.Range(6, 15));
        }

        private IEnumerator EnableRun(float duration)
        {
            if (!hasDied)
            {
                isRunningCoroutine = true;
                var prefabRun = Instantiate(runNPC, NPC.transform.position, NPC.transform.rotation);
                prefabRun.transform.Rotate(new Vector3(0f, 1f, 0f), 90f);
                yield return new WaitForSeconds(duration);
                isRunningCoroutine = false;
                Destroy(prefabRun);
            }
        }
    }
}