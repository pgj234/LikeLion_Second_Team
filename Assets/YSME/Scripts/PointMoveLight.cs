using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.Universal;

[Serializable]
public struct PositionAndSpeed
{
    public Transform pos;
    public float speed;
}

public class PointMoveLight : MonoBehaviour
{
    [Header("오브젝트 설정")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color activeColor;
    [SerializeField] private Color deactiveColor;

    [Space, Header("움직임 포인트")]
    [SerializeField] private PositionAndSpeed startPos;
    [SerializeField] private PositionAndSpeed[] movePos;
    [SerializeField] private PositionAndSpeed endPos;

    [Space, Header("빛 설정")]

    [Tooltip("캐릭터가 오브젝트 공격 시 나오는 빛")]
    [SerializeField] private Light2D lightObj;
    [SerializeField] private float outerRadius;
    [SerializeField] private float innerRadius;
    [SerializeField] private float readyTime;
    private bool isMoving = false;

    void Start()
    {
        EventManager.instance.OnPlayerRespawned += Init;
    }

    void Oestroy()
    {
        EventManager.instance.OnPlayerRespawned -= Init;
    }

    void Init()
    {
        isMoving = false;
        lightObj.gameObject.SetActive(false);
        lightObj.transform.position = startPos.pos.position;
        lightObj.pointLightOuterRadius = 0;
        lightObj.pointLightInnerRadius = 0;

        spriteRenderer.color = activeColor;
    }

    IEnumerator StartMove()
    {
        isMoving = true;
        lightObj.transform.position = startPos.pos.position;
        lightObj.pointLightOuterRadius = 0;
        lightObj.pointLightInnerRadius = 0;

        yield return StartCoroutine(LightOn());

        for (int i = 0; i < movePos.Length; i++)
        {
            if (isMoving == false) yield break;
            yield return StartCoroutine(Moving(movePos[i]));
            Debug.Log("움직임 : " + i + "번째");
        }

        yield return StartCoroutine(Moving(endPos));
        StartCoroutine(EndMove());
    }

    IEnumerator Moving(PositionAndSpeed pos)
    {
        while (true)
        {
            if (isMoving == false) yield break;
            lightObj.transform.position = Vector3.MoveTowards(lightObj.transform.position, pos.pos.position, Time.deltaTime * pos.speed);
            yield return null;
            if (lightObj.transform.position == pos.pos.position) break;
        }
    }

    IEnumerator EndMove()
    {
        yield return StartCoroutine(LightOff());
        isMoving = false;
    }

    IEnumerator LightOn()
    {
        lightObj.gameObject.SetActive(true);
        float curTime = 0;
        float percent = 0;
        while (curTime < readyTime)
        {
            if (isMoving == false) yield break;
            curTime += Time.deltaTime;
            percent = curTime / readyTime;

            lightObj.pointLightOuterRadius = Mathf.Lerp(0, outerRadius, percent);
            lightObj.pointLightInnerRadius = Mathf.Lerp(0, innerRadius, percent);

            spriteRenderer.color = Vector4.Lerp(activeColor, deactiveColor, percent);
            yield return null;
        }
    }

    IEnumerator LightOff()
    {
        float curTime = 0;
        float percent = 0;
        while (curTime < readyTime)
        {
            if (isMoving == false) yield break;
            curTime += Time.deltaTime;
            percent = curTime / readyTime;

            lightObj.pointLightOuterRadius = Mathf.Lerp(outerRadius, 0, percent);
            lightObj.pointLightInnerRadius = Mathf.Lerp(innerRadius, 0, percent);

            spriteRenderer.color = Vector4.Lerp(deactiveColor, activeColor, percent);
            yield return null;
        }
        lightObj.gameObject.SetActive(false);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Sword") && isMoving == false)
        {
            StartCoroutine(StartMove());
        }
    }
}
