using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOnStart : MonoBehaviour
{
    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = true;
    }
}
