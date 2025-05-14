using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ButtonTile : Tile
{
    public Sprite unpressedSprite, pressedSprite;
    private SpriteRenderer rend;
    private bool down;

    public override void InitialiseTile(Node n)
    {
        rend = GetComponent<SpriteRenderer>();
        base.InitialiseTile(n);
    }

    public override void UpdateTile()
    {
        if (!down && node.itemType != ItemTypes.None)
        {
            down = true;
            rend.sprite = pressedSprite;
            GridManager.Instance.ButtonDown();
        }
        else if (down && node.itemType == ItemTypes.None)
        {
            down = false;
            rend.sprite = unpressedSprite;
            GridManager.Instance.ButtonRelease();
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
