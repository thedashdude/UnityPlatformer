using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLineScript : MonoBehaviour
{
    public int keys;
    public PlayerRigidBody playerScript;
    public GameObject warp7;
    public float radius;
    private bool activated;

    // Start is called before the first frame update
    void Start()
    {
        activated = false;
        warp7.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!activated && keys <= playerScript.keys) 
        {
            activated = true;
            warp7.SetActive(true);
        }


    }
}
