using NaughtyAttributes;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyResponse
{
    Idle,
    Patrol,
    Chase,
    Attack
}

public class EnemyAI : MonoBehaviour
{
    [Header("References")]
    public PlayerController player;
    private NavMeshAgent agent;

    [Header("Patrol")]
    [SerializeField] Transform[] patrolPoints;
    private int currentPoint = 0;

    [Header("AI Settings")]
    public float chaseRange = 10f;
    public float attackRange = 2f;

    [Header("Debug Info")]
    public EnemyResponse enemyResponse;
    public float distanceToPlayer;

    [Header(" Shoot ")]
    public GameObject bullet;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //SetNextPatrolPoint();
        StartCoroutine(SetNextPatrolPoint());
    }

    void Update()
    {
        distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (distanceToPlayer <= attackRange)
        {
            enemyResponse = EnemyResponse.Attack;
        }
        else if (distanceToPlayer <= chaseRange)
        {
            enemyResponse = EnemyResponse.Chase;
        }
        else
        {
            enemyResponse = EnemyResponse.Patrol;
        }

        switch (enemyResponse)
        {
            case EnemyResponse.Patrol:
                Patrol();
                break;
            case EnemyResponse.Chase:
                Chase();
                break;
            case EnemyResponse.Attack:
                Attack();
                break;
            case EnemyResponse.Idle:
                agent.isStopped = true;
                break;

        }
    }

    void Patrol()
    {
        bulletDone = false;
        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            StartCoroutine(SetNextPatrolPoint());
        }
    }

    void Chase()
    {
        agent.isStopped = false;
        agent.SetDestination(player.transform.position);
    }

    void Attack()
    {
        agent.isStopped = true;
        transform.LookAt(player.transform);
        player.EnemyAttack();
        Shoot();
        // Example attack (replace with animation/damage)
        Debug.Log("Attacking player!");
    }

    public float shootForce = 5f;
    int bulletCont;
    bool bulletDone = false;
    [Button]
    public void Shoot()
    {
        if (bulletDone) return;
        StartCoroutine(DelayBullet());

    }
    GameObject shootBullet;
    private IEnumerator DelayBullet()
    {
        bulletDone = true;
        shootBullet = GetComponent<Object_polling>().ObjectPol();
        shootBullet.transform.position = transform.GetChild(0).transform.position;
        shootBullet.transform.rotation = transform.rotation;
        Rigidbody rb = shootBullet.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * shootForce;
        shootBullet.transform.SetParent(transform.parent);
        //Destroy(shootBullet, 1);
        Invoke(nameof(ReturnPoll), 1f);
        yield return new WaitForSeconds(0.5f);

        bulletDone = false;

    }
    public void ReturnPoll()
    {
        shootBullet.SetActive(false);
    }
    bool isIdling;
    private IEnumerator SetNextPatrolPoint()
    {
        if (patrolPoints.Length == 0 || isIdling) yield break;

        isIdling = true;
        enemyResponse = EnemyResponse.Idle;
        agent.isStopped = true;

        yield return new WaitForSeconds(2f);

        currentPoint = Random.Range(0, patrolPoints.Length);
        enemyResponse = EnemyResponse.Patrol;
        agent.isStopped = false;
        agent.destination = patrolPoints[currentPoint].position;

        isIdling = false;
    }


}
