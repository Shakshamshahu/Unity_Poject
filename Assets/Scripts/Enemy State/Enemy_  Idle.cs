public class IdleState : Enemy_Controller
{
    public IdleState(EnemyAI enemy) : base(enemy) { }

    public override void Enter()
    {
        enemy.agent.isStopped = true;
    }

    public override void Update()
    {
        if (enemy.DistanceToPlayer <= enemy.chaseRange)
        {
            enemy.ChangeState(new ChaseState(enemy));
        }
        else
        {
            enemy.ChangeState(new PatrolState(enemy));
        }
        //base.Update();
    }

    public override void Exit() { }
}
