using Unity.Cinemachine;
using UnityEngine;

public class ShakeTriggerZone : MonoBehaviour
{
    public CinemachineImpulseSource impulseSource;
    public float shakeStrength = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            impulseSource.GenerateImpulseAt(transform.position, Vector3.one * shakeStrength);
        }
    }
}
