using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainmentScript : MonoBehaviour
{
    int intersectingCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Intersecting() 
    {
        return intersectingCount > 0;
    }

    void OnCollisionEnter(Collision other)
    {
        intersectingCount++;
    }
    void OnCollisionExit(Collision other)
    {
        intersectingCount--;
    }
}
