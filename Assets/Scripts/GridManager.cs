using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;
    private List<Node> nodes = new List<Node>();
    [HideInInspector] public bool buttonDown = false;
    public int specialExitDestinationStage;
    [HideInInspector] public int exitDestinationStage;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);

        exitDestinationStage = specialExitDestinationStage;
        if (exitDestinationStage == 0) 
            exitDestinationStage = GameManager.Instance.stageIndex + 1;
    }


    void Start()
    {
        nodes.Clear();
        foreach (Node node in transform.GetComponentsInChildren<Node>())
        {
            nodes.Add(node);
            node.InitialiseNode();
        }
    }

    public void UpdateGrid()  // When the player does something
    {
        foreach (Node node in nodes)
        {
            node.UpdateTiles();
        }

        foreach (Node node in nodes)
        {
            node.UpdateItems();
        }

        foreach (Node node in nodes)
        {
            node.LateUpdateTiles();
        }

        foreach (Node node in nodes)
        {
            node.LateUpdateItems();
        }

        foreach (Node node in nodes)
        {
            node.PostUpdate();
        }
    }

    public void ButtonDown()
    {
        buttonDown = true;

        foreach (Node node in nodes)
        {
            if (node.tileType == TileTypes.Spikes)
                ((SpikeTile)node.tile).ButtonDown();
        }
    }

    public void ButtonRelease()
    {
        buttonDown = false;

        foreach (Node node in nodes)
        {
            if (node.tileType == TileTypes.Spikes)
                ((SpikeTile)node.tile).ButtonUp();
        }
    }
}
