using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SoundManager.instance.PlaySFX(SFX_KYW.TwinkleSound);            //메테오소리
            GameState.Instance.hasKeyItem = true;
            Destroy(gameObject);
            Debug.Log("������ ȹ��!");
        }
    }
}