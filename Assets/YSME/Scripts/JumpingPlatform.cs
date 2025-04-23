using System;
using UnityEngine;

public class JumpingPlatform : MonoBehaviour
{
    [Range(0, 20f)] public float jumpPower = 5; // 임시로 넣어둔 항목 나중에 캐릭터 상태에 슈퍼?점프 상태가 생기면 직접적으로 힘을 가하는 형식이 아닌 상태를 변화시키는 형식으로 수정할 것

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            Debug.Log("점프 발판 밟음");
            // player.stateMachine.ChangeState(player.objectJumpState);
        }
    }
}
