using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickupScript : MonoBehaviour
{
    public LayerMask playerLayer;
    public float radius;
    public PlayerRigidBody play;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.localRotation = transform.localRotation * Quaternion.AngleAxis(speed * Time.deltaTime, Vector3.up);
    }
    void FixedUpdate()
    {
        if (Physics.CheckSphere(transform.position, radius, playerLayer))
        {
            play.AddKey();
            Destroy(gameObject);
        }
    }
}
