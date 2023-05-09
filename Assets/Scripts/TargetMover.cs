using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * This component moves its object towards a given target position.
 */
public class TargetMover : MonoBehaviour
{
    [SerializeField]
    Tilemap tilemap = null;

    [SerializeField]
    AllowedTiles allowedTiles = null;

    [Tooltip(
        "The speed by which the object moves towards the target, in meters (=grid units) per second"
    )]
    [SerializeField]
    float speed = 2f;

    [Tooltip("Maximum number of iterations before BFS algorithm gives up on finding a path")]
    [SerializeField]
    int maxIterations = 1000;

    [Tooltip("The target position in world coordinates")]
    [SerializeField]
    Vector3 targetInWorld;

    [Tooltip("The target position in grid coordinates")]
    [SerializeField]
    Vector3Int targetInGrid;

    [SerializeField]
    TileBase stoneTile;

    [SerializeField]
    TileBase flowerTile;
    protected bool atTarget; // This property is set to "true" whenever the object has already found the target.

    public void SetTarget(Vector3 newTarget)
    {
        if (targetInWorld != newTarget)
        {
            targetInWorld = newTarget;
            targetInGrid = tilemap.WorldToCell(targetInWorld);
            atTarget = false;
        }
    }

    public Vector3 GetTarget()
    {
        return targetInWorld;
    }

    private TilemapGraph tilemapGraph = null;
    private float timeBetweenSteps;

    protected virtual void Start()
    {
        tilemapGraph = new TilemapGraph(tilemap, allowedTiles.Get());
        timeBetweenSteps = 1 / speed;
        StartCoroutine(MoveTowardsTheTarget());
    }

    IEnumerator MoveTowardsTheTarget()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(timeBetweenSteps);
            if (enabled && !atTarget)
                MakeOneStepTowardsTheTarget();
        }
    }

    private void MakeOneStepTowardsTheTarget()
    {
        Vector3Int startNode = tilemap.WorldToCell(transform.position);
        Vector3Int endNode = targetInGrid;
        List<Vector3Int> shortestPath = Dijkstra.GetPath(
            tilemap,
            flowerTile,
            tilemapGraph,
            startNode,
            endNode,
            maxIterations
        );

        if (shortestPath.Count >= 2)
        { // shortestPath contains both source and target.
            Vector3Int nextNode = shortestPath[1];
            Vector3 nextNodePosition = tilemap.GetCellCenterWorld(nextNode);

            TileBase nextTile = tilemap.GetTile(nextNode);
            if (nextTile == flowerTile)
            { // check if next tile is a flower tile
                // if it is, decrease the speed of the player
                transform.position +=
                    (nextNodePosition - transform.position).normalized
                    * (speed / 2)
                    * timeBetweenSteps;
            }
            else
            {
                // if it's not a flower tile, move at normal speed
                transform.position +=
                    (nextNodePosition - transform.position).normalized * speed * timeBetweenSteps;
            }
        }
        else
        {
            atTarget = true;
        }
    }
}
