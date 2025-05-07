using UnityEngine;
using Unity.Cinemachine;

public class CameraShakeCinemachine : MonoBehaviour
{
    public CinemachineImpulseSource weakShakeSource;
    public CinemachineImpulseSource strongShakeSource;
    public float weakShakeInterval = 2f;

    void Start()
    {
        InvokeRepeating(nameof(WeakShake), 0f, weakShakeInterval);
    }

    public void WeakShake()
    {
        weakShakeSource.GenerateImpulse();
    }

    public void StrongShake()
    {
        strongShakeSource.GenerateImpulse();
    }
}
