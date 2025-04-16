using UnityEngine;

public class Player : Entity
{
    [Header("이동 속성")]
    [SerializeField] internal float moveSpd;
    [SerializeField] internal float jumpPower;

    public PlayerStateMachine stateMachine { get; private set; }

    // 상태
    //public PlayerIdleState idleState { get; private set; }
    //public PlayerMoveState moveState { get; private set; }
    //public PlayerJumpState jumpState { get; private set; }
}
