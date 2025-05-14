using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTile : Tile
{
    public override void InitialiseTile(Node n)
    {
        base.InitialiseTile(n);
    }

    public override void UpdateTile()
    {
        if (node.itemType == ItemTypes.Player)
        {
            Player.Instance.inputDelay = 100;
            GameManager.Instance.NextStage();
        }

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
