public class PlayerStateMachine : BaseStateMachine
{
    public Player _Player { get; private set; }
    public PlayerIdelState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerMeleeAttackState MeleeAttackState { get; private set; }
    public PlayerShotAttackState ShotAttackState { get; private set; }

    public PlayerBaseState CurrentState { get { return (PlayerBaseState)currentState; } }

    public PlayerStateMachine(Player player)
    {
        IdleState = new PlayerIdelState(this);
        MoveState = new PlayerMoveState(this);
        MeleeAttackState = new PlayerMeleeAttackState(this);
        ShotAttackState = new PlayerShotAttackState(this);
        _Player = player;
    }

}
