using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

/**
 * This component allows the player to move by clicking the arrow keys,
 * but only if the new position is on an allowed tile.
 */
public class KeyboardMoverByTile : KeyboardMover
{
    [SerializeField]
    Tilemap tilemap = null;

    [SerializeField]
    AllowedTiles allowedTiles = null;

    [SerializeField]
    TileBase grassTile = null;

    bool hasTreasure = false;
    const string treasureTag = "Treasure";

    private TileBase TileOnPosition(Vector3 worldPosition)
    {
        Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);
        return tilemap.GetTile(cellPosition);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(treasureTag))
        {
            Destroy(collision.gameObject);
            hasTreasure = true;
        }
    }

    void Update()
    {
        Vector3 newPosition = NewPosition();
        TileBase tileOnNewPosition = TileOnPosition(newPosition);

        if (hasTreasure)
        {
            tilemap.SetTile(Vector3Int.RoundToInt(transform.position), grassTile);
        }

        if (allowedTiles.Contain(tileOnNewPosition))
        {
            transform.position = newPosition;
        }
        else
        {
            Debug.Log("You cannot walk on " + tileOnNewPosition + "!");
        }
    }
}
