using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameState.Instance.hasKeyItem = true;
            Destroy(gameObject);
            Debug.Log("æ∆¿Ã≈€ »πµÊ!");
        }
    }
}