using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PathfindingTile : Tile
{
    public bool isWalkable;

#if UNITY_EDITOR
    // The following is a helper that adds a menu item to create a RoadTile Asset
    [MenuItem("Assets/Create/PathfindingTile")]
    public static void CreatePathfindingTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save Pathfinding Tile", "New Pathfinding Tile", "Asset", "Save Pathfinding Tile", "Assets");
        if (path == "")
            return;
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<PathfindingTile>(), path);
    }
#endif
}


