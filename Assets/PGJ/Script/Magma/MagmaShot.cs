using UnityEngine;
using static UnityEngine.LightAnchor;

public class MagmaShot : MonoBehaviour
{
    [Header("발사 오브젝트 설정")]
    [SerializeField] GameObject shotObj;
    [SerializeField] float firstDelay;
    [SerializeField] float shotPower;
    [SerializeField] float shotDelayTime;

    [Space(10)]
    [Header("집탄률 최대 각도 (0 <---정밀   분산---> 180)")]
    [Range(0f, 180f)]
    [SerializeField] float moaAngle;

    float ranAngle;
    float rad;
    float rotateX;
    float rotateY;

    GameObject createObj;

    float shotDelayTimer;

    void Awake()
    {
        Init();
    }

    void Init()
    {
        shotDelayTimer = firstDelay;
    }

    void Update()
    {
        shotDelayTimer -= Time.deltaTime;

        if (shotDelayTimer < 0)
        {
            shotDelayTimer = shotDelayTime;

            createObj = Instantiate(shotObj, transform.position, Quaternion.identity);
            Debug.Log(createObj.transform.position);
            ranAngle = Random.Range(moaAngle * -0.5f, moaAngle * 0.5f);
            rad = ranAngle * Mathf.Deg2Rad;

            rotateX = transform.up.x * Mathf.Cos(rad) - transform.up.y * Mathf.Sin(rad);
            rotateY = transform.up.x * Mathf.Sin(rad) + transform.up.y * Mathf.Cos(rad);
            createObj.GetComponent<Rigidbody2D>().AddForce(new Vector2(rotateX, rotateY) * shotPower, ForceMode2D.Impulse);
        }
    }
}
