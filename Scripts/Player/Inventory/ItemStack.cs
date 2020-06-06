using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStack
{
    public Item obj;
    public int count;

    public ItemStack()
    {
        obj = null;
        count = 0;
    }

    public void AddItem(Item o, int amount)
    {
        obj = o;
        count += amount;
    }
}
