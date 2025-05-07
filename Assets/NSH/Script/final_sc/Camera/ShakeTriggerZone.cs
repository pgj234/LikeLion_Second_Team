using UnityEngine;

public class ShakeTriggerZone : MonoBehaviour
{
    public CameraShakeCinemachine cameraShake;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cameraShake.StrongShake();
        }
    }
}
