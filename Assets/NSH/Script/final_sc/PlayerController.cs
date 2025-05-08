using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool isStunned = false;
    private float stunTimer = 0f;

    void Update()
    {
        if (isStunned)
        {
            stunTimer -= Time.deltaTime;
            if (stunTimer <= 0f)
            {
                isStunned = false;
                // 여기서 다시 움직이도록 처리
            }
        }

        if (!isStunned)
        {
            // 이동/점프 등 플레이어 조작
        }
    }

    public void Stun(float duration)
    {
        isStunned = true;
        stunTimer = duration;
        // 여기에 애니메이션, 이펙트 추가 가능
    }
}
