using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Drop : Interaction
{
    private Item item;

    public void Start()
    {
        actionName = "Drop";
    }

    public override void SetParent(InteractableObject _parent)
    {
        base.SetParent(_parent);
        item = _parent as Item;
    }

    public override void Act()
    {
        Instantiate(item.prefab, GameManager.player.position, Quaternion.identity);
        parent.Interupt();
    }
}
