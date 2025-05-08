using UnityEngine;

public class TeleportOnTrigger : MonoBehaviour
{

    public Transform pointB; // B ÁöÁ¡ÀÇ Transform

    private void OnTriggerEnter2D(Collider2D other)
    {

        other.transform.position = pointB.position;


    }
}
