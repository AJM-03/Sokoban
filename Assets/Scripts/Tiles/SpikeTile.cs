using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTile : Tile
{
    public Sprite upSprite, downSprite;
    private bool up;
    private SpriteRenderer rend;
    public bool inverted;

    public override void InitialiseTile(Node n)
    {
        rend = GetComponent<SpriteRenderer>();

        CheckButtonState();

        base.InitialiseTile(n);
    }

    public void ButtonDown()
    {
        if (up && !inverted)
        {
            up = false;
            blocksObjects = false;
            rend.sprite = downSprite;
        }
        else if (!up && inverted)
        {
            up = true;
            blocksObjects = true;
            rend.sprite = upSprite;
            CheckForDeaths();
        }
    }

    public void ButtonUp()
    {
        if (!up && !inverted)
        {
            up = true;
            blocksObjects = true;
            rend.sprite = upSprite;
            CheckForDeaths();
        }
        else if (up && inverted)
        {
            up = false;
            blocksObjects = false;
            rend.sprite = downSprite;
        }
    }

    public override void LateUpdateTile()
    {
        CheckButtonState();

        CheckForDeaths();

        base.LateUpdateTile();
    }

    private void CheckButtonState()
    {
        if (!inverted)
        {
            if (up && GridManager.Instance.buttonDown)
                ButtonDown();
            else if (!up && !GridManager.Instance.buttonDown)
                ButtonUp();
        }
        else
        {
            if (!up && GridManager.Instance.buttonDown)
                ButtonUp();
            else if (up && !GridManager.Instance.buttonDown)
                ButtonDown();
        }
    }

    private void CheckForDeaths()
    {
        if (up && node && node.item != null)
        {
            if (node.itemType == ItemTypes.Player)
            {
                Player.Instance.KillPlayer();
            }

            if (node.itemType == ItemTypes.Enemy && ((Enemy) node.item).canDieToSpikes)
            {
                ((Enemy)node.item).KillEnemy(true);
            }
        }
    }

    public override void Interact(Vector2 sideInteracted)
    {
        base.Interact(sideInteracted);
    }

    public override void Kick(Vector2 sideKicked)
    {
        if (up)
        {
            Player.Instance.KillPlayer();
        }

        base.Kick(sideKicked);
    }
}
