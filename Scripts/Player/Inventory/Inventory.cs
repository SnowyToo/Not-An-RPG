using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public ItemStack[] items;

    public Inventory()
    {
        items = new ItemStack[20];
        for(int i = 0; i < items.Length; i++)
        {
            items[i] = new ItemStack();
        }
    }

    //Add item at the end of the inventory.
    public void AddItem(Item item, int count)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].obj == null || (items[i].obj == item && items[i].obj.isStackable))
            {
                //Change Pick Up with Drop
                item.interactions[0] = item.gameObject.AddComponent<Drop>();
                //Add to inv in a very very roundabout way
                items[i].AddItem(item, count);
                GameManager.uiManager.UpdateInventory();
                return;
            }
        }
        throw new System.Exception("No room in inventory");
    }
}
