using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : Pathfinding
{
    private Camera cam;
    private Transform player;

    private Vector3Int nextTile;
    private Stack<Vector3Int> path;

    private InteractableObject obj;
    private Interaction interaction;
    private Vector3Int objectPosition;

    public int speed;

    private void Start()
    {
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        player = GameManager.player;
    }

    private void Update()
    {
        //If we have a path, start moving.
        if (path != null)
        {
            Move();
        }
    }

    //Get the path
    public void GetPath(Vector3 rawDestination)
    {
        Vector3Int start = GameManager.tilemapManager.GetGridPosition(player.position);
        Vector3Int destination = GameManager.tilemapManager.GetGridPosition(rawDestination);

        //Make sure pathfinding doesn't get interrupted for null paths.
        //Players who click on unwalkable ties while walking won't be stopped mid-path.
        Stack<Vector3Int> dummy = Algorithm(start, destination);
        if (dummy != null)
        {
            path = dummy;
            nextTile = path.Pop();
        }
    }

    //Move the player
    private void Move()
    {
        player.position = Vector3.MoveTowards(player.position, nextTile, speed * Time.deltaTime);

        float distance = Vector3.Distance(nextTile, player.position);
        if (distance == 0f)
        {
            if(path.Count > 0) //Check if final tile
            {
                nextTile = path.Pop();
                return;
            }
            path = null; //End of Path
        }
    }

    public bool IsNextToObject(InteractableObject o)
    {
        if (nextTile == GameManager.tilemapManager.GetGridPosition(o.transform.position))
        {
            path = null;
            return true;
        }
        return false;
    }
}
