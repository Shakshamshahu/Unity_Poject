public class ChaseState : Enemy_Controller
{
    public ChaseState(EnemyAI enemy) : base(enemy) { }

    public override void Enter()
    {
        enemy.agent.isStopped = false;
    }

    public override void Update()
    {
        if (enemy.DistanceToPlayer <= enemy.attackRange)
        {
            enemy.ChangeState(new AttackState(enemy));
            return;
        }

        if (enemy.DistanceToPlayer > enemy.chaseRange)
        {
            enemy.ChangeState(new PatrolState(enemy));
            return;
        }

        enemy.agent.SetDestination(enemy.player.transform.position);
    }

    public override void Exit() { }
}
