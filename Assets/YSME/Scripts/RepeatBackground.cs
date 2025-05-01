using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private PlayerManager pm;

    [Header("스크롤 스피드")]
    [SerializeField] private float scrollSpeed;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        pm = PlayerManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (pm.player.GetVelocity().x < 0)
        {
            Vector2 offset = new Vector2(meshRenderer.material.mainTextureOffset.x - Time.deltaTime * scrollSpeed, 0);
            meshRenderer.material.mainTextureOffset = offset;
        }
        else if (pm.player.GetVelocity().x > 0)
        {
            Vector2 offset = new Vector2(meshRenderer.material.mainTextureOffset.x + Time.deltaTime * scrollSpeed, 0);
            meshRenderer.material.mainTextureOffset = offset;
        }
    }
}
