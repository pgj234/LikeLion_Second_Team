using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;

public class ToggleTilemapPlatform_2 : MonoBehaviour
{
    public Tilemap tilemap;
    public float visibleDuration = 2f;    // 보이는 시간
    public float hiddenDuration = 2f;     // 사라지는 시간

    private Dictionary<Vector3Int, TileBase> originalTiles = new Dictionary<Vector3Int, TileBase>();
    private TilemapCollider2D tilemapCollider;

    private void Start()
    {
        tilemapCollider = tilemap.GetComponent<TilemapCollider2D>();

        // 현재 타일 상태 저장
        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            if (tilemap.HasTile(pos))
            {
                originalTiles[pos] = tilemap.GetTile(pos);
            }
        }

        // 처음에는 타일이 보인 상태에서 시작
        StartCoroutine(ToggleTilesLoop());
    }

    IEnumerator ToggleTilesLoop()
    {
        while (true)
        {
            // 보이는 상태 유지
            yield return new WaitForSeconds(visibleDuration);

            // 타일 제거 (숨김)
            tilemap.ClearAllTiles();
            tilemapCollider.enabled = false;

            yield return new WaitForSeconds(hiddenDuration);

            // 타일 복원 (보임)
            foreach (var kvp in originalTiles)
            {
                tilemap.SetTile(kvp.Key, kvp.Value);
            }
            tilemapCollider.enabled = true;
        }
    }
}
