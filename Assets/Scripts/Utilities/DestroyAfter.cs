using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    public float destroyTime;

    void Awake()
    {
        Destroy(gameObject, destroyTime);
    }

    private void Update()
    {
        
    }
}
