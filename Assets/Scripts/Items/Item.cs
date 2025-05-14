using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Node node;
    public ItemTypes type;

    public virtual void InitialiseItem(Node n)
    {
        node = n;
        node.itemType = type;
    }

    public virtual void UpdateItem()
    {

    }

    public virtual void Interact()
    {

    }

    public virtual void Kick()
    {

    }
}