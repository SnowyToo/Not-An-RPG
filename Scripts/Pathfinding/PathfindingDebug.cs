using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathfindingDebug : MonoBehaviour
{
    private static PathfindingDebug ins;

    public static PathfindingDebug Ins
    {
        get
        {
            if(ins == null)
            {
                ins = FindObjectOfType<PathfindingDebug>();
            }
            return ins;
        }
    }

    public Tilemap tilemap;

    public Tile tile;

    public Color openColor, closedColor, pathColor, currentColor, startColor, goalColor;

    public void CreateTiles(HashSet<Node> openNodes, HashSet<Node> closedNodes, Vector3Int start, Vector3Int goal, Stack<Vector3Int> path = null)
    {
        foreach (Node n in openNodes)
        {
            ColorTile(n.Position, openColor);
        }
        foreach (Node n in closedNodes)
        {
            ColorTile(n.Position, closedColor);
        }

        if(path != null)
        {
            foreach(Vector3Int v in path)
            {
                ColorTile(v, pathColor);
            }
        }
        else
        {
            Debug.LogWarning("I cannot reach that!");
        }

        ColorTile(start, startColor);
        ColorTile(goal, goalColor);
    }

    public void ColorTile(Vector3Int position, Color color)
    {
        tilemap.SetTile(position, tile);
        tilemap.SetTileFlags(position, TileFlags.None);
        tilemap.SetColor(position, color);
    }
}
