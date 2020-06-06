using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    int index;

    private void Awake()
    {
        index = transform.GetSiblingIndex();
    }

    public void OnPointerClick(PointerEventData data)
    {
        //Get Itemstack
        ItemStack stack = GameManager.inv.items[index];

        Debug.Log(stack.count);

        if (data.button == PointerEventData.InputButton.Right)
        {
            //Show Right-click menu.
            GameManager.uiManager.SetUpRightClickMenu(stack.obj);
        }
    }
}
