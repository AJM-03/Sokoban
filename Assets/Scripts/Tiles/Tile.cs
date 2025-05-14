using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Node node;
    public TileTypes type;
    public TileData data;

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

    }

    public virtual void Kick(Vector2 sideKicked)
    {

    }
}
