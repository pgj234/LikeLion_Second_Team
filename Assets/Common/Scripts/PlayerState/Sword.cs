using UnityEngine;
using DG.Tweening;

public class Sword : MonoBehaviour
{
    public float swingAngle = 90f;              // 휘두를 각도 (예: 90도)
    public float swingDuration = 0.2f;          // 휘두르는 시간 (초)
    public GameObject slashObject;              // 활성화할 오브젝트
    public Transform swordPoint;                // 칼 회전점

    private bool isSwinging = false;
    private Quaternion originalRotation;

    void Start()
    {
        originalRotation = transform.localRotation;
        if (slashObject != null)
        {
            slashObject.SetActive(false);
        }
    }

    void Update()
    {
        if (!isSwinging && InputManager.instance.leftClick)
        {
            StartSwing();
        }
    }

    void StartSwing()
    {
        isSwinging = true;

        // 오브젝트 활성화
        if (slashObject != null)
        {
            slashObject.SetActive(true);
            // 0.1초 후 비활성화
            DOVirtual.DelayedCall(0.1f, () => slashObject.SetActive(false));
        }

        // DOTween으로 회전 후 복귀
        swordPoint
            .DOLocalRotate(new Vector3(0, 0, swingAngle), swingDuration / 2, RotateMode.LocalAxisAdd)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                swordPoint
                    .DOLocalRotate(new Vector3(0, 0, -swingAngle), swingDuration / 2, RotateMode.LocalAxisAdd)
                    .SetEase(Ease.InQuad)
                    .OnComplete(() =>
                    {
                        isSwinging = false;
                        transform.localRotation = originalRotation; // 정확하게 되돌림
                    });
            });
    }
}
