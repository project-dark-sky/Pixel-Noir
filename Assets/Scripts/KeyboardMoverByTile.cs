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

    private bool hasTreasure = false;
    private const string treasureTag = "Treasure";

    private TileBase TileOnPosition(Vector3 worldPosition)
    {
        Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);
        return tilemap.GetTile(cellPosition);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //check if the player took the treasure
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

        if (allowedTiles.Contain(tileOnNewPosition))
        {
            transform.position = newPosition;
        }
        else
        {
            tilemap.SetTile(Vector3Int.RoundToInt(newPosition), grassTile);
            Debug.Log("You cannot walk on " + tileOnNewPosition + "!");
        }

        //if he took the treasure draw grass wherever he walks
        if (hasTreasure)
        {
            tilemap.SetTile(Vector3Int.RoundToInt(newPosition), grassTile);
        }
    }
}
