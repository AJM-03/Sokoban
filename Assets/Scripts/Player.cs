using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : Item
{
    public static Player Instance;

    public GameObject heldTile;

    public Vector2 playerDirection;


    void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(this);
    }

    private void Start()
    {
        playerDirection = new Vector2 (0, -1);
    }



    void Update()
    {
        
    }




    public void PickUpTile(Tile t)
    {
        if (heldTile == null)
        {
            heldTile = t.prefab;

            t.node.tile = null;
            t.node.tileObject = null;
            t.node.tileType = TileTypes.Null;

            Destroy(gameObject);
        }
        else
            CannotInteract();
    }

    public void PlaceTile(Node n)
    {
        if (heldTile != null)
        {
            n.tileObject = GameObject.Instantiate(heldTile, n.transform.position, n.transform.rotation, n.transform);
            n.tile = n.tileObject.transform.GetComponent<Tile>();
            n.tileType = n.tile.type;

            heldTile = null;
        }
    }


    public void CannotInteract()
    {

    }


    public override void InitialiseItem(Node n)
    {
        base.InitialiseItem(n);
    }
}
