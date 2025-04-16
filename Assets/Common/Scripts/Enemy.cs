using UnityEngine;

public class Enemy : Entity
{
    [Header("이동 속성")]
    [SerializeField] internal float moveSpd;
    //[SerializeField] internal float jumpPower;

    // 상태를 관리하는 상태 머신
    public EnemyStateMachine stateMachine { get; private set; }

    // 상태
    //public PlayerIdleState idleState { get; private set; }
    //public PlayerMoveState moveState { get; private set; }
    //public PlayerJumpState jumpState { get; private set; }
}
