using UnityEngine;

public class OutofFluidPoint : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (col.TryGetComponent(out Player player))
            {
                player.ghostAvailable = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (col.TryGetComponent(out Player player))
            {
                player.ghostAvailable = false;
            }
        }
    }
}
