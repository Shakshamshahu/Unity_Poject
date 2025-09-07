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
    public Enemy_Controller currentState;

    [Header("References")]
    public PlayerController player;
    public NavMeshAgent agent;

    [Header("Patrol")]
    public Transform[] patrolPoints;
    private int currentPoint = 0;

    [Header("AI Settings")]
    public float chaseRange = 10f;
    public float attackRange = 2f;

    [Header("Debug Info")]
    public EnemyResponse enemyResponse;
    public float distanceToPlayer;

    [Header(" Shoot ")]
    public GameObject bullet;
    bool enemyStory;
    public float DistanceToPlayer => Vector3.Distance(player.transform.position, transform.position);
    private void OnEnable()
    {
        Event_Maneger.Subscribe("LevelCompleteCall", OnlevelComplete);
    }
    private void OnDisable()
    {
        Event_Maneger.Unsubscribe("LevelCompleteCall", OnlevelComplete);
    }

    private void OnlevelComplete(object obj)
    {
        enemyStory = true;
        agent.isStopped = true;
        player.isPlayerWin = true;
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        ChangeState(new IdleState(this));
    }
    public void ChangeState(Enemy_Controller newstate)
    {
        currentState?.Exit();
        currentState = newstate;
        currentState.Enter();
    }
    void Update()
    {
        currentState?.Update();
    }
    public float shootForce = 5f;
    int bulletCont;
    bool bulletDone = false;

    public void Shoot()
    {
        if (bulletDone || enemyStory) return;
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
    // Example for patrol points
    public void SetNextPatrolPoint()
    {
        if (patrolPoints.Length == 0) return;
        int randomIndex = Random.Range(0, patrolPoints.Length);
        agent.destination = patrolPoints[randomIndex].position;
    }


}
