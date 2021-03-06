﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickManager : MonoBehaviour
{
    public GameObject click;

    public Item i;
    
    private InteractableObject lastClicked;
    private bool isInteracting;

    // Update is called once per frame
    private void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            Camera cam = GameManager.cam;
            //Camera Zoom
            cam.orthographicSize -= Input.GetAxisRaw("Mouse ScrollWheel");
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, 3, 10);

            Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetMouseButtonDown(0)) //Left click == Move or Interact
            {
                
                //If we're interacting, we want to stop interacting.
                if (isInteracting)
                {
                    isInteracting = false;
                    lastClicked.Interupt();
                }
                GameObject c = Instantiate(click, mousePos + Vector3.forward, Quaternion.identity);


                //If we click an object, interact with it.
                lastClicked = ClickedObject();
                if (lastClicked != null)
                {
                    lastClicked.Interact(lastClicked.interactions[0]);
                    isInteracting = true;
                }
                else //Otherwise, walk to the tile.
                {
                    GameManager.playerMovement.GetPath(mousePos);
                }
            }
            else if(Input.GetMouseButtonDown(1)) //Right click == Menu dropdown
            {
                //Do Menu
                GameManager.uiManager.SetUpRightClickMenu(ClickedObject());

                //Debug.Log(GameManager.tilemapManager.GetObjectsAtPosition(mousePos));
            }

            else if(Input.GetMouseButtonDown(2)) //Middle click = store item
            {
                GameManager.tilemapManager.SetObjectAtPosition(mousePos, i);
            }
        }
    }

    //Get the top-most object at the cursor position.
        //TODO: STORE ITEM IN TILE INSTEAD.
    private InteractableObject ClickedObject()
    {
        /*RaycastHit2D hit = Physics2D.Raycast(GameManager.cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null)
        {
            if (hit.transform.tag == "Interactable")
            {
                return hit.transform.GetComponent<InteractableObject>();
            }
        }*/
        return GameManager.tilemapManager.GetObjectsAtPosition(GameManager.cam.ScreenToWorldPoint(Input.mousePosition))[0] as InteractableObject;
    }
}
