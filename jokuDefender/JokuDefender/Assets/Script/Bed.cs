using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{
    public int healthPoints = 20;
    public static Transform bedPosition {  get; private set; }
    void Start()
    {
        bedPosition = gameObject.transform;
    }

    void Update()
    {
        if(healthPoints <= 0)
        {
            Destroy(gameObject);
        }
    }
}
