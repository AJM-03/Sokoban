using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Node node;
    public ItemTypes type;
    [HideInInspector] public bool updated;
    [HideInInspector] public bool lateUpdated;


    public virtual void InitialiseItem(Node n)
    {
        node = n;
        node.itemType = type;
    }

    public virtual void UpdateItem()
    {
        updated = true;
    }

    public virtual void LateUpdateItem()
    {
        lateUpdated = true;
    }

    public virtual void Interact(Vector2 sideInteracted)
    {

    }

    public virtual void Kick(Vector2 sideKicked)
    {

    }
}