using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    public Transform startpos;
    public Transform endpos;
    public Transform platformpos;
    public float speed;

    void Start()
    {
        transform.position = startpos.position;
        platformpos = endpos;
    }

    void FixedUpdate()
    {
       transform.position = Vector2.MoveTowards(transform.position, platformpos.position, Time.deltaTime * speed);

        if (Vector2.Distance(transform.position, platformpos.position) < 0.05f)
        {
            if (platformpos == endpos) platformpos = startpos;
            else platformpos = endpos;
        }
    }
}
