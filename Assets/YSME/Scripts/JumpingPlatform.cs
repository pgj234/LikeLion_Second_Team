using System;
using UnityEngine;

public class JumpingPlatform : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            Debug.Log("점프 발판 밟음");
            player.stateMachine.ChangeState(player.objectJumpState);
        }
    }
}
