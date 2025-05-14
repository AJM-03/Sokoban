using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;
using UnityEngine.U2D;

public class Enemy : Item
{
    public bool horizontalMovement;
    public bool flipDirection;
    public SpriteRenderer rend;

    public override void InitialiseItem(Node n)
    {
        base.InitialiseItem(n);
    }

    public override void UpdateItem()
    {
        if (!updated)
        {
            Node targetNode = null;
            bool valid = true;
            rend.flipX = flipDirection;
            if (horizontalMovement)
            {
                if (flipDirection) targetNode = node.left;
                else targetNode = node.right;
            }
            else
            {
                if (flipDirection) targetNode = node.top;
                else targetNode = node.bottom;
            }


            if (targetNode == null) valid = false;
            if (valid)
            {
                if (targetNode.itemType == ItemTypes.Player)
                {
                    valid = false;
                    Player.Instance.KillPlayer();
                }
                if (targetNode.item != null) valid = false;
                if (targetNode.tileType == TileTypes.Null) valid = false;
            }


            if (valid)
            {
                Debug.Log(targetNode + " - " + flipDirection);
                node.item = null;
                node.itemType = ItemTypes.None;
                node.itemObject = null;

                transform.position = targetNode.transform.position;
                transform.parent = targetNode.transform;
                node = targetNode;

                targetNode.item = this;
                targetNode.itemType = ItemTypes.Enemy;
                targetNode.itemObject = gameObject;
            }
            else
            {
                flipDirection = !flipDirection;
                rend.flipX = flipDirection;
            }
        }


        base.UpdateItem();
    }

    public override void Interact(Vector2 sideInteracted)
    {

        base.Interact(sideInteracted);
    }

    public override void Kick(Vector2 sideKicked)
    {
        Player.Instance.KillPlayer();

        base.Kick(sideKicked);
    }
}
