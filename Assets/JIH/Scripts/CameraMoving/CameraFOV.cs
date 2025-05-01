using Unity.Cinemachine;
using UnityEngine;

public class CameraFOV : MonoBehaviour
{

    public CinemachineCamera virtualCamera; // CinemachineCamera -> CinemachineVirtualCamera
    public float startFOV = 10f;
    public float endFOV = 6f;
    public float duration = 2f;

    private float elapsedTime = 0f;

    void Start()
    {
        if (virtualCamera == null)
        {
            Debug.LogError("Virtual Camera가 할당되지 않았습니다!");
            return;
        }
        virtualCamera.Lens.FieldOfView = startFOV;
    }

    void Update()
    {
        if (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            virtualCamera.Lens.FieldOfView = Mathf.Lerp(startFOV, endFOV, t);
        }
    }
}
