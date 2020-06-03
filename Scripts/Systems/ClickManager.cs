using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickManager : MonoBehaviour
{
    public GameObject click;

    
    private InteractableObject lastClicked;

    // Update is called once per frame
    private void Update()
    {
        
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            Camera cam = GameManager.cam;
            //Camera Zoom
            cam.orthographicSize -= Input.GetAxisRaw("Mouse ScrollWheel");
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, 3, 10);

            if (Input.GetMouseButtonDown(0)) //Left click == Primary interact.
            {
                //If we're interacting, we want to stop interacting.
                if (lastClicked != null)
                {
                    lastClicked.Interupt();
                }
                GameObject c = Instantiate(click, cam.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward, Quaternion.identity);


                //Set up interaction to be passed.
                lastClicked = ClickedObject();
                if (lastClicked != null)
                    lastClicked.interactions[0].Interact();
                else
                {

                }

                //Walk towards appropriate tile.
                GameManager.playerMovement.GetPath(lastClicked);
            }
            else if(Input.GetMouseButtonDown(1)) //Right click == Menu dropdown
            {
                //Do Menu
                GameManager.uiManager.SetUpRightClickMenu(ClickedObject());
            }
        }
    }

    //Get any objects at the cursor position.
    private InteractableObject ClickedObject()
    {
        RaycastHit2D hit = Physics2D.Raycast(GameManager.cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null)
        {
            if (hit.transform.tag == "Interactable")
            {
                return hit.transform.GetComponent<InteractableObject>();
            }
        }
        return null;
    }
}
