using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceCube : MonoBehaviour
{
    public Vector3 oof;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        other.attachedRigidbody.AddForce(oof);
    }
}
