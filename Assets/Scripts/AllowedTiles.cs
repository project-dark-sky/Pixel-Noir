using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * This component just keeps a list of allowed tiles.
 * Such a list is used both for pathfinding and for movement.
 */
public class AllowedTiles : MonoBehaviour
{
    [SerializeField]
    List<TileBase> allowedTiles = null;

    public bool Contain(TileBase tile)
    {
        return allowedTiles.Contains(tile);
    }

    public void InsertTile(TileBase tile)
    {
        allowedTiles.Add(tile);
    }

    public List<TileBase> Get()
    {
        return allowedTiles;
    }
}
