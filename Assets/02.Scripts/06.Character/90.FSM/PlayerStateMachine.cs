public class PlayerStateMachine : BaseStateMachine
{
    public Player _Player { get; private set; }
    public PlayerIdelState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerAttackState AttackState { get; private set; }

    public PlayerStateMachine(Player player)
    {
        IdleState = new PlayerIdelState(this);
        MoveState = new PlayerMoveState(this);
        AttackState = new PlayerAttackState(this);

        _Player = player;
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
