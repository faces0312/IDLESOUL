public class EnemyStateMachine : BaseStateMachine
{
    public Enemy Enemy { get; private set; }
    public EnemyIdleState IdleState { get; private set; }
    public EnemyMoveState MoveState { get; private set; }
    public EnemyAttackState AttackState { get; private set; }
    public EnemySkillState SkillState { get; private set; }

    public EnemyStateMachine(Enemy enemy)
    {
        this.Enemy = enemy;
        IdleState = new EnemyIdleState(this);
        MoveState = new EnemyMoveState(this);
        AttackState = new EnemyAttackState(this);
        SkillState = new EnemySkillState(this);
    }

    public void Initialize()
    {
        ChangeState(MoveState);
    }

    //public BaseSlimeTower SlimeTower { get; private set; }
    //public SlimeTowerIdleState IdleState { get; private set; }
    //public SlimeTowerAttackState AttackState { get; private set; }

    //public SlimeTowerWalkState WalkState { get; private set; }


    //public SlimeStateMachine(BaseSlimeTower slimeTower)
    //{
    //    IdleState = new SlimeTowerIdleState(this);
    //    AttackState = new SlimeTowerAttackState(this);
    //    WalkState = new SlimeTowerWalkState(this);
    //    SlimeTower = slimeTower;
    //}
}
