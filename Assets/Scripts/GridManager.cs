using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;
    private List<Node> nodes = new List<Node>();


    void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(this);
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
            node.PostUpdate();
        }
    }
}
