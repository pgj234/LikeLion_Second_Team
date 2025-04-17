using UnityEngine;

public class Entity : MonoBehaviour
{
    internal Animator anim { get; private set; }
    internal Rigidbody2D rb { get; private set; }

    protected LayerMask groundLayerMask;

    internal int faceDir { get; private set; } = 1;             // ¿ÞÂÊ -1, ¿À¸¥ÂÊ 1

    protected virtual void Awake()
    {
        groundLayerMask = LayerMask.GetMask("Ground");

        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
    
    }

    internal void Damage()
    {

    }

    public virtual void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.linearVelocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
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
