using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private PlayerManager pm;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        pm = PlayerManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
