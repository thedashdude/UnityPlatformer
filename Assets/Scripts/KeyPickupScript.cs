using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickupScript : MonoBehaviour
{
    public LayerMask playerLayer;
    public float radius;
    private PlayerRigidBody play;
    public float speed;
    public GameObject[] killList;
    // Start is called before the first frame update
    void Start()
    {
        play = GameObject.FindGameObjectWithTag("Player Origin").GetComponentInChildren<PlayerRigidBody>();
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
            foreach (GameObject o in killList)
            {
                Destroy(o);
            }
            Destroy(gameObject);
        }
    }
}
