using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Spikes : Item
{
    public Sprite upSprite, downSprite;
    private bool up;
    private SpriteRenderer rend;

    public override void InitialiseItem(Node n)
    {
        rend = transform.GetChild(0).GetComponent<SpriteRenderer>();
        base.InitialiseItem(n);
    }

    public override void UpdateItem()
    {
        if (up && GridManager.Instance.buttonDown)
        {
            up = false;
            rend.sprite = downSprite;
        }
        else if (!up && !GridManager.Instance.buttonDown)
        {
            up = true;
            rend.sprite = upSprite;
        }

        // Player is also an item! They won't be able to walk over

        base.UpdateItem();
    }

    public override void Interact(Vector2 sideInteracted)
    {
        base.Interact(sideInteracted);
    }

    public override void Kick(Vector2 sideKicked)
    {
        //if () kill

        base.Kick(sideKicked);
    }
}
