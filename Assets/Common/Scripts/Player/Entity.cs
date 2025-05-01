using Unity.VisualScripting;
using UnityEngine;

public class Entity : MonoBehaviour
{
    internal Animator anim { get; private set; }
    internal Rigidbody2D rb { get; private set; }
    internal SpriteRenderer spriteRenderer { get; private set; }
    internal int faceDir { get; set; } = 1;   // 왼쪽 -1, 오른쪽 1
    [Header("Parrying Check")]
    public Transform ParryingCheck;
    public float ParryingCheckRadius;
    protected virtual void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public virtual void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.linearVelocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }

    public virtual Vector2 GetVelocity()
    {
        return rb.linearVelocity;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(ParryingCheck.position, ParryingCheckRadius);

    }
    protected void FlipController(float _x)
    {
        if (_x > 0 && -1 == faceDir)
        {
            Flip();
        }
        else if (_x < 0 && 1 == faceDir)
        {
            Flip();
        }
    }
    internal void Flip()
    {
        faceDir = faceDir * -1;
        transform.Rotate(0, 180, 0);
        rb.linearVelocity = new Vector2(0, rb.linearVelocityY);
    }
}
