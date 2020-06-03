using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum TileType { START, GOAL, UNWALKABLE, WALKABLE }

public class Pathfinding : MonoBehaviour
{
    private TileType tileType;

    public Tilemap map;

    private Vector3Int start, destination;

    private Node current;

    private Stack<Vector3Int> path;

    private Dictionary<Vector3Int, Node> nodes = new Dictionary<Vector3Int, Node>();
    private HashSet<Node> openNodes;
    private HashSet<Node> closedNodes;
    

    protected Stack<Vector3Int> Algorithm(Vector3Int _start, Vector3Int _dest)
    {
        start = _start;
        destination = _dest;      

        Initialize();

        while (openNodes.Count > 0 && path == null)
        {
            List<Node> neighbors = GetNeighbors(current.Position);

            ExamineNeighbors(neighbors, current);
            UpdateCurrentTile(ref current);
            path = GeneratePath(current);
        }

        PathfindingDebug.Ins.CreateTiles(openNodes, closedNodes, start, destination, path);

        return path;
    }

    private void Initialize()
    {
        current = GetNode(start);
        openNodes = new HashSet<Node>();
        closedNodes = new HashSet<Node>();
        PathfindingDebug.Ins.tilemap.ClearAllTiles();
        path = null;

        openNodes.Add(current);
    }

    private List<Node> GetNeighbors(Vector3Int parentPos)
    {
        List<Node> neighbors = new List<Node>();
        
        for(int x = -1; x <= 1; x++)
        {
            for(int y = -1; y <= 1; y++)
            {
                if(y != 0 || x != 0)
                {
                    Vector3Int neighborPos = new Vector3Int(parentPos.x + x, parentPos.y + y, parentPos.z);
                    if(neighborPos != start && map.GetTile(neighborPos))
                    {
                        Node neighbor = GetNode(neighborPos);
                        neighbors.Add(neighbor);
                    }
                }
            }
        }

        return neighbors;
    }

    private void ExamineNeighbors(List<Node> neighbors, Node current)
    {
        for(int i = 0; i < neighbors.Count; i++)
        {
            Node n = neighbors[i];
            int gScore = DetermineGScore(n, current);

            if(!IsConnectedDiagonally(current, n))
            {
                continue;
            }

            if(IsWalkable(n))
            {
                if(openNodes.Contains(n))
                {
                    if(current.G + gScore < n.G)
                    {
                        CalcValues(current, n, gScore);
                    }
                }
                else if(!closedNodes.Contains(n))
                {
                    CalcValues(current, n, gScore);
                    openNodes.Add(n);
                }
            }
        }
    }

    private void CalcValues(Node parent, Node neighbor, int cost)
    {
        neighbor.Parent = parent;

        neighbor.G = parent.G + cost;

        neighbor.H = (Math.Abs(neighbor.Position.x - destination.x) + Math.Abs(neighbor.Position.y - destination.y)) * 10;

        neighbor.F = neighbor.G + neighbor.H;
    }

    private int DetermineGScore(Node neighbor, Node current)
    {
        int gScore = 0;
        int x = current.Position.x - neighbor.Position.x;
        int y = current.Position.y - neighbor.Position.y;

        if(Math.Abs(x-y) % 2 == 1)
        {
            gScore = 10;
        }
        else
        {
            gScore = 14;
        }

        return gScore;
    }

    private void UpdateCurrentTile(ref Node current)
    {
        openNodes.Remove(current);

        closedNodes.Add(current);

        if(openNodes.Count > 0)
        {
            current = openNodes.OrderBy(x => x.F).First();
        }
    }

    private Stack<Vector3Int> GeneratePath(Node current)
    {
        if(current.Position == destination)
        {
            Stack<Vector3Int> path = new Stack<Vector3Int>();

            while(current.Position != start)
            {
                path.Push(current.Position);
                current = current.Parent;
            }

            return path;
        }
        return null;
    }

    private Node GetNode(Vector3Int pos)
    {
        if (nodes.ContainsKey(pos))
            return nodes[pos];
        else
        {
            Node node = new Node(pos);
            nodes.Add(pos, node);
            return node;
        }
    }

    private bool IsConnectedDiagonally(Node curernt, Node neighbor)
    {
        Vector3Int direction = current.Position - neighbor.Position;

        Vector3Int first = new Vector3Int(current.Position.x + (direction.x * -1), current.Position.y, current.Position.z);
        Vector3Int second = new Vector3Int(current.Position.x, current.Position.y + direction.y * -1, current.Position.z);

        return IsWalkable(first) && IsWalkable(second);
    }



    private bool IsWalkable(Node node)
    {
        return IsWalkable(node.Position);
    }

    private bool IsWalkable(Vector3Int pos)
    {
        return map.GetTile<PathfindingTile>(pos).isWalkable;
    }
}
