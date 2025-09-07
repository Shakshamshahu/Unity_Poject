public abstract class Enemy_Controller
{
    protected EnemyAI enemy;
    public Enemy_Controller(EnemyAI enemyAI) => this.enemy = enemyAI;
    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}
