using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;
using UnityEngine.U2D;
using UnityEngine.InputSystem.Processors;

public class Enemy : Item
{
    public bool horizontalMovement;
    public bool mutualDestructionPossible = true;
    public bool flipDirection;
    public SpriteRenderer rend;
    public GameObject deathEffect;
    public bool canDieToSpikes;
    [HideInInspector] public bool dead;
    [HideInInspector] public bool turnedThisUpdate;

    public override void InitialiseItem(Node n)
    {
        base.InitialiseItem(n);
    }

    public override void UpdateItem()
    {
        if (!updated && !dead)
        {
            Node targetNode = null;
            bool valid = true;
            rend.flipX = flipDirection;
            turnedThisUpdate = false;
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
                if (targetNode.itemType == ItemTypes.Enemy)
                {
                    Enemy otherEnemy = targetNode.item as Enemy;
                    if (otherEnemy != null && otherEnemy.updated && !otherEnemy.turnedThisUpdate && 
                        !otherEnemy.dead && mutualDestructionPossible && otherEnemy.mutualDestructionPossible)
                    {
                        valid = false;
                        otherEnemy.KillEnemy(true);
                        KillEnemy(false);
                    }
                }
                if (targetNode.item != null) valid = false;
                if (targetNode.tileType == TileTypes.Null) valid = false;
                if (targetNode.tile && targetNode.tile.blocksObjects) valid = false;
            }


            if (valid)
            {
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
                turnedThisUpdate = true;
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

    public void KillEnemy(bool playEffect = true)
    {
        StartCoroutine(IEKillEnemy(playEffect));
    }


    private IEnumerator IEKillEnemy(bool playEffect)
    {
        dead = true;

        node.item = null;
        node.itemType = ItemTypes.None;
        node.itemObject = null;

        if (deathEffect != null && playEffect)
        {
            GameObject effect = GameObject.Instantiate(deathEffect, transform.position, transform.rotation);
        }
        yield return new WaitForSeconds(0.25f);
        transform.GetChild(0).gameObject.SetActive(false);
        yield return new WaitForSeconds(0.75f);

        Destroy(gameObject);
    }
}
