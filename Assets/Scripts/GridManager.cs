using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private List<Node> nodes = new List<Node>();

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
    }
}
