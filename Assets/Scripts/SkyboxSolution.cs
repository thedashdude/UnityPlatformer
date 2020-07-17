using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxSolution : MonoBehaviour
{
    private Transform world;
    // Start is called before the first frame update
    void Start()
    {
        world = GameObject.FindGameObjectWithTag("Universe").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = world.rotation;
    }
}
