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
            Debug.Log("Hit Player");
        }
    }
}
