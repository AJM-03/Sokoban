using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Node node;
    public TileTypes type;
    public TileData data;
    public bool blocksObjects;

    public virtual void InitialiseTile(Node n)
    {
        node = n;
        node.tileType = type;
    }

    public virtual void UpdateTile()
    {

    }

    public virtual void LateUpdateTile()
    {

    }

    public virtual void Interact(Vector2 sideInteracted)
    {

    }

    public virtual void Kick(Vector2 sideKicked)
    {

    }
}
