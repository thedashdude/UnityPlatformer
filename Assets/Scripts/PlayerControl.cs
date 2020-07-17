using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public CharacterController controller;
    public Transform horizontal;
    public Transform vertical;
    public float topSpeed = 10f;
    public float midSpeed = 1f;
    public float acc = 15f;
    public float accEarly = 100f;
    public float friction = 5f;
    public float infSpeed = 150f;
    public float slipFriction = 3f;


    

    public Vector3 inputMovement = new Vector3(0f, 0f, 0f);

    public Vector3 otherMovement = new Vector3(0f, 0f, 0f);

    public Vector3 netMovement = new Vector3(0f, 0f, 0f);
    //public Vector3 actualDelta = new Vector3(0f, 0f, 0f);

    private float errorFactor = 0.1f;
    private Vector3 oldNetV = new Vector3(0f, 0f, 0f);
    private Vector3 lastLocation = new Vector3(0f, 0f, 0f);
    private float lastDT = 0f;

    public float gravity = -9.81f;
    public Vector3 gravVelocity = new Vector3(0f, 0f, 0f);
    public Transform groundCheck;
    public float groundDist = 0.5f;
    public LayerMask groundMask;
    bool isGrounded;

    public bool doErrorCorrect = false;
    public float jumpHeight = 5f;
    public bool grinding = false;

    // Start is called before the first frame update
    void Start()
    {
        lastLocation = controller.transform.position;
    }

    public void setGrinding(bool b) 
    { 
        grinding = b;
    }
    
    // Update is called once per frame
    void Update()
    {
        Vector3 newInputMovement = inputMovement;
        Vector3 newGravVelocity = gravVelocity;
        Vector3 newOtherMovement= otherMovement;
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDist, groundMask);

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        float mag = (x * x + z * z);
        if (x != 0 || z != 0)
        {
            x = x / Mathf.Sqrt(mag);
            z = z / Mathf.Sqrt(mag);
        }


        Vector3 move = Vector3.zero;
        if (!isGrounded)
        {
            move = horizontal.right * x * (acc-friction) * Time.deltaTime + horizontal.forward * z * (acc-friction) * Time.deltaTime;
        }
        else if (inputMovement.magnitude < midSpeed)
        {
            move = horizontal.right * x * accEarly * Time.deltaTime + horizontal.forward * z * accEarly * Time.deltaTime;
        }
        else
        {
            move = horizontal.right * x * acc * Time.deltaTime + horizontal.forward * z * acc * Time.deltaTime;
        }

        newInputMovement = newInputMovement + move;

        newInputMovement = Vector3.ClampMagnitude(newInputMovement, topSpeed);

        if (isGrounded)
        {
            newInputMovement = Vector3.ClampMagnitude(newInputMovement, Mathf.Clamp(newInputMovement.magnitude - friction * Time.deltaTime, 0f, topSpeed));
        }

        if (isGrounded)
        {
            newOtherMovement = Vector3.ClampMagnitude(newOtherMovement, Mathf.Clamp(newOtherMovement.magnitude - slipFriction * Time.deltaTime, 0f, infSpeed));
        }




        newGravVelocity.y += gravity * Time.deltaTime;
        if (isGrounded)
        {
            newGravVelocity.y = Mathf.Clamp(newGravVelocity.y, 0f, 1000f);
            if (Input.GetButtonDown("Jump"))
            {
                newGravVelocity.y = Mathf.Sqrt(-2 * jumpHeight * gravity);
            }
        }



        if (!grinding) 
        {
            inputMovement = newInputMovement;
            gravVelocity = newGravVelocity;
            otherMovement = newOtherMovement;
        }

        if (doErrorCorrect)
        {
            /*
            if (!((lastLocation + oldNetV*lastDT) == (controller.transform.position)))
            {
                Vector3 lastVelocity = (controller.transform.position - lastLocation) / (lastDT);
                float magnitude = lastVelocity.magnitude;
                gravVelocity.y = lastVelocity.y;
                lastVelocity.y = 0;
                inputMovement = Vector3.ClampMagnitude(lastVelocity, topSpeed);
                //otherMovement = lastVelocity - inputMovement;
                //actualDelta = Vector3.zero;
            }*/
            if ( (controller.collisionFlags & CollisionFlags.Above) != 0)
            {
                gravVelocity.y = Mathf.Clamp(gravVelocity.y, -infSpeed, 0f);
            }
            if ( (controller.collisionFlags & CollisionFlags.Below) != 0 )
            {
                gravVelocity.y = Mathf.Clamp(gravVelocity.y, 0f, infSpeed);
            }
        }

        netMovement = gravVelocity + inputMovement + otherMovement;

        lastLocation = controller.transform.position;

        controller.Move(netMovement * Time.deltaTime);

        oldNetV = netMovement;
        lastDT = Time.deltaTime;
        grinding = false;
    }
}
