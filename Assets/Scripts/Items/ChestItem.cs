using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestItem : Item
{
    private int kickCounter = 0;
    public int kicksToUpgrade = 3;

    public Sprite upgradedSprite;
    public SpriteRenderer sprite;

    public override void InitialiseItem(Node n)
    {
        base.InitialiseItem(n);
    }

    public override void UpdateItem()
    {
        base.UpdateItem();
    }

    public override void Interact()
    {
        base.Interact();
    }

    public override void Kick(Vector2 sideKicked)
    {
        if (sideKicked == new Vector2(0, 1))
        {
            kickCounter++;
            if (kickCounter == 3)
            {
                sprite.sprite = upgradedSprite;
                Player.Instance.ExclamationReaction();
            }
        }

        base.Kick(sideKicked);
    }
}
