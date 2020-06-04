using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot
{
    public Item obj;
    public int count;

    public void AddItem(Item o, int amount)
    {
        if (obj != null || !(obj == o && o.isStackable))
            return;

        obj = o;
        count += amount;
        //Remove from cursor.
    }
}
