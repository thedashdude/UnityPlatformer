using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityPickupLogic : MonoBehaviour
{
    public LayerMask playerLayer;
    public float radius;
    private PlayerRigidBody play;
    public GameObject[] killList;
    // Start is called before the first frame update
    void Start()
    {
        play = GameObject.FindGameObjectWithTag("Player Origin").GetComponentInChildren<PlayerRigidBody>();
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
            foreach (GameObject o in killList) 
            {
                Destroy(o);
            }

            Destroy(gameObject);
        }
    }
}
