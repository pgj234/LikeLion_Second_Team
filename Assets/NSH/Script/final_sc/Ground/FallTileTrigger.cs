using UnityEngine;
using UnityEngine.Tilemaps;

public class FallTileTrigger : MonoBehaviour
{
    public Tilemap fallTilemap;  // 사라지는 타일맵

    private bool hasFallen = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasFallen && collision.CompareTag("Player"))
        {
            hasFallen = true;
            RemoveAllTiles();
        }
    }

    private void RemoveAllTiles()
    {
        BoundsInt bounds = fallTilemap.cellBounds;

        // 모든 타일을 바로 제거
        for (int y = bounds.yMin; y < bounds.yMax; y++)
        {
            for (int x = bounds.xMin; x < bounds.xMax; x++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                if (fallTilemap.HasTile(pos))
                {
                    fallTilemap.SetTile(pos, null);
                }
            }
        }

        // 타일이 다 사라지면, 오브젝트 비활성화 (선택 사항)
        gameObject.SetActive(false);
    }
}
