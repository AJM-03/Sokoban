using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class BoxItem : Item
{
    public GameObject pushEffect;

    public override void InitialiseItem(Node n)
    {
        base.InitialiseItem(n);
    }

    public override void UpdateItem()
    {
        base.UpdateItem();
    }

    public override void Interact(Vector2 sideInteracted)
    {
        base.Interact(sideInteracted);
    }

    public override void Kick(Vector2 sideKicked)
    {
        Node targetNode = null;
        bool valid = true;
        if (sideKicked == new Vector2(0, 1)) targetNode = node.bottom;
        if (sideKicked == new Vector2(0, -1)) targetNode = node.top;
        if (sideKicked == new Vector2(1, 0)) targetNode = node.left;
        if (sideKicked == new Vector2(-1, 0)) targetNode = node.right;

        if (targetNode == null) valid = false;
        if (valid)
        {
            if (targetNode.item != null) valid = false;
            if (targetNode.tileType == TileTypes.Null) valid = false;
            if (targetNode.tile.blocksObjects) valid = false;
        }

        if (valid)
        {
            node.item = null;
            node.itemType = ItemTypes.None;
            node.itemObject = null;

            if (pushEffect != null && sideKicked.y == 0)
            {
                GameObject e = GameObject.Instantiate(pushEffect, transform.position, transform.rotation);
                e.GetComponentInChildren<SpriteRenderer>().flipX = sideKicked == new Vector2(1, 0) ? true : false;  
            }

            transform.position = targetNode.transform.position;
            transform.parent = targetNode.transform;
            node = targetNode;

            targetNode.item = this;
            targetNode.itemType = ItemTypes.Box;
            targetNode.itemObject = gameObject;
        }

        base.Kick(sideKicked);
    }
}
