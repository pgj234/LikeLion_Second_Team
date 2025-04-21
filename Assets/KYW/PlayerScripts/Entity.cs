using Unity.VisualScripting;
using UnityEngine;

public class Entity : MonoBehaviour
{
    //[SerializeField] protected int maxHP;
    //[SerializeField] protected int maxStamina;
    //protected int curHP;        // 현재 체력
    //protected int curStamina;   // 현재 스태미너

    internal Animator anim { get; private set; }
    internal Rigidbody2D rb { get; private set; }
    internal int faceDir { get; set; } = 1;   // 왼쪽 -1, 오른쪽 1
    protected virtual void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
<<<<<<<< HEAD:Assets/PGJ/Scripts/Entity.cs

========
>>>>>>>> KYW:Assets/KYW/PlayerScripts/Entity.cs
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
