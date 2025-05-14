using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestItem : Item
{
    public int reward = 1;

    private int kickCounter = 0;
    public int kicksToUpgrade = 3;

    private bool upgraded;
    public int upgradedReward = 3;
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

    public override void Interact(Vector2 sideInteracted)
    {
        if (sideInteracted == new Vector2(0, -1))
        {
            if (!upgraded) Player.Instance.potionCount += reward;
            else  Player.Instance.potionCount += upgradedReward;
            Player.Instance.potionCountUI.text = "x" + Player.Instance.potionCount.ToString();
        }

        base.Interact(sideInteracted);
    }

    public override void Kick(Vector2 sideKicked)
    {
        if (sideKicked == new Vector2(0, 1))
        {
            kickCounter++;
            if (kickCounter == 3)
            {
                upgraded = true;
                sprite.sprite = upgradedSprite;
                Player.Instance.ExclamationReaction();
            }
        }

        base.Kick(sideKicked);
    }
}
