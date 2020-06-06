using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : InteractableObject
{
    public Sprite sprite;
    public bool isStackable;

    public GameObject prefab;

    public override void Awake()
    {
        base.Awake();
        sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    public bool Equals(Item o)
    {
        return sprite == o.sprite;
    }
}
