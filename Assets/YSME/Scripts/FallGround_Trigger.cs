using System;
using System.Collections;
using UnityEditor.Callbacks;
using UnityEngine;

[Serializable]
public class FallGroundSet
{
    public Rigidbody2D rb;
    public float waitFallTime = 0;
    public float fallGravity = 2.5f;
    public float fallMass = 200;
    public float rotationPower = 0;
}

public class FallGround_Trigger : MonoBehaviour
{
    [Header("떨어질 땅 배열")]
    [SerializeField] private FallGroundSet[] sets;

    [Space, Header("트리거 설정")]
    [SerializeField] private bool Loop = false; // 트리거에 다시 닿으면 반복 재생할지 여부
    private bool alreadyPlayed = false; // 한번 재생되었는지 여부

    void Start()
    {
        EventManager.instance.OnPlayerRespawned += PlayerDie;
    }

    void OnDestroy()
    {
        EventManager.instance.OnPlayerRespawned -= PlayerDie;
    }

    void StartTrigger()
    {
        if (alreadyPlayed == true && Loop == false) return;

        alreadyPlayed = true;
        foreach (FallGroundSet set in sets)
        {
            StartCoroutine(FallStart(set));
        }
    }

    IEnumerator FallStart(FallGroundSet set)
    {
        yield return new WaitForSeconds(set.waitFallTime);
        set.rb.bodyType = RigidbodyType2D.Dynamic;
        set.rb.mass = set.fallMass;
        set.rb.gravityScale = set.fallGravity;
        set.rb.AddTorque(set.rotationPower);
    }

    void PlayerDie() // 플레이어가 죽은 경우 지형들 원상복구 시키기
    {
        foreach (FallGroundSet set in sets)
        {
            set.rb.bodyType = RigidbodyType2D.Static;
            set.rb.transform.position = Vector3.zero;
            set.rb.transform.rotation = Quaternion.identity;
        }
        alreadyPlayed = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartTrigger();
        }
    }
}
