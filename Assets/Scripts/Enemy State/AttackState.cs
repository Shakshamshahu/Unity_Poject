public class AttackState : Enemy_Controller
{
    public AttackState(EnemyAI enemy) : base(enemy) { }

    public override void Enter()
    {
        enemy.agent.isStopped = true;
    }

    public override void Update()
    {
        if (enemy.DistanceToPlayer > enemy.attackRange)
        {
            enemy.ChangeState(new ChaseState(enemy));
            return;
        }

        enemy.transform.LookAt(enemy.player.transform);
        enemy.Shoot();
    }

    public override void Exit() { }
}
