using UnityEngine;

public class EnemyDataManager : MonoBehaviour
{
    public Eneemy_ScritableObj enemyDataSO; // renamed
    public Transform[] enemyPostion;
    public EnemyAI enemyAI;
    public Transform enemyPatrolPos;

    //[Button("Apply Enemy Data")]
    /* public void ApplyEnemyData()
     {
         int count = Mathf.Min(transform.childCount, enemyDataSO.enemyDataShows.Length);

         for (int i = 0; i < count; i++)
         {
             EnemyAI ai = transform.GetChild(i).GetComponent<EnemyAI>();
             if (ai != null)
             {
                 EnemyDataShow data = enemyDataSO.enemyDataShows[i];
                 FillData(ai, data);
             }
         }
     }*/

    private void FillData(EnemyAI enemyAI, EnemyDataShow data)
    {
        enemyAI.name = data.enemyName;
        enemyAI.chaseRange = data.radius;
        enemyAI.attackRange = data.attackDistance;
        enemyAI.shootForce = data.shootForce;
    }
    private void Start()
    {
        SwapToPosition();
    }
    //[Button]
    public void SwapToPosition()
    {
        int transformsCount = Mathf.Min(enemyPostion.Length, enemyDataSO.enemyDataShows.Length);

        for (int i = 0; i < transformsCount; i++)
        {
            if (enemyPostion[i].transform.childCount >= 1)
            {
                Destroy(enemyPostion[i].transform.GetChild(0).gameObject);

            }
            EnemyAI enemy = Instantiate(enemyAI, enemyPostion[i].transform);
            enemy.transform.position = enemyPostion[i].position;
            enemy.player = UI_Maneger.instance.playerController;
            // Assign patrol points
            enemy.patrolPoints = new Transform[enemyPatrolPos.childCount];
            for (int j = 0; j < enemyPatrolPos.childCount; j++)
            {
                enemy.patrolPoints[j] = enemyPatrolPos.GetChild(j);
            }
            EnemyDataShow data = enemyDataSO.enemyDataShows[i];
            FillData(enemy, data);

        }

    }
}
