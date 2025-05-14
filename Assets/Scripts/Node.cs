using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Node top;
    public Node bottom;
    public Node left;
    public Node right;

    public TileTypes tileType;
    public Tile tile;
    public GameObject tileObject;

    public ItemTypes itemType;
    public Item item;
    public GameObject itemObject;

    public LayerMask nodeLayer;


    public void InitialiseNode()
    {
        // Top
        RaycastHit2D hit = Physics2D.Linecast(
            transform.position + new Vector3(0, 0.4f, 0), 
            transform.position + new Vector3(0, 0.8f, 0),
            nodeLayer);

        if (hit.collider != null) top = hit.collider.gameObject.GetComponent<Node>();


        // Bottom
        hit = Physics2D.Linecast(
            transform.position + new Vector3(0, -0.4f, 0),
            transform.position + new Vector3(0, -0.8f, 0),
            nodeLayer);

        if (hit.collider != null) bottom = hit.collider.gameObject.GetComponent<Node>();


        // Left
        hit = Physics2D.Linecast(
            transform.position + new Vector3(-0.4f, 0, 0),
            transform.position + new Vector3(-0.8f, 0, 0),
            nodeLayer);

        if (hit.collider != null) left = hit.collider.gameObject.GetComponent<Node>();


        // Right
        hit = Physics2D.Linecast(
            transform.position + new Vector3(0.4f, 0, 0),
            transform.position + new Vector3(0.8f, 0, 0),
            nodeLayer);

        if (hit.collider != null) right = hit.collider.gameObject.GetComponent<Node>();





        tile = transform.GetComponentInChildren<Tile>();
        if (tile != null)
        {
            tileObject = tile.gameObject;
            tile.InitialiseTile(this);
        }
        else
        {
            tileType = TileTypes.Null;
        }



        item = transform.GetComponentInChildren<Item>();
        if (item != null)
        {
            itemObject = item.gameObject;
            item.InitialiseItem(this);
        }
        else
        {
            itemType = ItemTypes.None;
        }
    }


    public void UpdateTiles()
    {
        if (tile != null)
        {
            tile.UpdateTile();
        }
    }


    public void UpdateItems()
    {
        if (item != null && itemType != ItemTypes.Player)
        {
            item.UpdateItem();
        }
    }


    public void PostUpdate()
    {
        if (item != null)
        {
            item.updated = false;
        }
    }


    public void Interact(Vector2 sideInteracted)
    {
        if (itemType == ItemTypes.None)
        {
            if (tile != null) tile.Interact(sideInteracted);

            if (tileType == TileTypes.Wall)
            {
                Player.Instance.QuestionReaction();
            }

            else if (Player.Instance.heldTile == null)
            {
                if (tile == null)
                {
                    Player.Instance.QuestionReaction();
                }
                else
                {
                    Player.Instance.PickUpTile(tile);
                }
            }

            else
            {
                if (tile == null)
                {
                    Player.Instance.PlaceTile(this);
                }
                else
                {
                    Player.Instance.QuestionReaction();
                }
            }
        }
        else
        {
            if (item != null)
            {
                item.Interact(sideInteracted);
            }
        }
    }


    public void KickNode()
    {

    }
}



public enum TileTypes
{
    Solid,
    Null,
    Wall,
    Button,
    Exit
}

public enum ItemTypes
{
    None,
    Player,
    Spikes,
    Chest,
    Enemy,
    Box
}