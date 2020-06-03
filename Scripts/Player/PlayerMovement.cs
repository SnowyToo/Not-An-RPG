﻿using System.Collections;
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
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        if(path != null)
        {
            Move();
        }
    }

    //Get the path
    public void GetPath(InteractableObject target, Interaction action)
    {
        obj = target;
        interaction = action;

        if (target != null)
            objectPosition = GetGridPosition(target.transform.position);

        Vector3Int start = GetGridPosition(player.position);

        Vector3 rawDestination = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int destination = GetGridPosition(rawDestination);

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
        if(obj != null && nextTile == GetGridPosition(objectPosition)) //If next tile is interactable, interact.
        {
            obj.Interact(interaction);
            path = null;
        }
        else
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
    }

    private Vector3Int GetGridPosition(Vector3 pos)
    {
        return new Vector3Int(Mathf.FloorToInt(pos.x + 0.5f), Mathf.FloorToInt(pos.y + 0.5f), 0);
    }
}
