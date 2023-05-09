using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * A graph that represents a tilemap, using only the allowed tiles.
 */
public class TilemapGraph : IGraph<Vector3Int>
{
    private Tilemap tilemap;
    private List<TileBase> allowedTiles;

    public TilemapGraph(Tilemap tilemap, List<TileBase> allowedTiles)
    {
        this.tilemap = tilemap;
        this.allowedTiles = allowedTiles;
    }

    static Vector3Int[] directions =
    {
        new Vector3Int(-1, 0, 0),
        new Vector3Int(1, 0, 0),
        new Vector3Int(0, -1, 0),
        new Vector3Int(0, 1, 0),
    };

    // get all the nodes on the tailmap
    public IEnumerable<Vector3Int> Nodes()
    {
        var nodes = new List<Vector3Int>();
        BoundsInt bounds = tilemap.cellBounds;
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                TileBase tile = tilemap.GetTile(pos);
                if (allowedTiles.Contains(tile))
                {
                    nodes.Add(pos);
                }
            }
        }
        return nodes;
    }

    public Vector3Int NodeToCell(Vector3Int node)
    {
        return node;
    }

    public IEnumerable<Vector3Int> Neighbors(Vector3Int node)
    {
        foreach (var direction in directions)
        {
            Vector3Int neighborPos = node + direction;
            TileBase neighborTile = tilemap.GetTile(neighborPos);
            if (allowedTiles.Contains(neighborTile))
                yield return neighborPos;
        }
    }
}
