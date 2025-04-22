using UnityEngine;

public class ObjectState : MonoBehaviour
{
    public float moveSpeed;
    public int damage;
    public virtual void OnParried()
    {
        Debug.Log("Parried");
    }
    public virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit Player (3D Collision)");
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Hit Player (2D Trigger)");
        }
    }
}
