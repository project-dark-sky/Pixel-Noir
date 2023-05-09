using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

/*Implementation if dijksta algo on tailmap grid*/
public class Dijkstra
{
    private const int EdgeWeight = 1;
    private const int FlowerEdgeWeight = 3;

    public static List<NodeType> FindPath<NodeType>(
        Tilemap tilemap,
        TileBase flowerTile,
        IGraph<NodeType> graph,
        NodeType startNode,
        NodeType endNode,
        List<NodeType> outputPath,
        int maxiterations = 1000
    )
    {
        Dictionary<NodeType, float> distances = graph
            .Nodes()
            .ToDictionary(node => node, node => float.MaxValue); // adding to each node in graph infity value

        Dictionary<NodeType, NodeType> prevNodes = new Dictionary<NodeType, NodeType>();

        List<NodeType> queue = new List<NodeType>();

        distances[startNode] = 0;

        queue.Add(startNode);
        int iteration = 0;

        while (queue.Count > 0 && iteration < maxiterations)
        {
            NodeType currentNode = queue.OrderBy(n => distances[n]).First();
            queue.Remove(currentNode);

            if (currentNode.Equals(endNode))
            {
                outputPath = ConstructPath(prevNodes, endNode);

                return outputPath;
            }
            foreach (NodeType neighborNode in graph.Neighbors(currentNode))
            {
                Vector3Int neighborCell = graph.NodeToCell(neighborNode);
                TileBase tile = tilemap.GetTile(neighborCell);

                bool isFlower = tile == flowerTile;

                int cost = !isFlower ? EdgeWeight : FlowerEdgeWeight; // if the neighbor is a flower means the cost is higher

                float distanceToNeighbor = distances[currentNode] + cost;

                //relaxtion
                if (distanceToNeighbor < distances[neighborNode])
                {
                    distances[neighborNode] = distanceToNeighbor;
                    prevNodes[neighborNode] = currentNode;
                    queue.Add(neighborNode);
                }
            }
            iteration++;
        }

        Debug.Log("NO PATH!!!!!!!!!!!!");

        outputPath = new List<NodeType>();
        return outputPath;
    }

    public static List<NodeType> GetPath<NodeType>(
        Tilemap tilemap,
        TileBase flowerTile,
        IGraph<NodeType> graph,
        NodeType startNode,
        NodeType endNode,
        int maxiterations = 1000
    )
    {
        List<NodeType> path = new List<NodeType>();
        path = FindPath(tilemap, flowerTile, graph, startNode, endNode, path, maxiterations);

        return path;
    }

    //get the path based on previous nodes.
    private static List<NodeType> ConstructPath<NodeType>(
        Dictionary<NodeType, NodeType> previousNodes,
        NodeType endNode
    )
    {
        List<NodeType> path = new List<NodeType>();
        NodeType currentNode = endNode;

        while (previousNodes.ContainsKey(currentNode))
        {
            path.Insert(0, currentNode);
            currentNode = previousNodes[currentNode];
        }

        path.Insert(0, currentNode);

        return path;
    }
}
