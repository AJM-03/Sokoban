using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Node node;
    public TileTypes type;
    public GameObject prefab;

    public virtual void InitialiseTile(Node n)
    {
        node = n;
        node.tileType = type;
    }

    public virtual void UpdateTile()
    {

    }

    public virtual void Interact()
    {
        Player.Instance.PickUpTile(this);
    }

    public virtual void Kick()
    {

    }
}
