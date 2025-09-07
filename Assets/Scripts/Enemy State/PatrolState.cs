public class PatrolState : Enemy_Controller
{
    public PatrolState(EnemyAI enemy) : base(enemy) { }

    public override void Enter()
    {
        enemy.agent.isStopped = false;
        enemy.SetNextPatrolPoint();
    }

    public override void Update()
    {
        if (enemy.DistanceToPlayer <= enemy.chaseRange)
        {
            enemy.ChangeState(new ChaseState(enemy));
            return;
        }

        if (!enemy.agent.pathPending && enemy.agent.remainingDistance <= enemy.agent.stoppingDistance)
        {
            enemy.SetNextPatrolPoint();
        }
    }

    public override void Exit() { }
}
