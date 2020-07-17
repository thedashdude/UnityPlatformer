using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityPickupLogic : MonoBehaviour
{
    public LayerMask playerLayer;
    public float radius;
    public PlayerRigidBody play;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate() 
    {
        if (Physics.CheckSphere(transform.position, radius, playerLayer)) 
        {
            play.AddAmmo();
            Destroy(gameObject);
        }
    }
}
