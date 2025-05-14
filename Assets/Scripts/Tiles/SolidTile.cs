using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidTile : Tile
{
    public override void InitialiseTile(Node n)
    {
        base.InitialiseTile(n);
    }

    public override void UpdateTile()
    {
        base.UpdateTile();
    }

    public override void Interact(Vector2 sideInteracted)
    {
        base.Interact(sideInteracted);
    }

    public override void Kick(Vector2 sideKicked)
    {
        base.Kick(sideKicked);
    }
}
